using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolBarView : MonoBehaviour
{
    private Transform _transform;
    private Transform bg_transform;

    private GameObject toolBarSlot_Prefab;

    public Transform BG_Transform { get { return bg_transform; } set { bg_transform = value; } }
    public GameObject ToolBarSlot_Prefab { get { return toolBarSlot_Prefab; } set { toolBarSlot_Prefab = value; } }

    void Start()
    {
        _transform = gameObject.GetComponent<Transform>();
        bg_transform = _transform.Find("Grid").GetComponent<Transform>();

        toolBarSlot_Prefab = Resources.Load<GameObject>("ToolBarSlot");
    }
}
