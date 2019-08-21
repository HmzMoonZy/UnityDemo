using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 合成面板Tab控制器
/// </summary>
public class SynthesisTabContorller : MonoBehaviour
{

    private Transform m_transform;
    private Image m_icon;
    private Button m_button;
    private Image m_buttonBG;

    private int index = -1;
    void Awake()
    {
        m_transform = gameObject.GetComponent<Transform>();
        m_button = m_transform.Find("Button").GetComponent<Button>();
        m_icon = m_transform.Find("Icon").GetComponent<Image>();
        m_buttonBG = m_transform.Find("Button").GetComponent<Image>();

        m_button.onClick.AddListener(ButtonClick);
    }
    /// <summary>
    /// 初始化自身
    /// </summary>
    public void Init(int index, Sprite icon)
    {
        this.index = index;
        gameObject.name = "tab" + this.index;
        m_icon.sprite = icon;
    }
    /// <summary>
    /// tab默认状态
    /// </summary>
    public void SetDefault()
    {
        if (m_buttonBG.color.a != 255)
            m_buttonBG.color = new Color(255, 255, 255, 255);
    }
    /// <summary>
    /// tab选中状态
    /// </summary>
    public void SetSelect()
    {
        m_buttonBG.color = new Color(255, 255, 255, 0);
    }

    private void ButtonClick()
    {
        SendMessageUpwards("SwitchTabAndContents", this.index);
    }

}
