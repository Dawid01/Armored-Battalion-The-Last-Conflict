using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DropTanks : MonoBehaviour
{

    public List<Transform> tanks;
    public List<Rigidbody> rigidbodies;


    void Start()
    {
        foreach(Transform tank in tanks){
            Rigidbody rb = tank.GetChild(0).GetComponent<Rigidbody>();
            rb.useGravity = false;
            rb.isKinematic = true;
            rigidbodies.Add(rb);
        }
    }

    void Update()
    {
        
    }

    public void Activate(){
        Drop();
        End();
    }


    public void Drop(){
        for(int i = 0; i < tanks.Count; i++){
            Transform tank = tanks[i];
            Rigidbody rb = rigidbodies[i];
            rb.isKinematic = false;
            rb.useGravity = true;
            tank.SetParent(null);
        }
    }

    public void End(){
        foreach(Transform tank in tanks){
            tank.position = tank.GetChild(0).position;
            tank.GetChild(0).localPosition = Vector3.zero;
            tank.GetComponent<NavMeshAgent>().enabled = true;
        }
        Destroy(this.transform.parent.gameObject);

    }
}
