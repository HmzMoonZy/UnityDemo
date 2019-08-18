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
    public List<List<string>> GetJsonDataByName(string filename)
    {
        //获取数据文本
        List<List<string>> temp = new List<List<string>>();
        string jsStr = Resources.Load<TextAsset>("JsonData/" + filename).text;

        //解析
        JsonData jsdata = JsonMapper.ToObject(jsStr);
        //Debug.Log(jsdata.Count);      2
        for (int i = 0; i < jsdata.Count; i++)
        {
            List<string> temp2 = new List<string>();
            JsonData jsdata2 = jsdata[i]["Type"];
            //Debug.Log(jsdata2.Count);     4   2
            for (int j = 0; j < jsdata2.Count; j++)
            {
                temp2.Add(jsdata2[j]["ItemName"].ToString());
            }
            temp.Add(temp2);
        }
        return temp;
    }

}
