using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenBowView : GunViewBase
{
    private GameObject arrow;       //箭模型

    public GameObject Arrow { get { return arrow; } }
    protected override void Init()
    {
        arrow = Resources.Load<GameObject>("Weapon/Arrow");
    }

    protected override void InitAimAnimationPos()
    {
        M_OriginPos = M_Transform.localPosition;
        M_OriginRot = M_Transform.localRotation.eulerAngles;
        M_AimPos = new Vector3(0.7f, -1.37f, 0.23f);
        M_AimRot = new Vector3(-2.26f, 5.14f, 27.67f);
    }

    protected override void InitFind()
    {
        M_FireAudioClip = Resources.Load<AudioClip>("Audio/Weapon/Arrow Release");
    }

    protected override void SetMuzzlePos()
    {
        M_MuzzlePos = M_Transform.Find("Armature/Arm_L/Forearm_L/Wrist_L/Weapon/FireEffectPoint");
    }
    
}
