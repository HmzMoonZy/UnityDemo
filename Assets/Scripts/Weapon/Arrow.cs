using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Transform m_transform;
    private Rigidbody m_rigidbody;
    private BoxCollider m_boxcollider;

    private Transform pivot;        //摇晃动画支点

    private Ray ray;
    private RaycastHit hit;
    private void Awake()
    {
        m_transform = gameObject.GetComponent<Transform>();
        m_rigidbody = gameObject.GetComponent<Rigidbody>();
        m_boxcollider = gameObject.GetComponent<BoxCollider>();

        pivot = m_transform.Find("Pivot");
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
            StartCoroutine(ShakeAnimation());
        }

    }
    /// <summary>
    /// 摇晃动画
    /// </summary>
    private IEnumerator ShakeAnimation()
    {
        float stopTime = Time.time + 1.0f;

        float range = 1f;
        float vel = 0;
        //Quaternion startRot = Quaternion.Euler(new Vector3(Random.Range(-5.0f, 5.0f), Random.Range(-5.0f, 5.0f), 0));
        while (Time.time <= stopTime)
        {
            //pivot.localRotation = Quaternion.Euler(new Vector3(Random.Range(-range, range),
            //    Random.Range(-range, range), 0)) * startRot;
            pivot.localRotation = Quaternion.Euler(new Vector3(Random.Range(-range, range), 
                Random.Range(-range, range), 0));
            range = Mathf.SmoothDamp(range, 0, ref vel, stopTime - Time.time);
            yield return null;
        }
    }
}