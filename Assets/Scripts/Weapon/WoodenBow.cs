using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenBow : GunControllerBase
{
    private WoodenBowView m_woodenBowView;
    protected override void Init()
    {
        m_woodenBowView = (WoodenBowView)M_GunViewBase;
    }
    protected override void Shot()
    {
        GameObject arrow = Instantiate(m_woodenBowView.Arrow, m_woodenBowView.M_MuzzlePos.position
            , m_woodenBowView.M_MuzzlePos.rotation);
        arrow.GetComponent<Arrow>().Flight(m_woodenBowView.M_MuzzlePos.forward, 3000, Damage);

        Durable--;
    }
}
