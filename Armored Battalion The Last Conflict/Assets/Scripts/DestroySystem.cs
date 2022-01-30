using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySystem : MonoBehaviour
{
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        //rb.isKinematic = true;
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > 20f)
        {
            Destroy(gameObject, 3f);
        } 
            rb.useGravity = true;
           // rb.isKinematic = false;
            //rb.AddForce(collision.relativeVelocity * 10f);
           // Destroy(gameObject, 3f);
       // }
    }
}
