using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 合成面板控制器脚本
/// 控制合成面板的逻辑
/// </summary>
public class SynthesisPanelContorller : MonoBehaviour
{
    private Transform m_Transform;
    private SynthesisPanelModel m_SynthesisPanelModel;
    private SynthesisPanelView m_SynthesisPanelView;

    private int tabCount = 2;

    private List<GameObject> tabList;
    private List<GameObject> contentList;
    void Start()
    {
        Init();

        CreateAllTabs();
        CreateAllContens();
        SwitchTabAndContents(0);
    }

    private void Init()
    {
        m_Transform = gameObject.GetComponent<Transform>();
        m_SynthesisPanelView = gameObject.GetComponent<SynthesisPanelView>();
        m_SynthesisPanelModel = gameObject.GetComponent<SynthesisPanelModel>();

        tabList = new List<GameObject>();
        contentList = new List<GameObject>();
    }
    /// <summary>
    /// 生成所有Tab
    /// </summary>
    private void CreateAllTabs()
    {
        for (int i = 0; i < tabCount; i++)
        {
            GameObject go = Instantiate<GameObject>(m_SynthesisPanelView.TabItemType_Prefab, m_SynthesisPanelView.Tab_Transform);
            Sprite temp = m_SynthesisPanelView.LoadSpriteByName(m_SynthesisPanelModel.TabIconName[i]);
            go.GetComponent<SynthesisTabContorller>().Init(i, temp);
            tabList.Add(go);
        }
    }
    /// <summary>
    /// 生成所有内容区域
    /// </summary>
    private void CreateAllContens()
    {
        for (int i = 0; i < tabCount; i++)
        {
            List<List<string>> temp = m_SynthesisPanelModel.GetJsonDataByName("SynthesisContentsJsonData");
            GameObject go = Instantiate<GameObject>(m_SynthesisPanelView.Content_Prefab, m_SynthesisPanelView.Content_Transform);
            go.GetComponent<SynthesisContentContorller>().Init(i,m_SynthesisPanelView.ContentItem_Prefab, temp[i]);
            contentList.Add(go);
        }
    }
    /// <summary>
    /// 切换tab和显示内容
    /// </summary>
    public void SwitchTabAndContents(int index)
    {
        for (int i = 0; i < tabCount; i++)
        {
            tabList[i].GetComponent<SynthesisTabContorller>().SetDefault();
            contentList[i].SetActive(false);
        }
        tabList[index].GetComponent<SynthesisTabContorller>().SetActive();
        contentList[index].SetActive(true);
    }

}
