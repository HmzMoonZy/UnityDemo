using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SynthesisContentContorller : MonoBehaviour
{
    private int index = -1;
    private Transform m_transform;
    private SynthesisContentItemContorller currentItem = null;
    void Awake()
    {
        m_transform = gameObject.GetComponent<Transform>();
    }

    public void Init(int index, GameObject prefab, List<ContentItem> item)
    {
        this.index = index;
        gameObject.name = "Content" + index;
        CreateContentItem(prefab, item);
    }
    /// <summary>
    /// 生成所有内容选项
    /// </summary>
    private void CreateContentItem(GameObject prefab, List<ContentItem> item)
    {
        for (int i = 0; i < item.Count; i++)
        {
            GameObject go = Instantiate<GameObject>(prefab, m_transform);
            go.GetComponent<SynthesisContentItemContorller>().Init(item[i]);
        }
    }
    /// <summary>
    /// 选择(点击)ContentItem
    /// </summary>
    public void SelectItem(SynthesisContentItemContorller item)
    {
        if (currentItem == item) return;
        Debug.Log(item.ID);
        if (currentItem != null) currentItem.SetDefault();

        item.SetSelect();
        currentItem = item;
        SendMessageUpwards("CreateSlotsContent", item.ID);
    }
}
