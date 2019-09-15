using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenSpearView : GunViewBase
{
    private GameObject spear;       //长矛模型

    public GameObject Spear { get { return spear; } }
    public override void Init()
    {
        spear = Resources.Load<GameObject>("Weapon/Wooden_Spear");
    }

    public override void InitAimAnimationPos()
    {
        M_OriginPos = M_Transform.localPosition;
        M_OriginRot = M_Transform.localRotation.eulerAngles;
        M_AimPos = new Vector3(0.002f, -1.545f, 0.232f);
        M_AimRot = new Vector3(0, 0, 0);
    }

    public override void InitFind()
    {
        M_FireAudioClip = Resources.Load<AudioClip>("Audio/Weapon/Arrow Release");
    }

    public override void SetFireEffectPos()
    {
        M_FireEffectPos = M_Transform.Find("Armature/Arm_R/Forearm_R/Wrist_R/Weapon/FireEffectPoint");
    }
}
