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

    private bool isTarget = false;      //能否成为拖拽的目标
    public bool IsTarget {
        get { return isTarget; }
        set { IsTarget = value; }
    }

    private string id;
    public string ID { get { return id; } }
    void Awake()
    {
        m_transform = gameObject.GetComponent<Transform>();
        m_Image = m_transform.Find("Image").GetComponent<Image>();
        m_Image.color = new Color(1, 1, 1, 0.23f);
        m_Image.gameObject.SetActive(false);
        m_Image.gameObject.AddComponent<CanvasGroup>().blocksRaycasts = false;
    }
    public void Init(string id, Sprite sprite)
    {
        m_Image.sprite = sprite;
        this.id = id;
        m_Image.gameObject.SetActive(true);
        
        isTarget = true;
    }
    public void Reset()
    {
        m_Image.gameObject.SetActive(false);
    }
}
