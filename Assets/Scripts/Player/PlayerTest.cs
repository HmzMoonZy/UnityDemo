using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 角色测试
/// </summary>
public class PlayerTest : MonoBehaviour
{
    private Transform _transform;

    private GameObject buildingPlan;
    private Animator buildingPlan_Animator;
    
    private bool buildState = true;

    void Start()
    {
        _transform = gameObject.GetComponent<Transform>();

        buildingPlan = _transform.Find("FirstPersonCharacter/Building Plan").gameObject;
        buildingPlan_Animator = buildingPlan.GetComponent<Animator>();
    }

    void Update()
    {
        BuildSwitch();
    }

    private void BuildSwitch()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (buildState)
            {
                buildingPlan_Animator.SetTrigger("Holster");
                StartCoroutine("HolsterWait");
                buildState = false;
            }
            else
            {
                buildingPlan.SetActive(true);
                buildState = true;
            }
        }
    }
    private IEnumerator HolsterWait()
    {
        yield return new WaitForSeconds(0.5f);
        buildingPlan.SetActive(false);
    }
}
