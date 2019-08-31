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
    private SynthesisController m_SynthesisController;

    private int currentIndex = -1;
    private int tabCount = 2;
    private int slotsCount = 25;

    private List<GameObject> tabList;
    private List<GameObject> contentList;
    private List<GameObject> slotList;
    void Start()
    {
        Init();

        CreateAllTabs();
        CreateAllContens();
        CreateAllSlots();
        SwitchTabAndContents(0);

    }

    private void Init()
    {
        m_Transform = gameObject.GetComponent<Transform>();
        m_SynthesisPanelView = gameObject.GetComponent<SynthesisPanelView>();
        m_SynthesisPanelModel = gameObject.GetComponent<SynthesisPanelModel>();
        m_SynthesisController = m_Transform.Find("Right").GetComponent<SynthesisController>();

        tabList = new List<GameObject>();
        contentList = new List<GameObject>();
        slotList = new List<GameObject>();
    }
    /// <summary>
    /// 生成所有Tab
    /// </summary>
    private void CreateAllTabs()
    {
        for (int i = 0; i < tabCount; i++)
        {
            GameObject go = Instantiate<GameObject>(m_SynthesisPanelView.TabItemType_Prefab, m_SynthesisPanelView.Tab_Transform);
            Sprite temp = m_SynthesisPanelView.GetSpriteByName(m_SynthesisPanelModel.TabIconName[i]);
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
            List<List<ContentItem>> temp = m_SynthesisPanelModel.GetJsonDataByName("SynthesisContentsJsonData");
            GameObject go = Instantiate<GameObject>(m_SynthesisPanelView.Content_Prefab, m_SynthesisPanelView.Content_Transform);
            go.GetComponent<SynthesisContentContorller>().Init(i, m_SynthesisPanelView.ContentItem_Prefab, temp[i]);
            contentList.Add(go);
        }
    }
    /// <summary>
    /// 生成所有图谱槽
    /// </summary>
    private void CreateAllSlots()
    {
        for (int i = 0; i < slotsCount; i++)
        {
            GameObject slotObj = Instantiate(m_SynthesisPanelView.ContentSlot_Prefab, m_SynthesisPanelView.Center_Transform);
            slotObj.name = "slot" + i;
            slotList.Add(slotObj);
        }
    }
    /// <summary>
    /// 图谱槽数据填充
    /// </summary>
    private void CreateSlotsContent(int id)
    {
        SynthesisMapItem temp = m_SynthesisPanelModel.GetMapItemByID(id);
        ClearSlots();
        if (temp != null)
        {
            for (int i = 0; i < temp.MapContents.Length; i++)
            {
                if (temp.MapContents[i] != "0")
                {
                    Sprite sp = m_SynthesisPanelView.GetSpritByID(temp.MapContents[i]);
                    slotList[i].GetComponent<SynthesisSlotContorller>().Init(temp.MapContents[i], sp);
                }
            }
            m_SynthesisController.Init(temp.MapName);
        }
        else { m_SynthesisController.Init(null); } //清空合成物品图标
    }
    /// <summary>
    /// 清空合成图谱槽
    /// </summary>
    private void ClearSlots()
    {
        for (int i = 0; i < slotList.Count; i++)
        {
            slotList[i].GetComponent<SynthesisSlotContorller>().Reset();
        }
    }
    /// <summary>
    /// 切换tab和显示内容
    /// </summary>
    public void SwitchTabAndContents(int index)
    {
        if (index == currentIndex) return;

        for (int i = 0; i < tabCount; i++)
        {
            tabList[i].GetComponent<SynthesisTabContorller>().SetDefault();
            contentList[i].SetActive(false);
        }
        tabList[index].GetComponent<SynthesisTabContorller>().SetSelect();
        contentList[index].SetActive(true);
        this.currentIndex = index;
    }

}
