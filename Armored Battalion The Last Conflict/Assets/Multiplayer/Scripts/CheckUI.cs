using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CheckUI : MonoBehaviour
{
    public PhotonView photonView;


    void Start()
    {
        if(!photonView.IsMine){
            Destroy(this.gameObject);
        }
    }

    void Update()
    {
        
    }
}
