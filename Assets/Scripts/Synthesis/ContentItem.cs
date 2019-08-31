using System.Collections;
using System.Collections.Generic;
/// <summary>
/// 内容面板数据实体类
/// </summary>
public class ContentItem
{
    private string itemName;
    private int itemID;

    public string ItemName
    {
        get { return itemName; }
        set { itemName = value; }
    }

    public int ItemID
    {
        get { return itemID; }
        set { itemID = value; }
    }
    public ContentItem() { }//默认会把此条构造函数私有化,导致LitJson解析失败
    public ContentItem(string name, int id)
    {
        this.itemName = name;
        this.itemID = id;
    }
    public override string ToString()
    {
        return string.Format("ID:{0}|Name:{1}", this.itemID, this.itemName);
    }

}
