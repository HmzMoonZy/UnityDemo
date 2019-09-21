using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : BulletBase
{
    private BoxCollider m_boxcollider;
    private Transform pivot;        //摇晃动画支点
    private Ray ray;
    private RaycastHit hit;

    public override void Init()
    {
        m_boxcollider = gameObject.GetComponent<BoxCollider>();
        pivot = M_Transform.Find("Pivot");
    }

    public override void Flight(Vector3 dir, float force, int damage, RaycastHit hit)
    {
        M_Rigidbody.AddForce(dir * force);
        this.M_Demage = damage;
        this.hit = hit;
    }
    public override void CollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Env"))
        {
            M_Rigidbody.Sleep();
            Destroy(M_Rigidbody);
            Destroy(m_boxcollider);
            M_Transform.SetParent(collision.transform);
            StartCoroutine(ShakeAnimation());
            collision.gameObject.GetComponent<BulletMark>().M_HP -= M_Demage;
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            M_Rigidbody.Sleep();
            Destroy(M_Rigidbody);
            Destroy(m_boxcollider);
            M_Transform.SetParent(collision.transform);
            collision.gameObject.GetComponentInParent<EnemyAI>().PlayEffect(hit);
            StartCoroutine(ShakeAnimation());
            if (collision.gameObject.GetComponentInParent<EnemyAI>().M_State != AnimationState.DEATH)
            {
                if (collision.gameObject.name == "collider_head")
                    collision.gameObject.GetComponentInParent<EnemyAI>().GetHitHard(M_Demage * 2);
                else
                    collision.gameObject.GetComponentInParent<EnemyAI>().GetHitNormal(M_Demage);
            }
                
        }
    }
    /// <summary>
    /// 摇晃动画;
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