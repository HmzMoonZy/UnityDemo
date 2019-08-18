using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 库存数据实体类
/// </summary>
public class InventoryItem
{
    private string itemName;
    private int itemNum;

    public string ItemName
    {
        get { return itemName; }
        set { itemName = value; }
    }
    public int ItemNum
    {
        get { return itemNum; }
        set { itemNum = value; }
    }

    public InventoryItem() { }
    public InventoryItem(string itemName, int itemNum)
    {
        this.itemName = itemName;
        this.itemNum = itemNum;
    }
    public override string ToString()
    {
        return string.Format("物品名称:{0}|物品数量:{1}", this.itemName, this.itemNum);
    }
}
