using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

/// <summary>
/// 角色控制器
/// </summary>
public class PlayerController : MonoBehaviour
{
    private Transform m_transform;
    private FirstPersonController m_fpsCtrl;  //第一人称角色控制器
    private PlayerInfoPanel m_infoPanel;      //UI管理器
    private BloodScreen m_bloodScreen;        //屏幕血液效果管理器

    private int m_hp = 1000;
    private int m_vit = 100;

    private float vitTimecount = 0;        //vit削减计时器
    void Start()
    {
        //组件查找
        m_transform = gameObject.GetComponent<Transform>();
        m_fpsCtrl = gameObject.GetComponent<FirstPersonController>();
        m_infoPanel = GameObject.Find("Canvas/PlayerInfoPanel").GetComponent<PlayerInfoPanel>();
        m_bloodScreen = GameObject.Find("Canvas/BloodScreen").GetComponent<BloodScreen>();

        StartCoroutine(AutoRegainVit());
        StartCoroutine(AutoRegainHp());
    }

    void FixedUpdate()
    {
        CutPlayerVit();
    }
    /// <summary>
    /// 削减生命值
    /// </summary>
    public void CutPlayerHp(int damage)
    {
        m_hp -= damage;
        m_infoPanel.FixUI(m_hp, m_vit);
        m_bloodScreen.SetBloodScreen(m_hp / 1000f);
    }
    /// <summary>
    /// 削减体力值;
    /// </summary>
    public void CutPlayerVit()
    {
        if (m_fpsCtrl.M_State == PlayerState.RUN)
        {
            vitTimecount += 0.02f;
            if (vitTimecount >= 1)
            {
                m_vit -= 5;
                vitTimecount = 0;
            }
        }
        if (m_fpsCtrl.M_State == PlayerState.WALK)
        {
            vitTimecount += 0.005f;
            if (vitTimecount >= 1)
            {
                m_vit -= 5;
                vitTimecount = 0;
            }
        }
        m_infoPanel.FixUI(m_hp, m_vit);
        FixSpeed();
    }
    /// <summary>
    /// 自动回复体力;
    /// </summary>
    /// <returns></returns>
    private IEnumerator AutoRegainVit()
    {
        while (true)
        {
            yield return new WaitForSeconds(3);
            if (m_vit <= 95 && m_fpsCtrl.M_State == PlayerState.IDLE)
            {
                m_vit += 5;
                FixSpeed();
                m_infoPanel.FixUI(m_hp, m_vit);
            }
        }
    }
    private IEnumerator AutoRegainHp()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);
            if (m_hp < 1000 && m_fpsCtrl.M_State == PlayerState.IDLE)
            {
                m_hp += 100;
                m_infoPanel.FixUI(m_hp, m_vit);
                m_bloodScreen.SetBloodScreen(m_hp / 1000f);
                if (m_hp >= 1000)
                {
                    m_hp = 1000;
                    m_bloodScreen.SetBloodScreen(m_hp / 1000f);
                }
            }
        }
    }
    /// <summary>
    /// 速度修正;
    /// </summary>
    private void FixSpeed()
    {
        m_fpsCtrl.M_RunSpeed = 10 * (0.01f * m_vit);
        m_fpsCtrl.M_WalkSpeed = 5 * (0.01f * m_vit);
    }
}
