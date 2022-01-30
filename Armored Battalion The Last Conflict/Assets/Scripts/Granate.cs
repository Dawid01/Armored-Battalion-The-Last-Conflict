using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Granate : MonoBehaviour
{

    public ParticleSystem explosion;
    public float demage;
    public SphereCollider collider;
    public SphereCollider collider2;

    private Rigidbody rb;
    private bool hit = false;
    private MeshRenderer mesh;


    private void OnCollisionEnter(Collision other) {
        if(!hit){
            explosion.Play();
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;
            rb.useGravity = false;
            collider.radius = 5f;
            Invoke("StopDemage", 0.1f);
            Destroy(this.gameObject, explosion.duration - 0.1f);
            hit = true;
            collider2.enabled = false;
            mesh.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag != "Player" && other.tag != "Friend"){
            HpSystem hpSystem = other.transform.GetComponent<HpSystem>();
            if(!hpSystem){
                try{
                    hpSystem = other.transform.parent.GetComponent<HpSystem>();
                }catch{}
            }
            if(hpSystem){
                hpSystem.giveDemage(demage);
            }
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mesh = GetComponent<MeshRenderer>();

    }

    void StopDemage(){
        collider.enabled = false;
    }
}
