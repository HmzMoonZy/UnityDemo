using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;
/// <summary>
/// 游戏结束委托
/// </summary>
public delegate void GameOverDelegate();
/// <summary>
/// 角色控制器
/// </summary>
public class PlayerController : MonoBehaviour
{
    private Transform m_transform;
    private FirstPersonController m_fpsCtrl;    //第一人称角色控制器
    private PlayerInfoPanel m_infoPanel;        //玩家UI管理器
    private BloodScreen m_bloodScreen;          //屏幕血液效果管理器

    private bool m_isBrethPlay = false;         //呼吸声是否正在播放
    private AudioSource m_audioSource;

    private int m_hp = 1000;
    private int m_vit = 100;
    private float vitTimecount = 0;             //vit计时器
    private bool isDeath = false;               //玩家死亡标识位
    public event GameOverDelegate GameOverDel;  //游戏结束事件
    void Start()
    {
        //组件查找
        m_transform = gameObject.GetComponent<Transform>();
        m_fpsCtrl = gameObject.GetComponent<FirstPersonController>();
        m_infoPanel = GameObject.Find("Canvas/PlayerInfoPanel").GetComponent<PlayerInfoPanel>();
        m_bloodScreen = GameObject.Find("Canvas/BloodScreen").GetComponent<BloodScreen>();
        m_audioSource = AudioManager._Instance.PlayAudioFormComponent(gameObject
            , ClipName.PlayerBreathingHeavy, false, true);

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
        AudioManager._Instance.PlayAudioAtPos(ClipName.PlayerHurt, m_transform.position);
        m_infoPanel.FixUI(m_hp, m_vit);
        if (m_hp > 0)
        {
            AudioManager._Instance.PlayAudioAtPos(ClipName.PlayerHurt, m_transform.position);
            m_infoPanel.FixUI(m_hp, m_vit);
            m_bloodScreen.SetBloodScreen(m_hp / 1000f);
        }
        else
        {
            GameOver();
        }

    }
    /// <summary>
    /// 自动恢复生命
    /// </summary>
    /// <returns></returns>
    private IEnumerator AutoRegainHp()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (m_hp < 1000 && m_fpsCtrl.M_State == PlayerState.IDLE)
            {
                m_hp += 10;
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
        //触发播放呼吸声;
        if (m_vit < 50 && !m_isBrethPlay)
        {
            m_isBrethPlay = true;
            m_audioSource.Play();
        }
        if (m_vit >= 50)
        {
            m_isBrethPlay = false;
            m_audioSource.Stop();
        }
        //更新UI;
        m_infoPanel.FixUI(m_hp, m_vit);
        //更新玩家速度;
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

    /// <summary>
    /// 速度修正;
    /// </summary>
    private void FixSpeed()
    {
        m_fpsCtrl.M_RunSpeed = 10 * (0.01f * m_vit);
        m_fpsCtrl.M_WalkSpeed = 5 * (0.01f * m_vit);
    }
    /// <summary>
    /// 游戏结束(玩家死亡);
    /// </summary>
    private void GameOver()
    {
        if (isDeath)
        {
            AudioSource temp = AudioManager._Instance.PlayAudioFormComponent(gameObject, ClipName.PlayerDeath, true, false);
        }
        m_fpsCtrl.enabled = false;
        GameOverDel();
        //Destroy(gameObject);
        StartCoroutine(ResetScene());
    }
    private IEnumerator ResetScene()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("ResetScene");
    }
}
