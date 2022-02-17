using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectTankImage : MonoBehaviour
{

    public string tankName;
    public int cost;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI nameTextShadow;
    public TextMeshProUGUI costText;

    void Start()
    {
        nameText.text = tankName;
        nameTextShadow.text = tankName;
        costText.text = "cost: " + cost;
    }

    void Update()
    {
        
    }
}
