using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AssaultRifle : MonoBehaviour
{
    private AssaultRifleView _assaultRifleView;

    private Ray ray;        //射击射线
    private RaycastHit hit; //射线碰撞点

    private ObjectPool[] pools;  //对象池数组.0:枪口特效.1:弹壳模型

    //武器类字段
    [SerializeField] private int id;
    [SerializeField] private int damage;
    [SerializeField] private int durable;
    [SerializeField] private WeaponType type;

    #region 属性
    public int ID { get { return id; } set { id = value; } }
    public int Damege { get { return damage; } set { damage = value; } }
    public int Durable
    {
        get { return durable; }
        set
        {
            durable = value;
            if (durable <= 0)
            {
                Destroy(gameObject);
                Destroy(_assaultRifleView.M_SightPos.gameObject);
            }
        }
    }
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
        pools = gameObject.GetComponents<ObjectPool>();
    }
    /// <summary>
    /// 射击检测
    /// </summary>
    private void ShootDetection()
    {
        //Debug.DrawRay(_assaultRifleView.FireEffectPos.position, _assaultRifleView.FireEffectPos.forward * 300, Color.red);
        ray = new Ray(_assaultRifleView.M_FireEffectPos.position, _assaultRifleView.M_FireEffectPos.forward * 300);
        if (Physics.Raycast(ray, out hit))
        {
            //准星定位(辅助瞄准?)
            Vector2 sightPos = RectTransformUtility.WorldToScreenPoint(_assaultRifleView.M_EnvCamera, hit.point);
            _assaultRifleView.M_SightPos.position = sightPos;
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
            _assaultRifleView.M_Animator.SetTrigger("Fire");
            PlayAudio();
            PlayEffect();
            Shoot();
        }
        //按住右键 -> 瞄准
        if (Input.GetMouseButton(1))
        {
            _assaultRifleView.M_Animator.SetBool("HoldPose", true);
            _assaultRifleView.AimAction();
            _assaultRifleView.M_SightPos.gameObject.SetActive(false);  //准星显示
        }
        //松开右键 -> 取消瞄准
        if (Input.GetMouseButtonUp(1))
        {
            _assaultRifleView.M_Animator.SetBool("HoldPose", false);
            _assaultRifleView.CancelAimAction();
            _assaultRifleView.M_SightPos.gameObject.SetActive(true);   //准星隐藏
        }
    }
    /// <summary>
    /// 射击
    /// </summary>
    private void Shoot()
    {
        if (hit.point != Vector3.zero)
        {
            if (hit.collider.GetComponent<BulletMark>() != null)
            {
                hit.collider.GetComponent<BulletMark>().CreateBulletMark(hit);
                hit.collider.GetComponent<BulletMark>().HP -= damage;
            }
            else
            {
                Instantiate<GameObject>(_assaultRifleView.Bullet_Prefab, hit.point, Quaternion.identity);
            }
        }
        Durable--;
    }
    /// <summary>
    /// 音效播放
    /// </summary>
    private void PlayAudio()
    {
        //在某点播放音源片段
        AudioSource.PlayClipAtPoint(_assaultRifleView.Fire_AudioClip, _assaultRifleView.M_FireEffectPos.position);
    }
    /// <summary>
    /// 特效播放
    /// </summary>
    private void PlayEffect()
    {
        GameObject effect = null;
        GameObject shell = null;
        //枪口特效
        if (pools[0].IsTemp())
        {
            effect = Instantiate(_assaultRifleView.FireEffect, _assaultRifleView.M_FireEffectPos.position
                , Quaternion.identity, _assaultRifleView.AllFireEffect_Parent);
        }
        else
        {
            effect = pools[0].GetObject();
            effect.SetActive(true);
            effect.GetComponent<Transform>().position = _assaultRifleView.M_FireEffectPos.position;
        }
        effect.GetComponent<ParticleSystem>().Play();
        effect.name = "FireEffect";


        //弹壳出仓动画
        if (pools[1].IsTemp())
        {
            shell = Instantiate(_assaultRifleView.Shell_Prefab, _assaultRifleView.M_ShellEffectPos.position
                 , Quaternion.identity, _assaultRifleView.AllShell_Parent);
        }
        else
        {
            shell = pools[1].GetObject();
            shell.SetActive(true);
            shell.GetComponent<Rigidbody>().isKinematic = true;
            shell.GetComponent<Transform>().position = _assaultRifleView.M_ShellEffectPos.position;
            shell.GetComponent<Rigidbody>().isKinematic = false;
        }
        shell.name = "shell";
        shell.GetComponent<Rigidbody>().AddForce(_assaultRifleView.M_ShellEffectPos.up * Random.Range(45f, 60f));

        StartCoroutine(DelayIntoPool(effect, pools[0]));
        StartCoroutine(DelayIntoPool(shell, pools[1]));
    }

    private IEnumerator DelayIntoPool(GameObject go, ObjectPool pool)
    {
        yield return new WaitForSeconds(2);
        go.SetActive(false);
        pool.AddObject(go);
    }
}
