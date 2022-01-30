using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Photon.Pun;

public class PlayerInfo : MonoBehaviour
{
    public PhotonView photonView;
    public TextMeshProUGUI nicknameText;
    private Transform cam;


    void Start()
    {
        if(!photonView.IsMine){
            nicknameText.text = photonView.Owner.NickName;
            cam = Camera.main.transform;
        }else{
            //Destroy(this.gameObject);
            gameObject.active = false;
        }
    }

    void Update()
    {
        transform.LookAt(cam);
    }
}
