using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
/// <summary>
/// 合成面板数据处理层(M层)
/// </summary>
public class SynthesisPanelModel : MonoBehaviour
{
    private Dictionary<int, SynthesisMapItem> mapItemDic = null;

    private string[] tabCIconName = new string[] { "Icon_House", "Icon_Weapon" };

    public string[] TabIconName { get { return tabCIconName; } }

    private void Awake()
    {
        mapItemDic = GetMapContents("SynthesisMapJsonData");
    }

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
        for (int i = 0; i < jsdata.Count; i++)
        {
            List<ContentItem> temp2 = new List<ContentItem>();
            JsonData jsdata2 = jsdata[i]["Type"];
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
    private Dictionary<int, SynthesisMapItem> GetMapContents(string jsonName)
    {
        Dictionary<int, SynthesisMapItem> temp = new Dictionary<int, SynthesisMapItem>();
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
            temp.Add(mapId, item);
        }
        return temp;
    }
    /// <summary>
    /// 通过id获取图谱元素
    /// </summary>
    public SynthesisMapItem GetMapItemByID(int id)
    {
        SynthesisMapItem temp = null;
        mapItemDic.TryGetValue(id, out temp);
        return temp;
    }
}
