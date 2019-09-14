using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenBow : GunControllerBase
{
    private WoodenBowView m_woodenBowView;
    public override void Init()
    {
        m_woodenBowView = (WoodenBowView)M_GunViewBase;
    }

    public override void PlayEffect(){}

    public override void Shot()
    {
        GameObject arrow = Instantiate(m_woodenBowView.Arrow, m_woodenBowView.M_FireEffectPos.position
            , m_woodenBowView.M_FireEffectPos.rotation);
        arrow.GetComponent<Arrow>().Flight(m_woodenBowView.M_FireEffectPos.forward, 3000);
    }
}
