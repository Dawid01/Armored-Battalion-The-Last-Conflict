using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkTankMovement : MonoBehaviourPun, IPunObservable
{
    protected TankMovement tankMovement;

    private void Awake() {
        tankMovement = GetComponent<TankMovement>();
    }


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){
         if (stream.IsWriting)
            {
                stream.SendNext(tankMovement.rb.position);
                stream.SendNext(tankMovement.rb.rotation);
                stream.SendNext(tankMovement.rb.velocity);
            }
            else
            {
                tankMovement.rb.position = (Vector3) stream.ReceiveNext();
                tankMovement.rb.rotation = (Quaternion) stream.ReceiveNext();
                tankMovement.rb.velocity = (Vector3) stream.ReceiveNext();

                float lag = Mathf.Abs((float) (PhotonNetwork.Time - info.timestamp));
                tankMovement.rb.position += tankMovement.rb.velocity * lag;
            }
    }
        

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
