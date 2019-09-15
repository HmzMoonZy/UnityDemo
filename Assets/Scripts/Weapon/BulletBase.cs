using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 子弹抽象父类
/// </summary>
public abstract class BulletBase : MonoBehaviour
{
    private Transform m_transform;
    private Rigidbody m_rigidbody;

    private int demage;

    public Transform M_Transform { get { return m_transform; } }
    public Rigidbody M_Rigidbody { get { return m_rigidbody; } }

    public int M_Demage { get { return demage; } set { demage = value; } }
    private void Awake()
    {
        m_transform = gameObject.GetComponent<Transform>();
        m_rigidbody = gameObject.GetComponent<Rigidbody>();

        Init();
        Invoke("DestroySelf", 3f);
    }
    private void OnCollisionEnter(Collision collision)
    {
        CollisionEnter(collision);
    }
    /// <summary>
    /// 销毁自身
    /// </summary>
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
    /// <summary>
    /// 初始化子类自身变量
    /// </summary>
    public abstract void Init();
    /// <summary>
    /// 子弹飞行
    /// </summary>
    public abstract void Flight(Vector3 dir, float force, int damage);
    /// <summary>
    /// 碰撞方法
    /// </summary>
    public abstract void CollisionEnter(Collision collision);
}
