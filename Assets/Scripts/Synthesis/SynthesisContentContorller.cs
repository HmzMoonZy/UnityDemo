using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SynthesisContentContorller : MonoBehaviour
{
    private int index = -1;
    private Transform m_transform;
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

    private void CreateContentItem(GameObject prefab, List<string> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            GameObject go = Instantiate<GameObject>(prefab, m_transform);
            go.GetComponent<SynthesisContentItemContorller>().Init(list[i]);
        }

    }
}
