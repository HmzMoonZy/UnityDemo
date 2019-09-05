using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 合成物品控制
/// </summary>
public class SynthesisController : MonoBehaviour
{
    private Transform m_transform;
    private Transform bg_Transform;
    private Image icon_Image;
    private Button createone_Button;
    private Button createall_Button;

    private int tempID;         //合成物品的ID
    private string tempName;    //合成物品的文件名

    private GameObject inventoryItem_Prefab;
    public GameObject InventoryItem_Prefab { set { inventoryItem_Prefab = value; } }
    void Awake()
    {
        m_transform = gameObject.GetComponent<Transform>();
        bg_Transform = m_transform.Find("Good");
        icon_Image = m_transform.Find("Good/Icon").GetComponent<Image>();
        createone_Button = m_transform.Find("Button_One").GetComponent<Button>();
        createall_Button = m_transform.Find("Button_All").GetComponent<Button>();
        icon_Image.gameObject.SetActive(false);

        createone_Button.onClick.AddListener(ButtonOneOnClick);

        ButtonUnActive();
    }

    public void Init(int id, string name, string iconName)
    {
        icon_Image.gameObject.SetActive(true);
        icon_Image.sprite = Resources.Load<Sprite>("Item/" + iconName);
        this.tempID = id;
        this.tempName = name;

    }
    public void ClearIcon()
    {
        icon_Image.enabled = false;
    }
    public void ButtonUnActive()
    {
        createone_Button.interactable = false;
        createall_Button.interactable = false;
        createone_Button.transform.Find("Text").GetComponent<Text>().color = Color.gray;
        createall_Button.transform.Find("Text").GetComponent<Text>().color = Color.gray;
    }
    public void ButtonActive()
    {
        createone_Button.interactable = true;
        createall_Button.interactable = true;
        createone_Button.transform.Find("Text").GetComponent<Text>().color = Color.white;
        createall_Button.transform.Find("Text").GetComponent<Text>().color = Color.white;
    }
    private void ButtonOneOnClick()
    {
        GameObject temp = Instantiate<GameObject>(inventoryItem_Prefab, bg_Transform);
        temp.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 120);
        temp.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 120);
        temp.GetComponent<InventoryItemController>().Init(tempID, tempName, 1);

        SendMessageUpwards("MakeFinish");
        ButtonUnActive();
    }
}
