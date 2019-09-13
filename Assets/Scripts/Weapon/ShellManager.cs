using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellManager : MonoBehaviour
{
    private Transform m_transform;
    void Awake()
    {
        m_transform = gameObject.GetComponent<Transform>();
    }
    void Update()
    {
        m_transform.Rotate(Vector3.up, Random.Range(0, 5f));
    }
}
