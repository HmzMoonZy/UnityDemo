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
    private GameObject woodenSpear;
    private Animator buildingPlan_Animator;
    private Animator woodenSpear_Animator;

    private GameObject currentEquip;
    private GameObject targetEquip;

    void Start()
    {
        //组件查找
        _transform = gameObject.GetComponent<Transform>();

        buildingPlan = _transform.Find("PlayerCamera/Building Plan").gameObject;
        woodenSpear = _transform.Find("PlayerCamera/Wooden Spear").gameObject;
        buildingPlan_Animator = buildingPlan.GetComponent<Animator>();
        woodenSpear_Animator = woodenSpear.GetComponent<Animator>();

        //值初始化
        currentEquip = buildingPlan;
        targetEquip = null;
    }

    void Update()
    {
        ChangeState();
    }

    private void ChangeState()
    {
        if (Input.GetKeyDown(KeyCode.B))    //建造模式
        {
            targetEquip = buildingPlan;
            ChangeModel();
        }
        if (Input.GetKeyDown(KeyCode.M))    //战斗模式(长矛模型)
        {
            targetEquip = woodenSpear;
            ChangeModel();
        }
    }
    private void ChangeModel()
    {
        currentEquip.GetComponent<Animator>().SetTrigger("Holster");
        StartCoroutine("HolsterWait");

    }
    private IEnumerator HolsterWait()
    {
        yield return new WaitForSeconds(1);
        currentEquip.SetActive(false);
        targetEquip.SetActive(true);
        currentEquip = targetEquip;
    }
}
