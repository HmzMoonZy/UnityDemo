using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 合成面板View脚本
/// 在这里加载所有合成面板需要的资源
/// </summary>
public class SynthesisPanelView : MonoBehaviour
{
    private GameObject tabItemType_Prefab;
    private GameObject content_Prefab;
    private GameObject contentItem_Prefab;

    private Transform m_transform;
    private Transform content_Transform;
    private Transform tab_Transform;

    private Dictionary<string, Sprite> tabIconDic;

    public GameObject TabItemType_Prefab { get { return tabItemType_Prefab; } }
    public GameObject Content_Prefab { get { return content_Prefab; } }
    public GameObject ContentItem_Prefab { get { return contentItem_Prefab; } }
    public Transform Content_Transform { get { return content_Transform; } }
    public Transform Tab_Transform { get { return tab_Transform; } }
    void Awake()
    {
        Init();

        LoadAllTabIcon();
    }

    private void Init()
    {
        tabItemType_Prefab = Resources.Load<GameObject>("Synthesis/TabItemType");
        content_Prefab = Resources.Load<GameObject>("Synthesis/Content");
        contentItem_Prefab = Resources.Load<GameObject>("Synthesis/ContentItem");

        m_transform = gameObject.GetComponent<Transform>();
        content_Transform = m_transform.Find("Left/Contents").GetComponent<Transform>();
        tab_Transform = m_transform.Find("Left/Tabs").GetComponent<Transform>();

        tabIconDic = new Dictionary<string, Sprite>();
    }
    /// <summary>
    /// 加载所有TabIcon并添加进tabIconDic
    private void LoadAllTabIcon()
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>("TabIcon");
        for (int i = 0; i < sprites.Length; i++)
        {
            tabIconDic.Add(sprites[i].name, sprites[i]);
        }
    }
    /// <summary>
    /// 根据Icon文件名返回icon图标
    /// </summary>
    public Sprite LoadSpriteByName(string key)
    {
        Sprite temp = null;
        tabIconDic.TryGetValue(key, out temp);
        return temp;
    }
}
