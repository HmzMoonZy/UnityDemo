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

    private Image m_Img;                //道具图标
    private Text m_Text;                //道具数量文本
    private CanvasGroup m_CanvasGroup;  //用于控制射线检测

    private int id = -1;                //自身ID
    private int num;                    //道具数量
    private bool isDrag = false;        //拖拽状态
    private bool isInInventory = true;  //是否处于背包中

    public int ID { get { return id; } set { id = value; } }
    public int Num
    {
        get { return num; }
        set
        {
            num = value;
            m_Text.text = num.ToString();
        }
    }
    public bool IsInInventory
    {
        get { return isInInventory; }
        set
        {
            isInInventory = value;
            if (isInInventory)
            {
                SetSlefImageSize(100, 100);
            }
        }
    }
    private void Awake()
    {
        FindInit();
    }
    private void Update()
    {
        SplitItem();
    }
    /// <summary>
    /// 查找组件
    /// </summary>
    private void FindInit()
    {
        m_RectTransform = gameObject.GetComponent<RectTransform>();
        parent_Transform = GameObject.Find("Canvas").GetComponent<Transform>();

        m_CanvasGroup = gameObject.GetComponent<CanvasGroup>();

        m_Img = gameObject.GetComponent<Image>();
        m_Text = m_RectTransform.Find("Num").GetComponent<Text>();
    }
    /// <summary>
    /// 初始化Item预制体
    /// </summary>
    public void Init(int id, string fileName, int count)
    {
        this.id = id;
        this.m_Img.sprite = Resources.Load<Sprite>("Item/" + fileName);
        this.m_Text.text = count.ToString();
        this.num = count;
    }
    /// <summary>
    /// 拆分道具
    /// </summary>
    private void SplitItem()
    {
        //拖拽状态中点击鼠标右键且物体数量大于一时,拆分.
        if (Input.GetMouseButtonDown(1) && isDrag == true && num > 1)
        {
            //物体拆分操作
            GameObject tempgo = Instantiate(gameObject);
            RectTransform temptransform = tempgo.GetComponent<RectTransform>();
            tempgo.name = "InventoryItem";
            temptransform.SetParent(last_Transform);
            temptransform.localPosition = Vector3.zero;
            temptransform.localScale = Vector3.one;
            //数量操作
            int halve = num / 2;
            tempgo.GetComponent<InventoryItemController>().Num = halve;
            Num -= halve;
            //状态重置
            tempgo.GetComponent<CanvasGroup>().blocksRaycasts = true;
            tempgo.GetComponent<InventoryItemController>().ID = this.id;
        }
    }
    /// <summary>
    /// 合并道具
    /// </summary>
    private void MergeItem(InventoryItemController target)
    {
        target.Num += this.num;
        Destroy(gameObject);
    }
    /// <summary>
    /// 设置图片的高度和宽度
    /// </summary>
    private void SetSlefImageSize(float width, float height)
    {
        m_RectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
        m_RectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
    }
    #region 拖拽事件
    /// <summary>
    /// 开始拖拽
    /// </summary>
    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        isDrag = true;

        //关闭射线检测,用于寻找拖拽目标物体。
        m_CanvasGroup.blocksRaycasts = false;
        last_Transform = m_RectTransform.parent.GetComponentInParent<Transform>();
    }
    /// <summary>
    /// 拖拽中
    /// </summary>
    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        m_RectTransform.SetParent(parent_Transform);    //拖拽时在最上层显示
        Vector3 pos;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(m_RectTransform, eventData.position
            , eventData.enterEventCamera, out pos);
        m_RectTransform.position = pos;
    }
    /// <summary>
    /// 结束拖拽
    /// </summary>
    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        GameObject target = eventData.pointerEnter; //目标物体
        if (target != null) //若有
        {
            DragLogic(target);
        }
        else { m_RectTransform.SetParent(last_Transform); }

        m_RectTransform.localPosition = Vector3.zero;
        m_CanvasGroup.blocksRaycasts = true;
        isDrag = false;
    }
    /// <summary>
    /// 拖拽功能交互逻辑
    /// </summary>
    private void DragLogic(GameObject target)
    {
        InventoryItemController iic = target.GetComponent<InventoryItemController>();
        SynthesisSlotContorller ssc = target.GetComponent<SynthesisSlotContorller>();
        #region 背包物品槽
        if (target.tag == "InventorySlot")
        {
            m_RectTransform.SetParent(target.transform);
            SetSlefImageSize(100, 100);
            isInInventory = true;
        }
        #endregion
        #region 物品交换位置
        else if (target.tag == "InventoryItem")
        {
            //若是相同物品
            if (ID == iic.ID)
            {
                MergeItem(iic);
            }
            //禁止在合成面板中交换位置
            else if (isInInventory && iic.isInInventory)
            {
                Debug.Log(isInInventory && iic.isInInventory);
                //两个物品交换位置
                Transform tempTransform = target.GetComponent<Transform>();
                m_RectTransform.SetParent(tempTransform.parent);
                tempTransform.SetParent(last_Transform);
                tempTransform.localPosition = Vector3.zero;
            }
            else { m_RectTransform.SetParent(last_Transform); }
        }
        #endregion
        #region 合成图谱槽非空且道具对应
        else if (target.tag == "SynthesisSlot" && ssc.IsTarget == true && int.Parse(ssc.ID) == this.id)
        {
            isInInventory = false;
            //target.GetComponent<SynthesisSlotContorller>().IsTarget = false;
            m_RectTransform.SetParent(target.transform);
            SetSlefImageSize(120, 95);
            InventoryPanelController._Instance.SendAddItemToSynthesisPanel(gameObject);
        }
        else { m_RectTransform.SetParent(last_Transform); }
    }
        #endregion
    #endregion

}
