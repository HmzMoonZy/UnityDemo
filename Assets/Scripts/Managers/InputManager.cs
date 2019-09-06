using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 按键输入管理器
/// </summary>
public class InputManager : MonoBehaviour
{
    private bool inventoryPanelShow = false;    //背包面板显示状态
    void Start()
    {
        InventoryPanelController._Instance.HidePanel();

    }

    void Update()
    {
        SwitchInventoryPanel();
        GetToolBarKey();
    }
    /// <summary>
    /// 切换背包面板显示状态
    /// </summary>
    private void SwitchInventoryPanel()
    {
        if (Input.GetKeyDown(GameConst.InventoryPanelKey))
        {
            if (inventoryPanelShow)
            {
                InventoryPanelController._Instance.HidePanel();
                inventoryPanelShow = false;
            }
            else
            {
                InventoryPanelController._Instance.ShowPanel();
                inventoryPanelShow = true;
            }
        }
    }
    /// <summary>
    /// 检测快捷工具栏快捷键
    /// </summary>
    private void GetToolBarKey()
    {
        ToolBarSelect(GameConst.ToolBarKey1, 0);
        ToolBarSelect(GameConst.ToolBarKey2, 1);
        ToolBarSelect(GameConst.ToolBarKey3, 2);
        ToolBarSelect(GameConst.ToolBarKey4, 3);
        ToolBarSelect(GameConst.ToolBarKey5, 4);
        ToolBarSelect(GameConst.ToolBarKey6, 5);
        ToolBarSelect(GameConst.ToolBarKey7, 6);
        ToolBarSelect(GameConst.ToolBarKey8, 7);
    }
    /// <summary>
    /// 将检测到的快捷键及角标触发相应事件
    /// </summary>
    private void ToolBarSelect(KeyCode key, int index)
    {
        if (Input.GetKeyDown(key))
        {
            ToolBarController._Instance.SelectedSlotByKeyBoard(index);
        }
    }
}
