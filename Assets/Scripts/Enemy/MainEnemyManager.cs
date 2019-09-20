using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 敌人主管理器
/// </summary>
public class MainEnemyManager : MonoBehaviour
{
    private Transform m_transform;
    private Transform[] m_points;
    void Start()
    {
        m_transform = gameObject.GetComponent<Transform>();
        m_points = m_transform.Find("points").GetComponentsInChildren<Transform>();

        for (int i = 1; i < m_points.Length; i++)
        {
            m_points[i].GetComponent<MeshRenderer>().enabled = false;

            if (i % 2 == 0)
            {
                m_points[i].gameObject.AddComponent<EnemyManager>().Type = EnemyType.BOAR;
            }
            else
            {
                m_points[i].gameObject.AddComponent<EnemyManager>().Type = EnemyType.CANNIBAL;
            }
        }
    }

}
