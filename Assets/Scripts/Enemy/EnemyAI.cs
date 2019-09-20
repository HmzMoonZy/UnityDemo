using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// 单个敌人逻辑
/// </summary>
public class EnemyAI : MonoBehaviour
{
    private Transform m_transform;
    private NavMeshAgent m_navMeshAgrnt;
    private Vector3 dir;                    //导航目标点
    private List<Vector3> dirList;          //导航目标点列表


    //属性
    public Vector3 Dir { get { return dir; } set { dir = value; } }
    public List<Vector3> DirList { get { return dirList; } set { dirList = value; } }

    private void Start()
    {
        m_transform = gameObject.GetComponent<Transform>();
        m_navMeshAgrnt = gameObject.GetComponent<NavMeshAgent>();

        m_navMeshAgrnt.SetDestination(dir);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            KillSelf();
        }
        ChangePos();
    }

    private void ChangePos()
    {
        if (Vector3.Distance(m_transform.position, dir) < 1)
        {
            int index = UnityEngine.Random.Range(0, dirList.Count);
            dir = dirList[index];
            m_navMeshAgrnt.SetDestination(dir);
        }
    }

    private void KillSelf()
    {
        Destroy(gameObject);
        SendMessageUpwards("Death", gameObject);
    }
}
