using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeManager : ResourcesManager
{
    private GameObject m_prefab_broadleaf;
    private GameObject m_prefab_conifer;
    private GameObject m_prefab_palm;

    protected override void FindPrefab()
    {
        m_prefab_broadleaf = Resources.Load<GameObject>("Env/Broadleaf_Desktop");
        m_prefab_conifer = Resources.Load<GameObject>("Env/Conifer_Desktop");
        m_prefab_palm = Resources.Load<GameObject>("Env/Palm_Desktop");
    }
    protected override void SetParent()
    {
        M_Parent = M_Transform.Find("Trees");
    }
    protected override void SetPoints()
    {
        M_Points = M_Transform.Find("points").GetComponentsInChildren<Transform>();
    }
    protected override void CreateAllStone()
    {
        //GameObject temp = null;
        GameObject temp = m_prefab_conifer;

        for (int i = 1; i < M_Points.Length; i++)
        {
            M_Points[i].GetComponent<MeshRenderer>().enabled = false;

            //if (Random.Range(0, 3) == 0)
            //    temp = m_prefab_broadleaf;
            //else if (Random.Range(0, 2) == 0)
            //    temp = m_prefab_conifer;
            //else
            //    temp = m_prefab_palm;

            Quaternion tempRot = Quaternion.Euler(new Vector3(0, Random.Range(0f, 360f), 0));
            GameObject tree = Instantiate(temp, M_Points[i].position, tempRot, M_Parent);
            tree.GetComponent<Transform>().localScale = new Vector3(Random.Range(0.7f, 1)
                , Random.Range(0.7f, 1), Random.Range(0.7f, 1));
            tree.name = "conifer";
        }
    }
}
