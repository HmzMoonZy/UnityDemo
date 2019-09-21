using System.Collections;
using UnityEngine;
/// <summary>
/// 步枪脚本
/// </summary>
public class AssaultRifle : LikeGunControllerBase
{
    private AssaultRifleView m_AssaultRifleView; //视图层引用
    private ObjectPool[] pools;         //对象池数组.0:枪口特效.1:弹壳模型

    protected override void Init()
    {
        m_AssaultRifleView = (AssaultRifleView)M_GunViewBase;
        pools = gameObject.GetComponents<ObjectPool>();
    }
    /// <summary>
    /// 射击
    /// </summary>
    protected override void Shot()
    {
        if (Hit.point != Vector3.zero)
        {
            if (Hit.collider.GetComponent<BulletMark>() != null)
            {
                Hit.collider.GetComponent<BulletMark>().CreateBulletMark(Hit);
                Hit.collider.GetComponent<BulletMark>().M_HP -= M_Damage;
            }
            if (Hit.collider.GetComponentInParent<EnemyAI>() != null
                && Hit.collider.GetComponentInParent<EnemyAI>().M_State != AnimationState.DEATH)
            {
                GameObject temp = Instantiate(m_AssaultRifleView.Bullet_Prefab, Hit.point, Quaternion.identity);
                temp.GetComponent<Transform>().SetParent(Hit.collider.gameObject.GetComponent<Transform>());
                Hit.collider.GetComponentInParent<EnemyAI>().M_HP -= M_Damage;
            }
            if (Hit.collider.GetComponent<BulletMark>() == null && Hit.collider.GetComponentInParent<EnemyAI>() == null)
            {
                Instantiate(m_AssaultRifleView.Bullet_Prefab, Hit.point, Quaternion.identity);
            }



        }
        Durable--;
    }
    /// <summary>
    /// 播放特效
    /// </summary>
    protected override void PlayEffect()
    {
        FireEffect();
        ShellAction();
    }
    /// <summary>
    /// 枪口火焰特效
    /// </summary>
    private void FireEffect()
    {
        GameObject effect = null;
        if (pools[0].IsTemp())      //池空则实例化
        {
            effect = Instantiate(m_AssaultRifleView.M_FireEffect, m_AssaultRifleView.M_MuzzlePos.position
                , Quaternion.identity, m_AssaultRifleView.AllFireEffect_Parent);
        }
        else        //重置池中对象
        {
            effect = pools[0].GetObject();
            effect.SetActive(true);
            effect.GetComponent<Transform>().position = m_AssaultRifleView.M_MuzzlePos.position;
        }
        effect.name = "FireEffect";
        effect.GetComponent<ParticleSystem>().Play();
        //延迟入池
        StartCoroutine(DelayIntoPool(effect, pools[0]));
    }
    /// <summary>
    /// 弹壳出仓动画
    /// </summary>
    private void ShellAction()
    {
        GameObject tempShell = null;

        if (pools[1].IsTemp())      //池空则实例化
        {
            tempShell = Instantiate(m_AssaultRifleView.Shell_Prefab, m_AssaultRifleView.M_ShellEffectPos.position
                , Quaternion.identity, m_AssaultRifleView.AllShell_Parent);
        }
        else        //重置池中对象
        {
            tempShell = pools[1].GetObject();
            tempShell.SetActive(true);
            tempShell.GetComponent<Rigidbody>().isKinematic = true;     //力学模拟,开启后正常修改position.
            tempShell.GetComponent<Transform>().position = m_AssaultRifleView.M_ShellEffectPos.position;
            tempShell.GetComponent<Rigidbody>().isKinematic = false;
        }
        //施加力产生弹出效果
        tempShell.GetComponent<Rigidbody>().AddForce(m_AssaultRifleView.M_ShellEffectPos.up * Random.Range(45f, 60f));
        //延迟入池
        StartCoroutine(DelayIntoPool(tempShell, pools[1]));
    }

}
