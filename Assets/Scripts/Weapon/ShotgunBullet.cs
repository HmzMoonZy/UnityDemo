using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 霰弹枪子弹脚本
/// </summary>
public class ShotgunBullet : MonoBehaviour
{
    private Transform m_transform;
    private Rigidbody m_rigidbody;
    void Awake()
    {
        m_transform = gameObject.GetComponent<Transform>();
        m_rigidbody = gameObject.GetComponent<Rigidbody>();
    }
    public void Flight(Vector3 dir, float force)
    {
        //散射偏移量
        Vector3 offset = new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), 0);
        m_rigidbody.AddForce((dir + offset) * force);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Bullet")
        {
            m_rigidbody.Sleep();
            Destroy(m_rigidbody);
        }
    }

}
