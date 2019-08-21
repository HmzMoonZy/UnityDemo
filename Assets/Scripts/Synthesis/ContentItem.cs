using System.Collections;
using System.Collections.Generic;

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

    public override string ToString()
    {
        return string.Format("ID:{0}|Name:{1}", this.itemID, this.itemName);
    }

}
