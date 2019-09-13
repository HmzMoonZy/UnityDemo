using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 霰弹枪控制层
/// </summary>
public class Shotgun : GunControllerBase
{
    private ShotgunView m_GunView;      //视图层引用
    public override void Init()
    {
        m_GunView = (ShotgunView)M_GunViewBase;
    }

    public override void PlayEffect()
    {
        GameObject temp = Instantiate(m_GunView.M_FireEffect, m_GunView.M_FireEffectPos);
        temp.GetComponent<ParticleSystem>().Play();
        StartCoroutine(DelayDestroy(temp, 2f));
    }

    public override void Shoot()
    {

    }
    /// <summary>
    /// 延迟销毁目标
    /// </summary>
    private IEnumerator DelayDestroy(GameObject go, float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(go);
    }
    /// <summary>
    /// 拉栓音效
    /// </summary>
    public void ReLoadAudio()
    {
        AudioSource.PlayClipAtPoint(m_GunView.ReLoad, m_GunView.M_FireEffectPos.position);
    }

}
