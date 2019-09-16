using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolBarController : MonoBehaviour
{
    public static ToolBarController _Instance;

    private ToolBarModel m_toolBarModel;
    private ToolBarView m_toolBarView;

    private GameObject m_selected = null;           //当前选定的物品槽.
    private GameObject m_currentWeapon = null;      //当前选定的武器.

    private List<GameObject> m_slotList;
    private Dictionary<GameObject, GameObject> m_weaponDic;

    public GameObject M_currentWeapon { get { return m_currentWeapon; } }

    private void Awake()
    {
        _Instance = this;
    }
    void Start()
    {
        m_toolBarModel = gameObject.GetComponent<ToolBarModel>();
        m_toolBarView = gameObject.GetComponent<ToolBarView>();

        m_slotList = new List<GameObject>();
        m_weaponDic = new Dictionary<GameObject, GameObject>();

        CreateAllSlot();
    }
    /// <summary>
    /// 生成所有物品槽.
    /// </summary>
    private void CreateAllSlot()
    {
        for (int i = 0; i < 8; i++)
        {
            GameObject slot = Instantiate<GameObject>(m_toolBarView.ToolBarSlot_Prefab, m_toolBarView.BG_Transform);
            m_slotList.Add(slot);
            slot.name = "ToolBarSlot";
            slot.GetComponent<ToolBarSlotController>().Init(i + 1);
        }
    }
    /// <summary>
    /// 选定物品槽键盘操作.
    /// </summary>
    public void SelectedSlotByKeyBoard(int index)
    {
        m_slotList[index].GetComponent<ToolBarSlotController>().ButtonEvent();
    }
    /// <summary>
    /// 激活物品槽.
    /// </summary>
    private void SelectedSlot(GameObject slot)
    {
        //记录当前选定的物品槽.
        if (m_selected != null && m_selected != slot)
        {
            m_selected.GetComponent<ToolBarSlotController>().NormalButton();
            m_selected = null;
        }
        m_selected = slot;

        InstaniateSelected();
    }
    /// <summary>
    /// 实例化选中物品槽的物品.
    /// </summary>
    public void InstaniateSelected()
    {
        Transform item = m_selected.GetComponent<Transform>().Find("InventoryItem");
        StartCoroutine(CallWeaponFactory(item));   
    }
    /// <summary>
    /// 调用工厂,实例化激活物品槽中的物体.
    /// </summary>
    private IEnumerator CallWeaponFactory(Transform item)
    {
        //若该物品槽中有物品.
        if (item != null)
        {
            //当前若持有武器则隐藏.
            if (m_currentWeapon != null)
            {
                m_currentWeapon.GetComponent<GunControllerBase>().PlayHoslst();
                yield return new WaitForSeconds(0.5f);
                m_currentWeapon.SetActive(false);
            }

            //查询字典.
            GameObject temp = null;
            m_weaponDic.TryGetValue(item.gameObject, out temp);

            //若未持有过(字典未找到).
            if (temp == null)
            {
                temp = WeaponFactory._Instance.CreateWeapon(item.GetComponent<Image>().sprite.name, item.gameObject);
                m_weaponDic.Add(item.gameObject, temp);
                m_currentWeapon = temp;
            }

            //持有过.
            else
            {
                //若按钮已被激活(按纽激活时间先于此方法执行).
                if (m_selected.GetComponent<ToolBarSlotController>().ActiveState)
                {
                    temp.SetActive(true);
                    m_currentWeapon = temp;
                }
                else m_currentWeapon = null;
            }
        }
        else Debug.Log("该快捷栏没有设置物品!");
    }
}
