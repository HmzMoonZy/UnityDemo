using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SynthesisContentItemContorller : MonoBehaviour {
    private Transform m_transform;

    private Text m_Text;
    private GameObject m_bg;
    private Button m_Button;

	void Awake () {
        m_transform = gameObject.GetComponent<Transform>();

        m_Text = gameObject.GetComponent<Text>();
        m_Button = gameObject.GetComponent<Button>();
        m_bg = m_transform.Find("Item").gameObject;
    }
    public void Init(string name) 
    {
        m_Text.text = name;
        m_bg.SetActive(false);
    }



}
