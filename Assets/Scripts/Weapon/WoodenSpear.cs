using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenSpear : GunControllerBase
{
    private WoodenSpearView m_woodenSpearView;
    public override void Init()
    {
        m_woodenSpearView = (WoodenSpearView)M_GunViewBase;
    }

    public override void PlayEffect() { }

    public override void Shot()
    {
        GameObject spear = Instantiate(m_woodenSpearView.Spear, m_woodenSpearView.M_FireEffectPos.position
            , m_woodenSpearView.M_FireEffectPos.rotation);
        spear.GetComponent<Arrow>().Flight(m_woodenSpearView.M_FireEffectPos.forward, 5000);
    }
}
