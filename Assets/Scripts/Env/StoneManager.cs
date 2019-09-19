using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 场景石头管理器
/// </summary>
public class StoneManager : ResourcesManager
{

    private GameObject m_prefab_stone;
    private GameObject m_prefab_metal;


    protected override void SetParent()
    {
        M_Parent = M_Transform.Find("Stones");
    }

    protected override void SetPoints()
    {
        M_Points = M_Parent.GetComponentsInChildren<Transform>();
    }
    protected override void FindPrefab()
    {

        m_prefab_stone = Resources.Load<GameObject>("Env/Rock_Normal");
        m_prefab_metal = Resources.Load<GameObject>("Env/Rock_Metal");

    }

    /// <summary>
    /// 在指定位置生成随机石头
    /// </summary>
    protected override void CreateAllStone()
    {
        GameObject tempStone = null;

        for (int i = 1; i < M_Points.Length; i++)
        {
            M_Points[i].GetComponent<MeshRenderer>().enabled = false;

            if (Random.Range(0, 2) == 0)
                tempStone = m_prefab_stone;
            else
                tempStone = m_prefab_metal;

            Quaternion tempRot = Quaternion.Euler(new Vector3(0, Random.Range(0f, 360f), 0));
            GameObject stone = Instantiate(tempStone, M_Points[i].position, tempRot, M_Parent);
            stone.GetComponent<Transform>().localScale = new Vector3(Random.Range(0.7f, 1)
                , Random.Range(0.7f, 1), Random.Range(0.7f, 1));

        }
    }


}
