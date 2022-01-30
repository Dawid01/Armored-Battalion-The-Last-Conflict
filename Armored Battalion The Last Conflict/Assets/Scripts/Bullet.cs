using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody rb;
    public TrailRenderer _renderer;
    public float timer;
    public Transform parent; 
    public ParticleSystem sparks;
    public float demage = 20;

    public bool enemyBullet = false;
    public bool granate = false;

    private AudioSource audioSource;

    public int maxRickoshets = 0;
    private int rickoshets;
    public bool isMultiplayer = false;



    void Start()
    {
        sparks.Stop();
        audioSource = GetComponent<AudioSource>();
        rickoshets = maxRickoshets;
    }

    void Update()
    {
        if (timer >= 0) {
            timer -= Time.deltaTime;
        }

        if (timer <= 0) {
            resetBullet();
        }
    }

    public void resetBullet() {
      //  audioSource.Play();
        transform.parent = parent;
        transform.eulerAngles = parent.eulerAngles;
        gameObject.SetActive(false);
        rb.velocity = Vector3.zero;
        _renderer.Clear();
        gameObject.transform.localPosition = Vector3.zero;
        rickoshets = maxRickoshets;
    }

    public void activeBullet(float force)
    {
        gameObject.SetActive(true);
        //rb.isKinematic = false;
        timer = 5f;
        transform.parent = null;
        rb.AddForce(rb.gameObject.transform.forward * force, ForceMode.Impulse);
    }


    private void OnTriggerEnter(Collider other) {
        if(enemyBullet && other.tag == "PlayerShield"){
            resetBullet();
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (!isMultiplayer)
        {
            if (!enemyBullet)
            {
                if (collision.transform.tag != "Player" && collision.transform.tag != "Friend")
                {
                    HpSystem hpSystem = collision.transform.GetComponent<HpSystem>();
                    if (!hpSystem)
                    {
                        try
                        {
                            hpSystem = collision.transform.parent.GetComponent<HpSystem>();
                        }
                        catch { }
                    }
                    if (hpSystem)
                    {
                        rickoshets = 0;
                        hpSystem.giveDemage(demage);
                    }
                    else
                    {
                        rickoshets--;
                    }
                    sparks.Play();
                    //rb.velocity = Vector3.zero;
                }

            }
            else
            {
                if (collision.transform.tag != "Enemy")
                {
                    HpSystem hpSystem = collision.transform.GetComponent<HpSystem>();
                    if (!hpSystem)
                    {
                        try
                        {
                            hpSystem = collision.transform.parent.GetComponent<HpSystem>();
                        }
                        catch { }
                    }
                    if (hpSystem)
                    {
                        hpSystem.giveDemage(demage);
                    }
                    sparks.Play();
                    rb.velocity = Vector3.zero;

                }
            }
        }
        else {
            resetBullet();
            HpSystem hpSystem = collision.transform.GetComponent<HpSystem>();
            if (!hpSystem)
            {
                try
                {
                    hpSystem = collision.transform.parent.GetComponent<HpSystem>();
                }
                catch { }
            }
            if (hpSystem)
            {
                hpSystem.giveMultiplayerDemage(demage);
            }
            sparks.Play();
            rb.velocity = Vector3.zero;
        }
        if(!granate){
            if(rickoshets <= 0){
                Invoke("resetBullet", 0.1f);
            }
        }else{
            Invoke("resetBullet", 2f);
        }
    }
}
