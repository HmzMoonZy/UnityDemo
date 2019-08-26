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
    private GameObject contentSlot_Prefab;

    private Transform m_transform;
    private Transform content_Transform;
    private Transform tab_Transform;
    private Transform center_Transform;

    private Dictionary<string, Sprite> tabIconDic;
    private Dictionary<string, Sprite> materialDic;

    public GameObject TabItemType_Prefab { get { return tabItemType_Prefab; } }
    public GameObject Content_Prefab { get { return content_Prefab; } }
    public GameObject ContentItem_Prefab { get { return contentItem_Prefab; } }
    public GameObject ContentSlot_Prefab { get { return contentSlot_Prefab; } }

    public Transform Content_Transform { get { return content_Transform; } }
    public Transform Tab_Transform { get { return tab_Transform; } }
    public Transform Center_Transform { get { return center_Transform; } }

    void Awake()
    {
        Init();

        //加载对应文件夹下的资源并添加进对应的字典中
        tabIconDic = ResourcesTools.LoadFolderAssets("TabIcon", tabIconDic);
        materialDic = ResourcesTools.LoadFolderAssets("Material", materialDic);
    }

    private void Init()
    {
        tabItemType_Prefab = Resources.Load<GameObject>("Synthesis/TabItemType");
        content_Prefab = Resources.Load<GameObject>("Synthesis/Content");
        contentItem_Prefab = Resources.Load<GameObject>("Synthesis/ContentItem");
        contentSlot_Prefab = Resources.Load<GameObject>("Synthesis/SynthesisSlot");

        m_transform = gameObject.GetComponent<Transform>();
        content_Transform = m_transform.Find("Left/Contents").GetComponent<Transform>();
        tab_Transform = m_transform.Find("Left/Tabs").GetComponent<Transform>();
        center_Transform = m_transform.Find("Center").GetComponent<Transform>();

        tabIconDic = new Dictionary<string, Sprite>();
        materialDic = new Dictionary<string, Sprite>();
    }

    /// <summary>
    /// 根据Icon文件名返回icon图标
    /// </summary>
    public Sprite GetSpriteByName(string name)
    {
        return ResourcesTools.GetSpriteFormDic(name, tabIconDic);
    }
    /// <summary>
    /// 通过文件名返回图谱槽图片
    /// </summary>
    public Sprite GetSpritByID(string name)
    {
        return ResourcesTools.GetSpriteFormDic(name, materialDic); ;
    }
}
