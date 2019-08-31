using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 内容面板Item自身控制脚本
/// </summary>
public class SynthesisContentItemContorller : MonoBehaviour {
    private Transform m_transform;

    new private string name;
    private int id;

    private Text m_Text;
    private GameObject m_bg;
    private Button m_Button;

    public int ID { get { return id; } }

	void Awake () {
        m_transform = gameObject.GetComponent<Transform>();

        m_Text = m_transform.Find("Name").GetComponent<Text>();
        m_Button = gameObject.GetComponent<Button>();
        m_bg = m_transform.Find("bg").gameObject;

        m_Button.onClick.AddListener(OnClick);
    }
    public void Init(ContentItem item) 
    {
        this.name = item.ItemName;
        this.id = item.ItemID;
        m_Text.text = name;
        SetDefault();
    }

    public void SetDefault()
    {
        m_bg.SetActive(false);
    }
    public void SetSelect()
    {
        m_bg.SetActive(true);
    }
    public void OnClick()
    {
        SendMessageUpwards("SelectItem", this);
    }


}
