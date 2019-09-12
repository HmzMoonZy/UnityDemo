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

    //
    private Transform m_fireEffctPos;     //枪口特效坐标
    private Transform m_shellEffctPos;    //弹壳弹出特效坐标
    private Transform m_sightPos;         //准星坐标

    //基础组件属性
    public Transform M_Transform { get { return m_transform; } }
    public Animator M_Animator { get { return m_animator; } }
    public Camera M_EnvCamera { get { return m_envCamera; } }

    //瞄准动画坐标变量属性
    public Vector3 M_OriginPos { get { return m_originPos; } set { m_originPos = value; } }
    public Vector3 M_OriginRot { get { return m_originRot; } set { m_originRot = value; } }
    public Vector3 M_AimPos { get { return m_aimPos; } set { m_aimPos = value; } }
    public Vector3 M_AimRot { get { return m_aimRot; } set { m_aimRot = value; } }

    //
    public Transform M_FireEffectPos { get { return m_fireEffctPos; } set { m_fireEffctPos = value; } }
    public Transform M_ShellEffectPos { get { return m_shellEffctPos; } set { m_shellEffctPos = value; } }
    public Transform M_SightPos { get { return m_sightPos; } set { m_sightPos = value; } }

    public void Awake()
    {
        m_transform = gameObject.GetComponent<Transform>();
        m_animator = gameObject.GetComponent<Animator>();
        m_envCamera = GameObject.Find("EnvCamera").GetComponent<Camera>();

        InitAimAnimationPos();
        InitFind();
    }
    /// <summary>
    /// 瞄准动作
    /// </summary>
    public void AimAction(int fov = 40, float time = 0.2f)
    {
        m_envCamera.DOFieldOfView(fov, time);
        m_transform.DOLocalMove(m_aimPos, time);
        m_transform.DOLocalRotate(m_aimRot, time);
    }
    /// <summary>
    /// 瞄准取消动作
    /// </summary>
    public void CancelAimAction(int fov = 60, float time = 0.2f)
    {
        m_envCamera.DOFieldOfView(fov, time);
        m_transform.DOLocalMove(m_originPos, time);
        m_transform.DOLocalRotate(m_originRot, time);
    }
    /// <summary>
    /// 初始化瞄准动画相关坐标
    /// </summary>
    public abstract void InitAimAnimationPos();
    /// <summary>
    /// 初始化查找组件
    /// </summary>
    public abstract void InitFind();

}
