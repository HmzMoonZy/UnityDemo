using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class SynthesisPanelModel : MonoBehaviour
{
    private string[] tabCIconName = new string[] { "Icon_House", "Icon_Weapon" };

    public string[] TabIconName { get { return tabCIconName; } }

    /// <summary>
    /// 获取JsonData数据
    /// </summary>
    public List<List<ContentItem>> GetJsonDataByName(string filename)
    {
        //获取数据文本
        List<List<ContentItem>> temp = new List<List<ContentItem>>();
        string jsStr = Resources.Load<TextAsset>("JsonData/" + filename).text;
    
        //解析
        JsonData jsdata = JsonMapper.ToObject(jsStr);
        //Debug.Log(jsdata.Count);      //2
        for (int i = 0; i < jsdata.Count; i++)
        {
            List<ContentItem> temp2 = new List<ContentItem>();
            JsonData jsdata2 = jsdata[i]["Type"];
            //Debug.Log(jsdata2.Count);     //4   2
            for (int j = 0; j < jsdata2.Count; j++)
            {
                //temp2.Add(jsdata2[j]["ItemName"]);
                temp2.Add(JsonMapper.ToObject<ContentItem>(jsdata2[j].ToJson()));
            }
            temp.Add(temp2);
        }
        return temp;
    }
    /// <summary>
    /// 获取合成图谱JSON数据.
    /// </summary>
    public List<SynthesisMapItem> GetMapContents(string jsonName)
    {
        List<SynthesisMapItem> temp = new List<SynthesisMapItem>();
        string jsonStr = Resources.Load<TextAsset>("JsonData/" + jsonName).text;

        JsonData jsonData = JsonMapper.ToObject(jsonStr);
        for (int i = 0; i < jsonData.Count; i++)
        {
            //取临时数据.
            int mapId = int.Parse(jsonData[i]["MapID"].ToString());
            string tempStr = jsonData[i]["MapContents"].ToString();
            string[] mapContents = tempStr.Split(',');
            string mapName = jsonData[i]["MapName"].ToString();
            //构造对象.
            SynthesisMapItem item = new SynthesisMapItem(mapId, mapContents, mapName);
            temp.Add(item);
        }
        return temp;
    }
}
