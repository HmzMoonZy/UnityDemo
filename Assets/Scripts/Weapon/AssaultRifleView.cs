using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AssaultRifleView : GunViewBase
{
    private GameObject bullet_Prefab;           //临时子弹模型
    private GameObject shell_Prefab;            //射击弹壳弹出模型
    private Transform m_shellEffctPos;          //弹壳弹出坐标

    #region 属性
    public GameObject Bullet_Prefab { get { return bullet_Prefab; } }

    public GameObject Shell_Prefab { get { return shell_Prefab; } }

    public Transform M_ShellEffectPos { get { return m_shellEffctPos; } set { m_shellEffctPos = value; } }
    #endregion

    public override void Init()
    {
        bullet_Prefab = Resources.Load<GameObject>("Weapon/Bullet");
        shell_Prefab = Resources.Load<GameObject>("Weapon/Shell");
        M_ShellEffectPos = M_Transform.Find("Assault_Rifle/ShellEffectPoint").GetComponent<Transform>();
    }
    public override void InitAimAnimationPos()
    {
        M_OriginPos = M_Transform.localPosition;
        M_OriginRot = M_Transform.localRotation.eulerAngles;
        M_AimPos = new Vector3(-0.025f, -1.834f, 0.135f);
        M_AimRot = new Vector3(-0.67f, 1.31f, 1.4f);
    }
    public override void InitFind()
    {
        M_FireEffect = Resources.Load<GameObject>("Effects/Weapon/AssaultRifle_GunPoint_Effect");
        M_FireAudioClip = Resources.Load<AudioClip>("Audio/Weapon/AssaultRifle_Fire");
    }
    public override void SetFireEffectPos()
    {
        M_FireEffectPos = M_Transform.Find("Assault_Rifle/FireEffectPoint").GetComponent<Transform>();
    }
}
