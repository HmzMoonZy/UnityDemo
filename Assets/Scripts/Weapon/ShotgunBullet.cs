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

    private Ray ray;
    private RaycastHit hit;

    public RaycastHit Hit { get { return hit; } }

    void Awake()
    {
        m_transform = gameObject.GetComponent<Transform>();
        m_rigidbody = gameObject.GetComponent<Rigidbody>();

        Invoke("DestroySelf", 3f);
    }
    /// <summary>
    /// 弹头射击方法
    /// </summary>
    /// <param name="dir"></param>
    /// <param name="force"></param>
    public void Flight(Vector3 dir, float force)
    {
        //散射偏移量
        Vector3 offset = new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), 0);
        m_rigidbody.AddForce((dir + offset) * force);

        //生成贴图
        ray = new Ray(m_transform.position, dir + offset);
        Physics.Raycast(ray, out hit, 1500, 1 << 11);
        if (hit.collider != null && hit.collider.GetComponent<BulletMark>() != null)
        {
            hit.collider.GetComponent<BulletMark>().CreateBulletMark(hit);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.layer == 11) 
        if(collision.gameObject.layer == LayerMask.NameToLayer("Env"))
        {
            m_rigidbody.Sleep();
            DestroySelf();
        }

    }
    private void DestroySelf()
    {
        Destroy(gameObject);
    }

}
