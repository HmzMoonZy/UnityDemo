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

    public override void Init()
    {
    }

    public override void Flight(Vector3 dir, float force, int damage)
    {
        //散射偏移量
        Vector3 offset = new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), 0);
        M_Rigidbody.AddForce((dir + offset) * force);

        //生成贴图
        ray = new Ray(M_Transform.position, dir + offset);
        Physics.Raycast(ray, out hit, 1500, 1 << 11);
        if (hit.collider != null && hit.collider.GetComponent<BulletMark>() != null)
        {
            hit.collider.GetComponent<BulletMark>().CreateBulletMark(hit);
        }
        this.M_Demage = damage;
    }

    public override void CollisionEnter(Collision collision)
    {
        //if (collision.gameObject.layer == 11) 
        if (collision.gameObject.layer == LayerMask.NameToLayer("Env"))
        {
            M_Rigidbody.Sleep();
            DestroySelf();

            collision.gameObject.GetComponent<BulletMark>().M_HP -= M_Demage;
        }
    }
}
