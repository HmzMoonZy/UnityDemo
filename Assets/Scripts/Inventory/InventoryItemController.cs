using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; //实现拖拽功能的命名空间

/// <summary>
/// 库存(背包)Item自身控制脚本
/// </summary>
public class InventoryItemController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform m_RectTransform;
    private Transform parent_Transform;
    private Transform last_Transform;

    private CanvasGroup m_CanvasGroup;

    private int id = -1;

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
    public void SetItem(int id, string fileName, int count)
    {
        this.id = id;
        this.m_Img.sprite = Resources.Load<Sprite>("Item/" + fileName);
        this.m_Text.text = count.ToString();
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        //关闭射线检测,用于寻找拖拽目的物体。
        m_CanvasGroup.blocksRaycasts = false;
        last_Transform = m_RectTransform.parent.GetComponentInParent<Transform>();
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
        GameObject target = eventData.pointerEnter;
        if (target != null)
        {
            //背包物品槽
            if (target.tag == "InventorySlot")
            {
                m_RectTransform.SetParent(target.transform);
                SetSlefImageSize(100, 100);
            }
            //交换位置
            else if (target.tag == "InventoryItem")
            {
                //SetParent()效率高于parent = **.
                Transform tempTransform = target.GetComponent<Transform>();
                m_RectTransform.SetParent(tempTransform.parent);
                tempTransform.SetParent(last_Transform);
                tempTransform.localPosition = Vector3.zero;
            }
            //合成图谱槽
            else if (target.tag == "SynthesisSlot")
            {
                //合成图谱非空区域
                if (target.GetComponent<SynthesisSlotContorller>().IsTarget == true)
                {
                    //合成图谱指定道具
                    if (int.Parse(target.GetComponent<SynthesisSlotContorller>().ID) == this.id)
                    {
                        m_RectTransform.SetParent(target.transform);
                        SetSlefImageSize(120, 95);
                    }
                    else { m_RectTransform.SetParent(last_Transform); }
                }
                else { m_RectTransform.SetParent(last_Transform); }
            }
            else { m_RectTransform.SetParent(last_Transform); }
        }
        else { m_RectTransform.SetParent(last_Transform); }
        m_RectTransform.localPosition = Vector3.zero;
        m_CanvasGroup.blocksRaycasts = true;
    }
    /// <summary>
    /// 设置图片的高度和宽度
    /// </summary>
    private void SetSlefImageSize(float width, float height)
    {
        m_RectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
        m_RectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
    }
}
