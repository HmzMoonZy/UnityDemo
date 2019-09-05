using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 库存(背包)Item数据实体类
/// </summary>
public class InventoryItem
{
    private string itemName;
    private int itemNum;
    private int itemID;

    public string ItemName { get { return itemName; } set { itemName = value; } }
    public int ItemNum { get { return itemNum; } set { itemNum = value; } }
    public int ItemID { get { return itemID; } set { itemID = value; } }

    public InventoryItem() { }
    public InventoryItem(string itemName, int itemNum, int itemID)
    {
        this.itemName = itemName;
        this.itemNum = itemNum;
        this.itemID = itemID;
    }
    public override string ToString()
    {
        return string.Format("物品名称:{0}|物品数量:{1}|物品ID:{2}", this.itemName, this.itemNum,this.itemID);
    }
}
