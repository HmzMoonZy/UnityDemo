using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AssaultRifleView : GunViewBase
{
    private GameObject bullet_Prefab;           //临时子弹模型
    private GameObject shell_Prefab;            //射击弹壳弹出模型
    private Transform m_shellEffctPos;          //弹壳弹出坐标

    //父物体管理
    private Transform allShell_Parent;          //弹壳资源父物体
    private Transform allFireEffect_Parent;     //开火特效父物体

    #region 属性
    public GameObject Bullet_Prefab { get { return bullet_Prefab; } }

    public GameObject Shell_Prefab { get { return shell_Prefab; } }

    public Transform M_ShellEffectPos { get { return m_shellEffctPos; } set { m_shellEffctPos = value; } }


    public Transform AllShell_Parent { get { return allShell_Parent; } set { allShell_Parent = value; } }
    public Transform AllFireEffect_Parent { get { return allFireEffect_Parent; } set { allFireEffect_Parent = value; } }
    #endregion

    protected override void Init()
    {
        bullet_Prefab = Resources.Load<GameObject>("Weapon/Bullet");
        shell_Prefab = Resources.Load<GameObject>("Weapon/Shell");
        M_ShellEffectPos = M_Transform.Find("Assault_Rifle/ShellEffectPoint").GetComponent<Transform>();


        allShell_Parent = GameObject.Find("TempManager/AllShell").GetComponent<Transform>();
        allFireEffect_Parent = GameObject.Find("TempManager/AllFireEffect").GetComponent<Transform>();
    }
    protected override void InitAimAnimationPos()
    {
        M_OriginPos = M_Transform.localPosition;
        M_OriginRot = M_Transform.localRotation.eulerAngles;
        M_AimPos = new Vector3(-0.025f, -1.834f, 0.135f);
        M_AimRot = new Vector3(-0.67f, 1.31f, 1.4f);
    }
    protected override void InitFind()
    {
        M_FireEffect = Resources.Load<GameObject>("Effects/Weapon/AssaultRifle_GunPoint_Effect");
        M_FireAudioClip = Resources.Load<AudioClip>("Audio/Weapon/AssaultRifle_Fire");
    }
    protected override void SetMuzzlePos()
    {
        M_MuzzlePos = M_Transform.Find("Assault_Rifle/FireEffectPoint").GetComponent<Transform>();
    }
}
