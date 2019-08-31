using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 合成面板物品槽控制器
/// </summary>
public class SynthesisSlotContorller : MonoBehaviour
{
    private Transform m_transform;
    private Image m_Image;
    void Awake()
    {
        m_transform = gameObject.GetComponent<Transform>();
        m_Image = m_transform.Find("Image").GetComponent<Image>();
        m_Image.gameObject.SetActive(false);
    }
    public void Init(Sprite sprite)
    {
        m_Image.sprite = sprite;
        m_Image.gameObject.SetActive(true);
    }
    public void Reset()
    {
        m_Image.gameObject.SetActive(false);
    }
}
