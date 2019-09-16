using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 库存(背包)面板控制器
/// </summary>
public class InventoryPanelController : MonoBehaviour, IUIPanelShowHide
{
    //单例
    public static InventoryPanelController _Instance;
    //持有自身M V脚本
    private InventoryPanelModel m_inventoryModel;
    private InventoryPanelView m_inventoryView;

    List<GameObject> slotList = null;   //物品槽数组
    private int inventoryCount = 27;

    void Awake()
    {
        _Instance = this;
    }
    void Start()
    {
        m_inventoryModel = gameObject.GetComponent<InventoryPanelModel>();
        m_inventoryView = gameObject.GetComponent<InventoryPanelView>();

        slotList = new List<GameObject>();

        CreateAllSlot();
        CreateAllItem();
    }

    /// <summary>
    /// 生成所有物品槽
    /// </summary>
    private void CreateAllSlot()
    {
        for (int i = 0; i < inventoryCount; i++)
        {
            GameObject go = Instantiate<GameObject>(m_inventoryView.Slot_Prefab, m_inventoryView.Grid_Transform);
            go.name = "InventorySlot_" + i;
            slotList.Add(go);
        }
    }
    /// <summary>
    /// 生成所有物品
    /// </summary>
    private void CreateAllItem()
    {
        List<InventoryItem> list = m_inventoryModel.ReadJson("InventoryJsonData");

        for (int i = 0; i < list.Count; i++)
        {
            GameObject item = Instantiate<GameObject>(m_inventoryView.Item_Prefab, slotList[i].GetComponent<Transform>());
            item.name = "InventoryItem";
            item.GetComponent<InventoryItemController>().Init(list[i].ItemID, list[i].ItemName, list[i].ItemNum, list[i].DuraBar);
        }
    }
    /// <summary>
    /// 将物品添加进背包物品槽
    /// </summary>
    public void AddItem(List<GameObject> itemList)
    {
        int index = 0;
        for (int i = 0; i < slotList.Count; i++)
        {
            Transform slotTransform = slotList[i].GetComponent<Transform>();
            if (slotTransform.Find("InventoryItem") == null && index < itemList.Count)
            {
                itemList[index].GetComponent<Transform>().SetParent(slotTransform);
                itemList[index].GetComponent<Transform>().localPosition = Vector3.zero;
                itemList[index].GetComponent<InventoryItemController>().IsInInventory = true;
                index++;
            }
        }
    }
    public void SendAddItemToSynthesisPanel(GameObject temp)
    {
        SynthesisPanelContorller._Instance.AddItemToSynthesisPanel(temp);
    }

    public void ShowPanel()
    {
        gameObject.SetActive(true);
    }

    public void HidePanel()
    {
        gameObject.SetActive(false);
    }

}
