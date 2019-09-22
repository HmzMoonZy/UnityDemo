using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 霰弹枪子弹脚本
/// </summary>
public class ShotgunBullet : BulletBase
{
    private Ray ray;
    private RaycastHit hit;

    public override void Init() { }

    public override void Flight(Vector3 dir, float force, int damage, RaycastHit hit)
    {
        Vector3 offset = new Vector3(Random.Range(-0.08f, 0.08f), Random.Range(-0.06f, 0.06f), 0);  //散射偏移量
        M_Rigidbody.AddForce((dir + offset) * force);
        ray = new Ray(M_Transform.position, dir + offset);
        this.M_Demage = damage;
    }

    public override void CollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Env"))
        {
            Physics.Raycast(ray, out hit, 1000, 1 << 11);   //Env
            M_Rigidbody.Sleep();
            hit.collider.GetComponent<BulletMark>().CreateBulletMark(hit);
            collision.gameObject.GetComponent<BulletMark>().M_HP -= M_Demage;
            DestroySelf();
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Physics.Raycast(ray, out hit, 1000, 1 << 12);    //Enemy
            M_Rigidbody.Sleep();
            collision.gameObject.GetComponentInParent<EnemyAI>().PlayEffect(hit);
            if (collision.gameObject.GetComponentInParent<EnemyAI>().M_State != ActionState.DEATH)
            {
                if (collision.gameObject.name == "collider_head")
                    collision.gameObject.GetComponentInParent<EnemyAI>().GetHitHard(M_Demage * 2);
                else
                    collision.gameObject.GetComponentInParent<EnemyAI>().GetHitNormal(M_Demage);
            }

            DestroySelf();
        }
    }
}
