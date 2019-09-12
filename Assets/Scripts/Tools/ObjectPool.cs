using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 对象池
/// </summary>
public class ObjectPool : MonoBehaviour
{
    private Queue<GameObject> pool;
    void Awake()
    {
        pool = new Queue<GameObject>();
    }

    //将对象添加进对象池
    public void AddObject(GameObject go)
    {
        pool.Enqueue(go);
    }
    /// <summary>
    /// 获取池中对象
    /// </summary>
    /// <returns></returns>
    public GameObject GetObject()
    {
        GameObject temp = null;
        temp = pool.Dequeue();
        return temp;
    }
    /// <summary>
    /// 是否是空池?
    /// </summary>
    public bool IsTemp()
    {
        if (pool.Count <= 0)
            return true;
        else
            return false;
    }

}
