using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public enum AnimationState
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
    private Transform m_player;
    private NavMeshAgent m_navMeshAgrnt;
    private Animator m_animator;
    private Vector3 m_navDir;                    //导航目标点
    private List<Vector3> m_navDirList;          //导航目标点列表

    private AnimationState m_state = AnimationState.IDLE;


    //属性
    public Vector3 M_NavDir { get { return m_navDir; } set { m_navDir = value; } }
    public List<Vector3> M_NavDirList { get { return m_navDirList; } set { m_navDirList = value; } }
    public AnimationState M_State { get { return m_state; } set { m_state = value; } }

    private void Start()
    {
        m_transform = gameObject.GetComponent<Transform>();
        m_navMeshAgrnt = gameObject.GetComponent<NavMeshAgent>();
        m_animator = gameObject.GetComponent<Animator>();
        m_player = GameObject.Find("FPSController").GetComponent<Transform>();

        m_navMeshAgrnt.SetDestination(m_navDir);
    }
    void Update()
    {
        Patrol();
        TrackingPlayer();
        AttackPlayer();

        if (Input.GetKeyDown(KeyCode.K))
        {
            DeathState();
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            m_animator.SetTrigger("GetHitNormal");
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            m_animator.SetTrigger("GetHitHard");
        }

    }
    /// <summary>
    /// 切换状态
    /// </summary>
    private void SwitchState(AnimationState state)
    {
        switch (state)
        {
            case AnimationState.IDLE:
                IdleState();
                break;
            case AnimationState.WALK:
                WalkState();
                break;
            case AnimationState.ENTERRUN:
                EnterRunState();
                break;
            case AnimationState.EXITRUN:
                ExitRunState();
                break;
            case AnimationState.ENTERATTACK:
                EnterAttackState();
                break;
            case AnimationState.EXITATTACK:
                ExitAttackState();
                break;
            case AnimationState.DEATH:
                DeathState();
                break;
        }
    }

    /// <summary>
    /// 在导航目标点巡逻;
    /// </summary>
    private void Patrol()
    {
        if (m_state == AnimationState.IDLE || m_state == AnimationState.WALK)
        {
            if (Vector3.Distance(m_transform.position, m_navDir) < 1)
            {
                SwitchState(AnimationState.IDLE);
                int index = UnityEngine.Random.Range(0, m_navDirList.Count);
                m_navDir = m_navDirList[index];
                m_navMeshAgrnt.SetDestination(m_navDir);
            }
            else
                SwitchState(AnimationState.WALK);
        }
    }
    /// <summary>
    /// 追踪玩家;
    /// </summary>
    private void TrackingPlayer()
    {
        if (Vector3.Distance(m_transform.position, m_player.position) <= 20)
        {
            SwitchState(AnimationState.ENTERRUN);
        }
        else SwitchState(AnimationState.EXITRUN);
    }
    /// <summary>
    /// 攻击玩家
    /// </summary>
    private void AttackPlayer()
    {
        if (m_state == AnimationState.ENTERRUN)
        {
            if (Vector3.Distance(m_transform.position, m_player.position) <= 2.5f)
            {
                SwitchState(AnimationState.ENTERATTACK);
            }
            else
            {
                SwitchState(AnimationState.EXITATTACK);
            }
        }
    }
    /// <summary>
    /// 等待状态;
    /// </summary>
    private void IdleState()
    {
        m_animator.SetBool("walk", false);
        m_state = AnimationState.IDLE;
    }
    /// <summary>
    /// 行走状态;
    /// </summary>
    private void WalkState()
    {
        m_animator.SetBool("Walk", true);
        m_state = AnimationState.WALK;
    }
    /// <summary>
    /// 进入奔跑状态;
    /// </summary>
    private void EnterRunState()
    {
        m_animator.SetBool("Run", true);
        m_state = AnimationState.ENTERRUN;
        m_navMeshAgrnt.speed = 8;
        m_navMeshAgrnt.SetDestination(m_player.position);
    }
    /// <summary>
    /// 退出奔跑状态;
    /// </summary>
    private void ExitRunState()
    {
        m_animator.SetBool("Run", false);
        SwitchState(AnimationState.WALK);
        m_navMeshAgrnt.speed = 0.8f;
        m_navMeshAgrnt.SetDestination(m_navDir);
    }
    /// <summary>
    /// 进入攻击状态;
    /// </summary>
    private void EnterAttackState()
    {
        m_animator.SetBool("Attack", true);
        m_navMeshAgrnt.enabled = false;
        m_state = AnimationState.ENTERATTACK;
    }
    /// <summary>
    /// 退出攻击状态;
    /// </summary>
    private void ExitAttackState()
    {
        m_animator.SetBool("Attack", false);
        m_navMeshAgrnt.enabled = true;
        SwitchState(AnimationState.ENTERRUN);
    }
    private void DeathState()
    {
        m_state = AnimationState.DEATH;
        m_animator.SetTrigger("Death");
        m_navMeshAgrnt.enabled = false;

        StartCoroutine(KillSelf());
    }
    private IEnumerator KillSelf()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
        SendMessageUpwards("Death", gameObject);
    }
}
