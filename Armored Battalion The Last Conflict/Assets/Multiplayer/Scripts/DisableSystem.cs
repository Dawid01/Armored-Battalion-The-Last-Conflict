using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class DisableSystem : MonoBehaviour
{

    public PhotonView photonView;
    public List<MonoBehaviour> objectToDisalbe;

    void Start()
    {
        if (!photonView.IsMine) {
            foreach (MonoBehaviour b in objectToDisalbe) {
                b.enabled = false;
            }
        }
    }

    void Update()
    {
        
    }
}
