using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
/// <summary>
/// 按键输入管理器
/// </summary>
public class InputManager : MonoBehaviour
{
    private bool inventoryPanelShow = false;        //背包面板显示状态

    private GunControllerBase gunctrl;              //枪械控制器
    private FirstPersonController fpsctrl;          //第一人称控制器

    private GameObject Sight_UI;                    //准星UI
    void Start()
    {
        InventoryPanelController._Instance.HidePanel();

        gunctrl = GameObject.Find("FPSController/PlayerCamera/ShotGun").GetComponent<GunControllerBase>();
        fpsctrl = GameObject.Find("FPSController").GetComponent<FirstPersonController>();

        Sight_UI = GameObject.Find("Canvas/MainPanel/Sight");
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
            if (inventoryPanelShow)     //关闭背包
            {
                InventoryPanelController._Instance.HidePanel();
                inventoryPanelShow = false;
                //启用组件
                gunctrl.enabled = true;
                fpsctrl.enabled = true;
                Sight_UI.SetActive(true);
            }
            else                        //打开背包
            {
                InventoryPanelController._Instance.ShowPanel();
                inventoryPanelShow = true;
                //禁用组件
                gunctrl.enabled = false;
                fpsctrl.enabled = false;
                //鼠标解锁
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                //关闭准星
                Sight_UI.SetActive(false);
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
