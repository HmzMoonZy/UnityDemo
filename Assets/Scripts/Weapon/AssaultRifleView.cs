using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AssaultRifleView : GunViewBase
{
    private GameObject bullet_Prefab;   //子弹模型
    private GameObject fireEffct;       //射击枪口特效
    private GameObject shell_Prefab;    //射击弹壳弹出模型
    private AudioClip fire_audioClip;   //射击音效

    private Transform allShell_Parent;          //弹壳资源父物体
    private Transform allFireEffect_Parent;     //开火特效父物体

    #region 属性
    public GameObject Bullet_Prefab { get { return bullet_Prefab; } }
    public GameObject FireEffect { get { return fireEffct; } }
    public GameObject Shell_Prefab { get { return shell_Prefab; } }
    public AudioClip Fire_AudioClip { get { return fire_audioClip; } }

    public Transform AllShell_Parent { get { return allShell_Parent; } set { allShell_Parent = value; } }
    public Transform AllFireEffect_Parent { get { return allFireEffect_Parent; } set { allFireEffect_Parent = value; } }
    #endregion

    private void Awake()
    {
        base.Awake();

        bullet_Prefab = Resources.Load<GameObject>("Weapon/Bullet");
        fireEffct = Resources.Load<GameObject>("Effects/Weapon/AssaultRifle_GunPoint_Effect");
        shell_Prefab = Resources.Load<GameObject>("Weapon/Shell");
        fire_audioClip = Resources.Load<AudioClip>("Audio/Weapon/AssaultRifle_Fire");

        allShell_Parent = GameObject.Find("TempManager/AllShell").GetComponent<Transform>();
        allFireEffect_Parent = GameObject.Find("TempManager/AllFireEffect").GetComponent<Transform>();
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
        M_FireEffectPos = GameObject.Find("FireEffectPoint").GetComponent<Transform>();
        M_ShellEffectPos = GameObject.Find("ShellEffectPoint").GetComponent<Transform>();
        M_SightPos = GameObject.Find("Canvas/MainPanel/Sight").GetComponent<Transform>();
    }
}
