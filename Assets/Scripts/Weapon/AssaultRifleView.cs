using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AssaultRifleView : MonoBehaviour
{
    //自身基本组件
    private Transform _transform;
    private Animator _animator;

    //动画相关变量
    private Vector3 originPosition;     //初始坐标
    private Vector3 originRotate;       //初始旋转
    private Vector3 aimPosition;        //瞄准坐标
    private Vector3 aimRotate;          //瞄准旋转

    private Camera env_Camera;          //环境摄像机

    private Transform fireEffctPos;     //开火特效坐标
    private Transform shellEffctPos;    //弹壳弹出特效坐标
    private Transform sight_Transform;  //准星坐标

    private GameObject bullet_Prefab;   //子弹模型
    private GameObject fireEffct;       //射击枪口特效
    private GameObject shell_Prefab;    //射击弹壳弹出模型
    private AudioClip fire_audioClip;

    #region 属性
    public Transform _Transform { get { return _transform; } }
    public Animator _Animator { get { return _animator; } }

    public Camera Env_Camera { get { return env_Camera; } }

    public Transform FireEffectPos { get { return fireEffctPos; } }
    public Transform ShellEffctPos { get { return shellEffctPos; } }
    public Transform Sight_Transform { get { return sight_Transform; } }

    public GameObject Bullet_Prefab { get { return bullet_Prefab; } }
    public GameObject FireEffct { get { return fireEffct; } }
    public GameObject Shell_Prefab { get { return shell_Prefab; } }
    public AudioClip Fire_AudioClip { get { return fire_audioClip; } }
    
    #endregion

    void Awake()
    {
        _transform = gameObject.GetComponent<Transform>();
        _animator = gameObject.GetComponent<Animator>();

        originPosition = _transform.localPosition;
        originRotate = _transform.localRotation.eulerAngles;
        aimPosition = new Vector3(-0.025f, -1.834f, 0.135f);
        aimRotate = new Vector3(-0.67f, 1.31f, 1.4f);

        env_Camera = GameObject.Find("EnvCamera").GetComponent<Camera>();

        fireEffctPos = GameObject.Find("FireEffectPoint").GetComponent<Transform>();
        shellEffctPos = GameObject.Find("ShellEffectPoint").GetComponent<Transform>();
        sight_Transform = GameObject.Find("Canvas/MainPanel/Sight").GetComponent<Transform>();

        bullet_Prefab = Resources.Load<GameObject>("Weapon/Bullet");
        fireEffct = Resources.Load<GameObject>("Effects/Weapon/AssaultRifle_GunPoint_Effect");
        shell_Prefab = Resources.Load<GameObject>("Weapon/Shell");
        fire_audioClip = Resources.Load<AudioClip>("Audio/Weapon/AssaultRifle_Fire");

    }
    //瞄准动画
    public void AimAction()
    {
        env_Camera.DOFieldOfView(40, 0.2f);
        _transform.DOLocalMove(aimPosition, 0.2f);
        _transform.DOLocalRotate(aimRotate, 0.2f);
    }
    public void CancelAimAction()
    {
        env_Camera.DOFieldOfView(60, 0.2f);
        _transform.DOLocalMove(originPosition, 0.2f);
        _transform.DOLocalRotate(originRotate, 0.2f);
    }
}
