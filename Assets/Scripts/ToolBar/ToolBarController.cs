using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolBarController : MonoBehaviour
{
    public static ToolBarController _Instance;

    private ToolBarModel _toolBarModel;
    private ToolBarView _toolBarView;

    private List<GameObject> slotList;
    private GameObject selected = null;    //当前选定的物品槽
    private void Awake()
    {
        _Instance = this;
    }
    void Start()
    {
        _toolBarModel = gameObject.GetComponent<ToolBarModel>();
        _toolBarView = gameObject.GetComponent<ToolBarView>();

        slotList = new List<GameObject>();

        CreateAllSlot();
    }
    /// <summary>
    /// 生成所有物品槽
    /// </summary>
    private void CreateAllSlot()
    {
        for (int i = 0; i < 8; i++)
        {
            GameObject slot = Instantiate<GameObject>(_toolBarView.ToolBarSlot_Prefab, _toolBarView.BG_Transform);
            slotList.Add(slot);
            slot.name = "ToolBarSlot";
            slot.GetComponent<ToolBarSlotController>().Init(i + 1);   
        }
    }
    /// <summary>
    /// 记录当前选定的物品槽(唯一)
    /// </summary>
    private void SelectedSlot(GameObject slot)
    {   
        if (selected != null && selected != slot)
        {
            selected.GetComponent<ToolBarSlotController>().NormalButton();
            selected = null;
        }
        selected = slot;
    }
    public void SelectedSlotByKeyBoard(int index)
    {
        slotList[index].GetComponent<ToolBarSlotController>().ButtonEvent();
    }
}
