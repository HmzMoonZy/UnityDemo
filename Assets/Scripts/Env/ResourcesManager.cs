using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 场景资源管理器;
/// </summary>
public abstract class ResourcesManager : MonoBehaviour
{
    //自身组件;
    private Transform m_transform;          //自身transform组件
    private Transform m_parent;             //预制体父物体

    private Transform[] m_points;


    //属性
    public Transform M_Transform { get { return m_transform; } set { m_transform = value; } }
    public Transform M_Parent { get { return m_parent; } set { m_parent = value; } }
    public Transform[] M_Points { get { return m_points; } set { m_points = value; } }

    void Start()
    {
        m_transform = gameObject.GetComponent<Transform>();

        FindPrefab();
        SetParent();
        SetPoints();
        CreateAllStone();
    }

    /// <summary>
    /// 查找需要用到的预制体
    /// </summary>
    protected abstract void FindPrefab();
    /// <summary>
    /// 设置父物体
    /// </summary>
    protected abstract void SetParent();
    /// <summary>
    /// 设置生成坐标点
    /// </summary>
    protected abstract void SetPoints();
    /// <summary>
    /// 在指定位置生成随机石头
    /// </summary>
    protected abstract void CreateAllStone();

}
