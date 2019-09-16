using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 枪械控制层父类.
/// </summary>
public abstract class GunControllerBase : MonoBehaviour
{
    //持有V层父类
    private GunViewBase m_gunViewBase;

    //武器类数值字段
    [SerializeField] private int id;
    [SerializeField] private int damage;
    [SerializeField] private int durable;
    private float totalDurable;
    [SerializeField] private WeaponType type;

    private bool canShot = true;          //开火状态.

    //射线相关
    private Ray ray;                      //射击射线.
    private RaycastHit hit;               //射线碰撞点.

    private GameObject item;              //对应的ItemUI

    #region 属性
    public GunViewBase M_GunViewBase { get { return m_gunViewBase; } set { m_gunViewBase = value; } }

    public int ID { get { return id; } set { id = value; } }
    public int Damage { get { return damage; } set { damage = value; } }
    public WeaponType Type { get { return type; } set { type = value; } }


    public int Durable
    {
        get { return durable; }
        set
        {
            durable = value;
            if (durable <= 0)
            {
                Destroy(gameObject);
                Destroy(m_gunViewBase.M_SightPos.gameObject);
            }
        }
    }

    public Ray MyRay { get { return ray; } set { ray = value; } }
    public RaycastHit Hit { get { return hit; } set { hit = value; } }

    public bool CanShot { get { return canShot; } set { canShot = value; } }

    public GameObject Item { get { return item; } set { item = value; } }
    #endregion

    public virtual void Start()
    {
        m_gunViewBase = gameObject.GetComponent<GunViewBase>();
        totalDurable = durable;

        Init();
    }

    private void Update()
    {
        ShootDetection();
        MouseCtrl();
    }

    /// <summary>
    /// 射击检测.
    /// </summary>
    private void ShootDetection()
    {
        ray = new Ray(M_GunViewBase.M_MuzzlePos.position, M_GunViewBase.M_MuzzlePos.forward * 1000);
        Debug.DrawRay(m_gunViewBase.M_MuzzlePos.position, m_gunViewBase.M_MuzzlePos.forward * 500, Color.red);
        if (Physics.Raycast(ray, out hit, 1500, 1 << 11))       //11层:Env层
        {
            //准星定位(辅助瞄准?).
            Vector2 sightPos = RectTransformUtility.WorldToScreenPoint(M_GunViewBase.M_EnvCamera, hit.point);
            m_gunViewBase.M_SightPos.position = sightPos;
        }
        else
            hit.point = Vector3.zero;
    }

    /// <summary>
    /// 鼠标控制.
    /// </summary>
    private void MouseCtrl()
    {
        //左键 -> 开火
        if (Input.GetMouseButtonDown(0) && canShot)
        {
            MouseButtonDown0();
        }
        //按住右键 -> 瞄准
        if (Input.GetMouseButton(1))
        {
            MouseButton1();
        }
        //松开右键 -> 取消瞄准
        if (Input.GetMouseButtonUp(1))
        {
            MouseButtonUp1();
        }
    }

    public virtual void MouseButtonDown0()
    {
        m_gunViewBase.M_Animator.SetTrigger("Fire");
        Shot();
        item.GetComponent<InventoryItemController>().UpdateBar(durable/totalDurable);
        PlayAudio();
    }

    private void MouseButton1()
    {
        m_gunViewBase.M_Animator.SetBool("HoldPose", true);
        m_gunViewBase.AimAction();
        m_gunViewBase.M_SightPos.gameObject.SetActive(false);
    }

    private void MouseButtonUp1()
    {
        m_gunViewBase.M_Animator.SetBool("HoldPose", false);
        m_gunViewBase.CancelAimAction();
        m_gunViewBase.M_SightPos.gameObject.SetActive(true);
    }

    /// <summary>
    /// 播放GunViewBase.M_FireAudioClip音效
    /// </summary>
    protected void PlayAudio()
    {
        AudioSource.PlayClipAtPoint(M_GunViewBase.M_FireAudioClip, M_GunViewBase.M_MuzzlePos.position);
    }

    /// <summary>
    /// 延迟入池
    /// </summary>
    protected IEnumerator DelayIntoPool(GameObject go, ObjectPool pool)
    {
        yield return new WaitForSeconds(2);
        go.SetActive(false);
        pool.AddObject(go);
    }

    /// <summary>
    /// 射击状态,动画Event调用
    /// 1:可射击 0:不可射击
    /// </summary>
    public void ChangeCanShot(int state)
    {
        if (state == 1)
            CanShot = true;
        else
            CanShot = false;
    }

    /// <summary>
    /// 初始化子类自身变量
    /// </summary>
    protected abstract void Init();

    /// <summary>
    /// 枪械射击方法
    /// </summary>
    protected abstract void Shot();
}
