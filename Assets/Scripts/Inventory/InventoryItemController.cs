using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; //实现拖拽功能的命名空间

/// <summary>
/// 库存(背包)Item自身控制脚本
/// </summary>
public class InventoryItemController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler{
    private RectTransform m_RectTransform;
    private Transform parent_Transform;

    private CanvasGroup m_CanvasGroup;

    private Image m_Img;
    private Text m_Text;

    private void Awake()
    {
        m_RectTransform = gameObject.GetComponent<RectTransform>();
        parent_Transform = GameObject.Find("InventoryPanel").GetComponent<Transform>();

        m_CanvasGroup = gameObject.GetComponent<CanvasGroup>();

        m_Img = gameObject.GetComponent<Image>();
        m_Text = m_RectTransform.Find("Num").GetComponent<Text>();
    }
    /// <summary>
    /// 创建Item预制体
    /// </summary>
    public void SetItem(string fileName, int count)
    {
        this.m_Img.sprite = Resources.Load<Sprite>("Item/" + fileName);
        this.m_Text.text = count.ToString();
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        //关闭射线检测,用于寻找拖拽目的物体。
        m_CanvasGroup.blocksRaycasts = false;
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        m_RectTransform.SetParent(parent_Transform);    //拖拽时在最上层显示
        Vector3 pos;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(m_RectTransform, eventData.position, eventData.enterEventCamera, out pos);
        m_RectTransform.position = pos;
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log(eventData.pointerEnter.name);
        m_RectTransform.SetParent(eventData.pointerEnter.GetComponent<Transform>());
        m_RectTransform.localPosition = Vector3.zero;
        m_CanvasGroup.blocksRaycasts = true;
    }
}
