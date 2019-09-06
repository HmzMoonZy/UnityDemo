using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolBarSlotController : MonoBehaviour
{
    private Transform _transform;
    private Button _button;
    private Image _image;

    private Text key_text;

    private bool activeState = false;   //该槽的激活与否
    void Awake()
    {
        _transform = gameObject.GetComponent<Transform>();
        _button = gameObject.GetComponent<Button>();
        _image = gameObject.GetComponent<Image>();

        key_text = _transform.Find("Key").GetComponent<Text>();

        _button.onClick.AddListener(ButtonEvent);
    }
    public void Init(int key)
    {
        key_text.text = key.ToString();
    }

    public void ButtonEvent()
    {
        if (activeState)
        {
            NormalButton();
        }
        else
        {
            ActiveButton();
        }
        SendMessageUpwards("SelectedSlot", gameObject);
    }
    private void ActiveButton()
    {
        _image.color = Color.red;
        activeState = true;
    }
    public void NormalButton()
    {
        _image.color = Color.white;
        activeState = false;
    }
}
