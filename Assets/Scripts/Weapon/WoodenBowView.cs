using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenBowView : GunViewBase
{
    private GameObject arrow;       //箭模型

    public GameObject Arrow { get { return arrow; } }
    public override void Init()
    {
        arrow = Resources.Load<GameObject>("Weapon/Arrow");
    }

    public override void InitAimAnimationPos()
    {
        M_OriginPos = M_Transform.localPosition;
        M_OriginRot = M_Transform.localRotation.eulerAngles;
        M_AimPos = new Vector3(0.7f, -1.37f, 0.23f);
        M_AimRot = new Vector3(-2.26f, 5.14f, 27.67f);
    }

    public override void InitFind()
    {
        M_FireAudioClip = Resources.Load<AudioClip>("Audio/Weapon/Arrow Release");
    }

    public override void SetFireEffectPos()
    {
        M_FireEffectPos = M_Transform.Find("Armature/Arm_L/Forearm_L/Wrist_L/Weapon/FireEffectPoint");
    }
    
}
