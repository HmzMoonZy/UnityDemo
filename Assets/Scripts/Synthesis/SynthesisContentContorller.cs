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

    public void Init(int index, GameObject prefab, List<string> list)
    {
        this.index = index;
        gameObject.name = "Content" + index;
        CreateContentItem(prefab, list);
    }
    /// <summary>
    /// 生成所有内容选项
    /// </summary>
    private void CreateContentItem(GameObject prefab, List<string> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            GameObject go = Instantiate<GameObject>(prefab, m_transform);
            go.GetComponent<SynthesisContentItemContorller>().Init(list[i]);
        }
    }
    public void SelectItem(SynthesisContentItemContorller item)
    {
        if (currentItem == item) return;
        if (currentItem != null) currentItem.SetDefault();
        item.SetSelect();
        currentItem = item;
    }
}
