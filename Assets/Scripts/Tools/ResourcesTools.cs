using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Unity资源加载工具类
/// </summary>
public sealed class ResourcesTools{
    /// <summary>
    /// 加载文件夹中的资源
    /// </summary>
    public static Dictionary<string, Sprite> LoadFolderAssets(string folderName, Dictionary<string, Sprite> dic)
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>(folderName);
        for (int i = 0; i < sprites.Length; i++)
        {
            dic.Add(sprites[i].name, sprites[i]);
        }
        return dic;
    }
    /// <summary>
    /// 从对应字典中获取图片
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static Sprite GetSpriteFormDic(string key, Dictionary<string, Sprite> dic)
    {
        Sprite temp = null;
        dic.TryGetValue(key, out temp);
        return temp;
    }
}
