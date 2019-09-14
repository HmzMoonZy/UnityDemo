using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 霰弹枪视图层
/// </summary>
public class ShotgunView : GunViewBase
{
    private AudioClip reLoad;       //拉栓音效
    private Transform shellPos;     //弹壳生成坐标
    private GameObject shell;       //弹壳模型
    private GameObject bullet;      //临时子弹模型
    


    public AudioClip ReLoad { get { return reLoad; } }
    public Transform ShellPos { get { return shellPos; } }
    public GameObject Shell { get { return shell; } }
    public GameObject Bullet { get { return bullet; } }
    public override void Init()
    {
        reLoad = Resources.Load<AudioClip>("Audio/Weapon/Shotgun_Pump");
        shell = Resources.Load<GameObject>("Weapon/Shotgun_Shell");
        bullet = Resources.Load<GameObject>("Weapon/ShotgunBullet");
        shellPos = M_Transform.Find("Armature/Weapon/ShellEffectPoint").GetComponent<Transform>();
    }

    public override void InitAimAnimationPos()
    {
        M_OriginPos = M_Transform.localPosition;
        M_OriginRot = M_Transform.localRotation.eulerAngles;
        M_AimPos = new Vector3(-0.1f, -1.78f, 0.06f);
        M_AimRot = new Vector3(0.47f, -0.02f, 0.8f);
    }

    public override void InitFind()
    {
        M_FireEffect = Resources.Load<GameObject>("Effects/Weapon/Shotgun_GunPoint_Effect");
        M_FireAudioClip = Resources.Load<AudioClip>("Audio/Weapon/Shotgun_Fire");

    }

    public override void SetFireEffectPos()
    {
        M_FireEffectPos = M_Transform.Find("Armature/Weapon/FireEffectPoint").GetComponent<Transform>();

    }
}
