using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 库存(背包)Item数据实体类
/// </summary>
public class InventoryItem
{
    private string itemName;        //对应文件名 
    private int itemNum;            //物品数量
    private int itemID;             //物品ID
    private int duraBar;            //耐久度标记


    public string ItemName { get { return itemName; } set { itemName = value; } }
    public int ItemNum { get { return itemNum; } set { itemNum = value; } }
    public int ItemID { get { return itemID; } set { itemID = value; } }
    public int DuraBar { get { return duraBar; } set { duraBar = value; } }

    public InventoryItem() { }
    public InventoryItem(string itemName, int itemNum, int itemID, int duraBar)
    {
        this.itemName = itemName;
        this.itemNum = itemNum;
        this.itemID = itemID;
        this.duraBar = duraBar;
    }
    public override string ToString()
    {
        return string.Format("物品名称:{0}|物品数量:{1}|物品ID:{2}|耐久度:{3}", this.itemName, this.itemNum,this.itemID,this.duraBar);
    }
}
