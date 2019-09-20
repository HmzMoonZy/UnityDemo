using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private Transform m_transform;

    private GameObject m_prefab_boar;           //野猪
    private GameObject m_prefab_cannibal;       //野人
    private EnemyType type = EnemyType.NULL;    //敌人类型
    private List<GameObject> m_enemys = null;   //相同敌人列表

    //巡逻相关组件
    private Transform[] m_navPos = null;           //巡逻点
    private List<Vector3> m_list_navPos = null;    //巡逻点队列
    private int index = 0;                         //初始化巡逻点角标

    //属性
    public EnemyType Type { get { return type; } set { type = value; } }

    void Start()
    {
        FindInit();

        CreateEnemyByEnum();
    }

    private void FindInit()
    {
        m_transform = gameObject.GetComponent<Transform>();

        m_prefab_boar = Resources.Load<GameObject>("Enemy/MyBoar");
        m_prefab_cannibal = Resources.Load<GameObject>("Enemy/MyCannibal");
        m_enemys = new List<GameObject>();

        m_list_navPos = new List<Vector3>();

        //获取目标点数组
        m_navPos = m_transform.GetComponentsInChildren<Transform>(true);    //包含隐藏组件
        //数组入列
        for (int i = 1; i < m_navPos.Length; i++)
            m_list_navPos.Add(m_navPos[i].position);
    }

    /// <summary>
    /// 根据类型创建敌人预制体
    /// </summary>
    private void CreateEnemyByEnum()
    {
        if (type == EnemyType.BOAR)
        {
            CreateEnemy(m_prefab_boar);
        }
        if (type == EnemyType.CANNIBAL)
        {
            CreateEnemy(m_prefab_cannibal);
        }
    }
    /// <summary>
    /// 创建三个敌人预制体
    /// </summary>
    private void CreateEnemy(GameObject prefab)
    {
        GameObject tempEnemy = null;
        for (int i = 0; i < 3; i++)
        {
            tempEnemy = Instantiate(prefab, m_transform.position, Quaternion.identity, m_transform);
            //敌人目标点
            tempEnemy.GetComponent<EnemyAI>().M_NavDir = m_list_navPos[i];
            tempEnemy.GetComponent<EnemyAI>().M_NavDirList = m_list_navPos;
            m_enemys.Add(tempEnemy);
        }
    }
    /// <summary>
    /// 死亡方法
    /// </summary>
    private void Death(GameObject enemy)
    {
        //移出列表
        m_enemys.Remove(enemy);
        StartCoroutine(DelayCreateEnemy());
    }
    /// <summary>
    /// 延迟后生成一个敌人
    /// </summary>
    private IEnumerator DelayCreateEnemy()
    {
        GameObject tempEnemy = null;

        yield return new WaitForSeconds(3);
        if (type == EnemyType.BOAR)
        {
            tempEnemy = Instantiate(m_prefab_boar, m_transform.position, Quaternion.identity, m_transform);
        }
        if (type == EnemyType.CANNIBAL)
        {
            tempEnemy = Instantiate(m_prefab_cannibal, m_transform.position, Quaternion.identity, m_transform);
        }

        //初始化巡逻点
        tempEnemy.GetComponent<EnemyAI>().M_NavDir = m_list_navPos[index];
        tempEnemy.GetComponent<EnemyAI>().M_NavDirList = m_list_navPos;
        index++;
        index  = index % m_list_navPos.Count;
        //入组
        m_enemys.Add(tempEnemy);
    }
}
