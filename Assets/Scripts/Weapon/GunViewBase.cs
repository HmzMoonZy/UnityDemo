using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// 枪械视图层父类
/// </summary>
public abstract class GunViewBase : MonoBehaviour
{
    //基础组件
    private Transform m_transform;
    private Animator m_animator;
    private Camera m_envCamera;

    //瞄准动画坐标变量
    private Vector3 m_originPos;        //初始坐标
    private Vector3 m_originRot;        //初始旋转
    private Vector3 m_aimPos;           //瞄准坐标
    private Vector3 m_aimRot;           //瞄准旋转

    //特效
    private Transform m_muzzlePos;     //枪口特效坐标

    //音效及特效资源
    private GameObject m_fireEffct;      //射击枪口特效
    private AudioClip m_fireAudioClip;   //射击音效

    //准星
    private Transform m_sightPos;       //准星坐标
    private GameObject m_prefab_sight;  //准星预制体


    #region 属性
    //基础组件属性
    public Transform M_Transform { get { return m_transform; } }
    public Animator M_Animator { get { return m_animator; } }
    public Camera M_EnvCamera { get { return m_envCamera; } }

    //瞄准动画坐标变量属性
    public Vector3 M_OriginPos { get { return m_originPos; } set { m_originPos = value; } }
    public Vector3 M_OriginRot { get { return m_originRot; } set { m_originRot = value; } }
    public Vector3 M_AimPos { get { return m_aimPos; } set { m_aimPos = value; } }
    public Vector3 M_AimRot { get { return m_aimRot; } set { m_aimRot = value; } }

    //特效/准星坐标属性
    public Transform M_MuzzlePos { get { return m_muzzlePos; } set { m_muzzlePos = value; } }
    public Transform M_SightPos { get { return m_sightPos; } set { m_sightPos = value; } }

    //特效及音效
    public GameObject M_FireEffect { get { return m_fireEffct; } set { m_fireEffct = value; } }
    public AudioClip M_FireAudioClip { get { return m_fireAudioClip; } set { m_fireAudioClip = value; } }

    #endregion
    public virtual void Awake()
    {
        m_transform = gameObject.GetComponent<Transform>();
        m_animator = gameObject.GetComponent<Animator>();
        m_envCamera = GameObject.Find("EnvCamera").GetComponent<Camera>();
        m_prefab_sight = Instantiate(Resources.Load<GameObject>("Weapon/Sight")
            , GameObject.Find("MainPanel").transform);
        m_sightPos = m_prefab_sight.GetComponent<Transform>();


        InitAimAnimationPos();
        InitFind();
        SetMuzzlePos();
        Init();
    }
    private void OnEnable()
    {
        ActiveSight();
    }
    private void OnDisable()
    {
        if(m_prefab_sight != null)
            HideSight();
    }
    /// <summary>
    /// 瞄准动作
    /// </summary>
    public virtual void AimAction(int fov = 40, float time = 0.2f)
    {
        m_envCamera.DOFieldOfView(fov, time);
        m_transform.DOLocalMove(m_aimPos, time);
        m_transform.DOLocalRotate(m_aimRot, time);
    }
    /// <summary>
    /// 瞄准取消动作
    /// </summary>
    public virtual void CancelAimAction(int fov = 60, float time = 0.2f)
    {
        m_envCamera.DOFieldOfView(fov, time);
        m_transform.DOLocalMove(m_originPos, time);
        m_transform.DOLocalRotate(m_originRot, time);
    }
    /// <summary>
    /// 隐藏准星
    /// </summary>
    private void HideSight()
    {
        m_prefab_sight.SetActive(false);
    }
    /// <summary>
    /// 显示准星
    /// </summary>
    private void ActiveSight()
    {
        m_prefab_sight.SetActive(true);
    }
    /// <summary>
    /// 初始化子类自身变量
    /// </summary>
    protected abstract void Init();
    /// <summary>
    /// 初始化瞄准动画相关坐标
    /// </summary>
    protected abstract void InitAimAnimationPos();
    /// <summary>
    /// 设置枪口坐标
    /// </summary>
    protected abstract void SetMuzzlePos();
    /// <summary>
    /// 初始化查找特效文件.音效文件等
    /// </summary>
    protected abstract void InitFind();


}
