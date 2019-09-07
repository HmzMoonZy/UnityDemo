using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AssaultRifle : MonoBehaviour
{
    private Transform _transform;
    private Animator _animator;

    private Vector3 originPosition;     //初始坐标
    private Vector3 originRotate;       //初始旋转
    private Vector3 aimPosition;        //瞄准坐标
    private Vector3 aimRotate;          //瞄准旋转

    private Camera env_Camera;          //环境摄像机

    void Start()
    {
        _transform = gameObject.GetComponent<Transform>();
        _animator = gameObject.GetComponent<Animator>();

        originPosition = _transform.localPosition;
        originRotate = _transform.localRotation.eulerAngles;
        aimPosition = new Vector3(-0.025f, -1.834f, 0.135f);
        aimRotate = new Vector3(-0.67f, 1.31f, 1.4f);

        env_Camera = GameObject.Find("EnvCamera").GetComponent<Camera>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))    //腰射
        {
            _animator.SetTrigger("Fire");
        }
        if (Input.GetMouseButton(1))        //开镜
        {
            _animator.SetBool("HoldPose", true);
            AimAction();
        }
        if (Input.GetMouseButtonUp(1))
        {
            _animator.SetBool("HoldPose", false);   //取消开镜
            CancelAimAction();
        }
    }
    private void AimAction()
    {
        env_Camera.DOFieldOfView(40, 0.2f);
        _transform.DOLocalMove(aimPosition, 0.2f);
        _transform.DOLocalRotate(aimRotate,0.2f);
    }
    private void CancelAimAction()
    {
        env_Camera.DOFieldOfView(60, 0.2f);
        _transform.DOLocalMove(originPosition, 0.2f);
        _transform.DOLocalRotate(originRotate, 0.2f);
    }
}
