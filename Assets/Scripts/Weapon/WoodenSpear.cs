using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenSpear : GunControllerBase
{
    private WoodenSpearView m_woodenSpearView;
    protected override void Init()
    {
        m_woodenSpearView = (WoodenSpearView)M_GunViewBase;
    }
    protected override void Shot()
    {
        GameObject spear = Instantiate(m_woodenSpearView.Spear, m_woodenSpearView.M_MuzzlePos.position
            , m_woodenSpearView.M_MuzzlePos.rotation);
        spear.GetComponent<Arrow>().Flight(m_woodenSpearView.M_MuzzlePos.forward, 5000, M_Damage);

        Durable--;
    }
}
