using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerInfoPanel : MonoBehaviour
{
    private Transform m_transform;

    private Image m_hp_bar;
    private Image m_vit_bar;

    void Awake()
    {
        m_transform = gameObject.GetComponent<Transform>();

        m_hp_bar = m_transform.Find("hp_bg/icon/bar").GetComponent<Image>();
        m_vit_bar = m_transform.Find("vit_bg/icon/bar").GetComponent<Image>();
    }
    public void FixUI(int hp, int vit)
    {
        m_hp_bar.fillAmount = hp / 1000f;
        m_vit_bar.fillAmount = vit / 100f;
    }
}
