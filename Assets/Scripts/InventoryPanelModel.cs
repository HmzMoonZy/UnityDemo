using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class InventoryPanelModel : MonoBehaviour
{

    void Start()
    {

    }

    /// <summary>
    /// 读取Json文件数据为Item实体类,存储在List中并返回
    /// </summary>
    /// <param name="jsFileName">json数据文件名</param>
    /// <returns></returns>
    public List<InventoryItem> ReadJson(string jsFileName)
    {
        List<InventoryItem> list = new List<InventoryItem>();
        string jsStr = Resources.Load<TextAsset>("Inventory/" + jsFileName).text;
        JsonData jsData = JsonMapper.ToObject(jsStr);

        for (int i = 0; i < jsData.Count; i++)
        {
           
            InventoryItem ii = JsonMapper.ToObject<InventoryItem>(jsData[i].ToJson());
            
            list.Add(ii);
            Debug.Log(ii.ItemName);
        }

        return list;
    }

}
