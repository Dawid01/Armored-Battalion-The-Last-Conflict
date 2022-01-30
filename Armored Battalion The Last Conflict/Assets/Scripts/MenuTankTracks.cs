using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTankTracks : MonoBehaviour
{

    private Truck truck;
    private Quaternion oldRot;
    void Start()
    {
        truck = GetComponent<Truck>();
        oldRot = transform.rotation;
    }

    void Update()
    {
        
        float deltaRot = transform.rotation.y - oldRot.y;
        oldRot = transform.rotation;
        deltaRot = Mathf.Clamp(deltaRot, -0.01f, 0.01f);

        truck.rightTruckSpeed = deltaRot/2f;
        truck.leftTruckSpeed = -deltaRot/2f;
            
        
    }
}
