using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 武器工厂
/// </summary>
public class WeaponFactory : MonoBehaviour
{
    public static WeaponFactory _Instance;

    private Transform m_transform;

    //枪械预制体
    private GameObject m_assaultRifle;
    private GameObject m_shotgun;
    private GameObject m_woodenBow;
    private GameObject m_woodenSpear;

    private int index = 0;      //枪械index 

    private void Awake()
    {
        _Instance = this;
    }

    private void Start()
    {
        m_transform = gameObject.GetComponent<Transform>();

        LoadWeaponPrefabs();
    }

    /// <summary>
    /// 加载枪械预制体
    /// </summary>
    private void LoadWeaponPrefabs()
    {
        m_assaultRifle = Resources.Load<GameObject>("Weapon/Prefabs/Assault Rifle");
        m_shotgun = Resources.Load<GameObject>("Weapon/Prefabs/Shotgun");
        m_woodenBow = Resources.Load<GameObject>("Weapon/Prefabs/Wooden Bow");
        m_woodenSpear = Resources.Load<GameObject>("Weapon/Prefabs/Wooden Spear");
    }

    /// <summary>
    /// 创建武器预制体
    /// </summary>
    public GameObject CreateWeapon(string fileName, GameObject item)
    {
        GameObject temp = null;
        switch (fileName)
        {
            case "Assault Rifle":
                temp = Instantiate(m_assaultRifle, m_transform);
                SetProperty(temp, index, 80, WeaponType.AssaultRifle, 100, item);
                break;
            case "Shotgun":
                temp = Instantiate(m_shotgun, m_transform);
                SetProperty(temp, index, 350, WeaponType.Shotgun, 30, item);
                break;
            case "Wooden Bow":
                temp = Instantiate(m_woodenBow, m_transform);
                SetProperty(temp, index, 70, WeaponType.AssaultRifle, 50, item);
                break;
            case "Wooden Spear":
                temp = Instantiate(m_woodenSpear, m_transform);
                SetProperty(temp, index, 70, WeaponType.AssaultRifle, 50, item);
                break;
        }
        index++;
        return temp;
    }
    private void SetProperty(GameObject weapon, int id, int damage, WeaponType type, int durable, GameObject item)
    {
        GunControllerBase gcb = weapon.GetComponent<GunControllerBase>();
        gcb.ID = id;
        gcb.M_Damage = damage;
        gcb.Type = type;
        gcb.Durable = durable;
        gcb.Item = item;
    }
}
