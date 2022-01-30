using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using System;

public class ShowPING : MonoBehaviour
{

    private TextMeshProUGUI lag;

    void Start()
    {
        lag = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) { 

        lag.text = Mathf.Abs((float) (PhotonNetwork.Time - info.timestamp)) + "ms";
    }

}
