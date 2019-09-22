using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BloodScreen : MonoBehaviour
{
    private Transform m_transform;
    private Image m_image;
    private byte alpha;
    void Awake()
    {
        m_transform = gameObject.GetComponent<Transform>();
        m_image = gameObject.GetComponent<Image>();
    }
    public void SetBloodScreen(float hp_per)
    {
        alpha = (byte)((1 - hp_per) * 255);
        m_image.color = new Color32(255, 255, 255, alpha);
    }
}
