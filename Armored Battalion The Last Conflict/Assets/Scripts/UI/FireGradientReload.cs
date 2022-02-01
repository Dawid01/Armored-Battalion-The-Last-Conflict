using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireGradientReload : MonoBehaviour
{

    private Image img;

    void Start()
    {
        img = GetComponent<Image>();
    }

    void Update()
    {
        float fillAmount = img.fillAmount;
        byte r = (byte)(255 - (fillAmount * 255));
        byte g = (byte)(fillAmount * 255);
        img.color = new Color32(r,g,0,255);
    }
}
