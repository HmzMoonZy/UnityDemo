using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class InventoryPanelModel : MonoBehaviour
{
    /// <summary>
    /// 读取Json文件数据为Item实体类,存储在List中并返回
    /// </summary>
    public List<InventoryItem> ReadJson(string fileName)
    {
        return JsonTools.LoadJsonToList<InventoryItem>(fileName);
    }

}
