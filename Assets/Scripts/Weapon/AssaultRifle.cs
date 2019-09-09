using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AssaultRifle : MonoBehaviour
{
    private AssaultRifleView _assaultRifleView;

    private Ray ray;        //射击射线
    private RaycastHit hit; //射线碰撞点


    //武器类字段属性
    private int id;
    private int damage;
    private int durable;
    private WeaponType type;

    #region 属性
    public int ID { get { return id; } set { id = value; } }
    public int Damege { get { return damage; } set { damage = value; } }
    public int Durable { get { return durable; } set { durable = value; } }
    public WeaponType Type { get { return type; } set { type = value; } }
    #endregion
    void Start()
    {
        Init();
    }
    void Update()
    {
        ShootDetection();
        MouseCtrl();
    }

    private void Init()
    {
        _assaultRifleView = gameObject.GetComponent<AssaultRifleView>();
    }
    /// <summary>
    /// 射击检测
    /// </summary>
    private void ShootDetection()
    {
        //Debug.DrawRay(_assaultRifleView.FireEffectPos.position, _assaultRifleView.FireEffectPos.forward * 300, Color.red);
        ray = new Ray(_assaultRifleView.FireEffectPos.position, _assaultRifleView.FireEffectPos.forward * 300);
        if (Physics.Raycast(ray, out hit))
        {
            //准星定位(辅助瞄准?)
            Vector2 sightPos = RectTransformUtility.WorldToScreenPoint(_assaultRifleView.Env_Camera, hit.point);
            _assaultRifleView.Sight_Transform.position = sightPos;
        }
        else
        {
            hit.point = Vector3.zero;
        }
    }
    /// <summary>
    /// 鼠标控制
    /// </summary>
    private void MouseCtrl()
    {
        //左键 -> 开火
        if (Input.GetMouseButtonDown(0))
        {
            _assaultRifleView._Animator.SetTrigger("Fire");
            PlayAudio();
            PlayEffect();
            Shoot();
        }
        //按住右键 -> 瞄准
        if (Input.GetMouseButton(1))
        {
            _assaultRifleView._Animator.SetBool("HoldPose", true);
            _assaultRifleView.AimAction();
            _assaultRifleView.Sight_Transform.gameObject.SetActive(false);  //准星显示
        }
        //松开右键 -> 取消瞄准
        if (Input.GetMouseButtonUp(1))
        {
            _assaultRifleView._Animator.SetBool("HoldPose", false);
            _assaultRifleView.CancelAimAction();
            _assaultRifleView.Sight_Transform.gameObject.SetActive(true);   //准星隐藏
        }
    }
    /// <summary>
    /// 射击
    /// </summary>
    private void Shoot()
    {
        if (hit.point != Vector3.zero)
        {
            //Instantiate<GameObject>(_assaultRifleView.Bullet_Prefab, hit.point, Quaternion.identity);
            hit.collider.GetComponent<BulletMark>().CreateBulletMark(hit);
        }


    }
    /// <summary>
    /// 音效播放
    /// </summary>
    private void PlayAudio()
    {
        //在某点播放音源片段
        AudioSource.PlayClipAtPoint(_assaultRifleView.Fire_AudioClip, _assaultRifleView.FireEffectPos.position);
    }
    /// <summary>
    /// 特效播放
    /// </summary>
    private void PlayEffect()
    {
        //枪口特效
        Instantiate(_assaultRifleView.FireEffct, _assaultRifleView.FireEffectPos.position
            , Quaternion.identity).GetComponent<ParticleSystem>().Play();
        //弹壳出仓动画
        GameObject shell = Instantiate(_assaultRifleView.Shell_Prefab, _assaultRifleView.ShellEffctPos.position
             , Quaternion.identity);
        shell.GetComponent<Rigidbody>().AddForce(_assaultRifleView.ShellEffctPos.up * Random.Range(45f, 60f));
    }
}
