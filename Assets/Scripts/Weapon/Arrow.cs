using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Transform m_transform;
    private Rigidbody m_rigidbody;
    private BoxCollider m_boxcollider;

    private Ray ray;
    private RaycastHit hit;
    private void Awake()
    {
        m_transform = gameObject.GetComponent<Transform>();
        m_rigidbody = gameObject.GetComponent<Rigidbody>();
        m_boxcollider = gameObject.GetComponent<BoxCollider>();
    }
    public void Flight(Vector3 dir, float force)
    {
        m_rigidbody.AddForce(dir * force);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Env"))
        {
            m_rigidbody.Sleep();
            Destroy(m_rigidbody);
            Destroy(m_boxcollider);
            m_transform.SetParent(collision.transform);//设置父物体,跟随效果
        }

    }
}
