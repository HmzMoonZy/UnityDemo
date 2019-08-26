using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

/// <summary>
/// Json数据工具类
/// </summary>
public sealed class JsonTools
{
    /// <summary>
    /// 通过文件名读取Json文件,并返回对应的List
    /// </summary>
    public static List<T> LoadJsonToList<T>(string fileName)
    {
        List<T> temp = new List<T>();
        string str = Resources.Load<TextAsset>("JsonData/" + fileName).text;
        JsonData jsData = JsonMapper.ToObject(str);
        for (int i = 0; i < jsData.Count; i++)
        {
            T t = JsonMapper.ToObject<T>(jsData[i].ToJson());
            temp.Add(t);
        }
        return temp;
    }
}
