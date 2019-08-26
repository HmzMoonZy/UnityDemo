using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 合成物品控制
/// </summary>
public class SynthesisController : MonoBehaviour
{
    private Transform m_transform;
    private Image icon_Image;
    private Button createone_Button;
    private Button createall_Button;
    void Awake()
    {
        m_transform = gameObject.GetComponent<Transform>();
        icon_Image = m_transform.Find("Good/Icon").GetComponent<Image>();
        createone_Button = m_transform.Find("Button_One").GetComponent<Button>();
        createone_Button = m_transform.Find("Button_All").GetComponent<Button>();

        icon_Image.gameObject.SetActive(false);
    }

    public void Init(string iconName)
    {
        if (iconName != null)
        {
            icon_Image.gameObject.SetActive(true);
            icon_Image.sprite = Resources.Load<Sprite>("Item/" + iconName);
        }
        else
        {
            icon_Image.gameObject.SetActive(false);
        }
    }
}
