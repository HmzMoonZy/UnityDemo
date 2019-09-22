using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public enum ActionState
{
    IDLE,
    WALK,
    ENTERRUN,
    EXITRUN,
    ENTERATTACK,
    EXITATTACK,
    DEATH
}

/// <summary>
/// 单个敌人逻辑
/// </summary>
public class EnemyAI : MonoBehaviour
{
    private Transform m_transform;
    private NavMeshAgent m_navMeshAgrnt;
    private Animator m_animator;

    private Transform m_player;             //玩家Transform       
    private PlayerController m_playerCtrl;  //玩家控制器
    private Vector3 m_navDir;               //导航目标点
    private List<Vector3> m_navDirList;     //导航目标点列表

    private GameObject m_bloodEffect;       //血液特效

    private ActionState m_state = ActionState.IDLE;

    //游戏数值
    private int m_helthPoint;
    private int m_attackPoint;

    //属性
    public Vector3 M_NavDir { get { return m_navDir; } set { m_navDir = value; } }
    public List<Vector3> M_NavDirList { get { return m_navDirList; } set { m_navDirList = value; } }
    public ActionState M_State { get { return m_state; } set { m_state = value; } }

    public int M_HP
    {
        get { return m_helthPoint; }
        set
        {
            m_helthPoint = value;
            if (M_HP <= 0)
                DeathState();
        }

    }
    public int M_AP { get { return m_attackPoint; } set { m_attackPoint = value; } }

    private void Awake()
    {
        m_transform = gameObject.GetComponent<Transform>();
        m_navMeshAgrnt = gameObject.GetComponent<NavMeshAgent>();
        m_animator = gameObject.GetComponent<Animator>();
        m_player = GameObject.Find("FPSController").GetComponent<Transform>();
        m_playerCtrl = m_player.GetComponent<PlayerController>();

        m_bloodEffect = Resources.Load<GameObject>("Effects/Weapon/Bullet Impact FX_Flesh");

        m_navMeshAgrnt.SetDestination(m_navDir);
    }
    void Update()
    {
        if (m_state != ActionState.DEATH)
        {
            Patrol();
            TrackingPlayer();
            AttackPlayer();
        }

        //if (Input.GetKeyDown(KeyCode.K))
        //{
        //    DeathState();
        //}
        //if (Input.GetKeyDown(KeyCode.N))
        //{
        //    m_animator.SetTrigger("GetHitNormal");
        //    m_navMeshAgrnt.speed = 0;
        //}
        //if (Input.GetKeyDown(KeyCode.H))
        //{
        //    m_animator.SetTrigger("GetHitHard");
        //    m_navMeshAgrnt.speed = 0;
        //}

    }
    /// <summary>
    /// 切换状态
    /// </summary>
    private void SwitchState(ActionState state)
    {
        switch (state)
        {
            case ActionState.IDLE:
                IdleState();
                break;
            case ActionState.WALK:
                WalkState();
                break;
            case ActionState.ENTERRUN:
                EnterRunState();
                break;
            case ActionState.EXITRUN:
                ExitRunState();
                break;
            case ActionState.ENTERATTACK:
                EnterAttackState();
                break;
            case ActionState.EXITATTACK:
                ExitAttackState();
                break;
            case ActionState.DEATH:
                DeathState();
                break;
        }
    }
    private IEnumerator Death()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
        SendMessageUpwards("Death", gameObject);
    }

    /// <summary>
    /// 在导航目标点巡逻;
    /// </summary>
    private void Patrol()
    {
        if (m_state == ActionState.IDLE || m_state == ActionState.WALK)
        {
            if (Vector3.Distance(m_transform.position, m_navDir) <= 1)
            {
                int index = UnityEngine.Random.Range(0, m_navDirList.Count);
                m_navDir = m_navDirList[index];
                m_navMeshAgrnt.SetDestination(m_navDir);

                SwitchState(ActionState.IDLE);
            }
            else
            {
                SwitchState(ActionState.WALK);
            }
        }
    }
    /// <summary>
    /// 追踪玩家;
    /// </summary>
    private void TrackingPlayer()
    {
        if (Vector3.Distance(m_transform.position, m_player.position) <= 20)
        {
            SwitchState(ActionState.ENTERRUN);
        }
        else
        {
            SwitchState(ActionState.EXITRUN);
        }
    }
    /// <summary>
    /// 攻击玩家
    /// </summary>
    private void AttackPlayer()
    {
        if (m_state == ActionState.ENTERRUN)
        {
            if (Vector3.Distance(m_transform.position, m_player.position) <= 2.1f)
            {
                SwitchState(ActionState.ENTERATTACK);
            }
            else
            {
                SwitchState(ActionState.EXITATTACK);
            }
        }
    }

    /// <summary>
    /// 等待状态;
    /// </summary>
    private void IdleState()
    {
        m_animator.SetBool("walk", false);
        m_state = ActionState.IDLE;
    }
    /// <summary>
    /// 行走状态;
    /// </summary>
    private void WalkState()
    {
        m_animator.SetBool("Walk", true);
        m_state = ActionState.WALK;
    }
    /// <summary>
    /// 进入奔跑状态;
    /// </summary>
    private void EnterRunState()
    {
        m_animator.SetBool("Run", true);
        m_state = ActionState.ENTERRUN;
        m_navMeshAgrnt.speed = 5f;
        m_navMeshAgrnt.enabled = true;
        m_navMeshAgrnt.SetDestination(m_player.position);
    }
    /// <summary>
    /// 退出奔跑状态;
    /// </summary>
    private void ExitRunState()
    {
        m_animator.SetBool("Run", false);
        SwitchState(ActionState.WALK);
        m_navMeshAgrnt.speed = 0.8f;
        //m_navMeshAgrnt.enabled = true;
        m_navMeshAgrnt.SetDestination(m_navDir);
    }
    /// <summary>
    /// 进入攻击状态;
    /// </summary>
    private void EnterAttackState()
    {
        m_animator.SetBool("Attack", true);
        m_navMeshAgrnt.enabled = false;
        m_state = ActionState.ENTERATTACK;
    }
    /// <summary>
    /// 退出攻击状态;
    /// </summary>
    private void ExitAttackState()
    {
        m_animator.SetBool("Attack", false);
        m_navMeshAgrnt.enabled = true;
        SwitchState(ActionState.ENTERRUN);
    }
    /// <summary>
    /// 死亡状态
    /// </summary>
    private void DeathState()
    {
        m_state = ActionState.DEATH;
        m_animator.SetTrigger("Death");
        m_navMeshAgrnt.enabled = false;

        StartCoroutine(Death());
    }
    public void GetHitNormal(int damage)
    {
        m_animator.SetTrigger("GetHitNormal");
        M_HP -= damage;
        m_navMeshAgrnt.enabled = false;
        Debug.Log("命中! 造成了" + damage + "的伤害");
    }
    public void GetHitHard(int damage)
    {
        m_animator.SetTrigger("GetHitHard");
        M_HP -= damage;
        Debug.Log("命中头部! 造成了" + damage + "的伤害");
        m_navMeshAgrnt.enabled = false;
    }

    public void PlayEffect(RaycastHit hit)
    {
        GameObject effect = Instantiate<GameObject>(m_bloodEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(effect, 3);
    }

    public void AttackPleyer()
    {
        m_playerCtrl.CutPlayerHp(m_attackPoint);
    }
}
