using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
/// <summary>
/// 按键输入管理器
/// </summary>
public class InputManager : MonoBehaviour
{
    public static InputManager _Instance;

    private bool m_inventoryPanelState = false;    //背包面板显示状态true:显示 fasle:隐藏

    private FirstPersonController m_fpsctrl;       //角色控制器


    private void Awake()
    {
        _Instance = this;
    }
    void Start()
    {
        InventoryPanelController._Instance.HidePanel();

        m_fpsctrl = GameObject.Find("FPSController").GetComponent<FirstPersonController>();
    }

    void Update()
    {
        SwitchInventoryPanel();
        //仅背包隐藏时接受快捷栏按键
        if (!m_inventoryPanelState)
        {
            GetToolBarKey();
        }



    }
    /// <summary>
    /// 切换背包面板显示状态
    /// </summary>
    private void SwitchInventoryPanel()
    {
        if (Input.GetKeyDown(GameConst.InventoryPanelKey))
        {
            //关闭背包.
            if (m_inventoryPanelState)
            {
                InventoryPanelController._Instance.HidePanel();
                m_inventoryPanelState = false;

                //显示角色及控制器
                m_fpsctrl.enabled = true;
                if (ToolBarController._Instance.M_currentWeapon != null)
                    ToolBarController._Instance.M_currentWeapon.gameObject.SetActive(true);
            }
            //打开背包.
            else
            {
                InventoryPanelController._Instance.ShowPanel();
                m_inventoryPanelState = true;
                //隐藏角色及控制器
                m_fpsctrl.enabled = false;
                if (ToolBarController._Instance.M_currentWeapon != null)
                    ToolBarController._Instance.M_currentWeapon.gameObject.SetActive(false);

                //鼠标解锁
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

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
