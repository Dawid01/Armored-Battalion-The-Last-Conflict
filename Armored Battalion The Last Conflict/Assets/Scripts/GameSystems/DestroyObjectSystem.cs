using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectSystem : MonoBehaviour
{
    public ParticleSystem particle;
    private MeshRenderer mesh;
    private MeshCollider collider;
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        collider = GetComponent<MeshCollider>();   
    }

    private void OnCollisionEnter(Collision other) {
        mesh.enabled = false;
        collider.enabled = false;
        particle.Play();
        Destroy(this.gameObject, particle.startLifetime);
    }

}
