using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RocketBullet : MonoBehaviour
{
    public Transform target;
    public float speed = 10f;
    public float curveSpeed = 50f;

    public float demage = 50f;
    bool canDemage = false;
    [HideInInspector] public HpSystem hp;

    private float xRandom;
    private float yRandom;
    private float zRandom;
    private TrailRenderer trailRenderer;

    public Transform parent; 


    private void Awake() {
        gameObject.SetActive(false);
        trailRenderer = GetComponent<TrailRenderer>();
    }

    void Start()
    {        
        xRandom = transform.localRotation.x + Random.Range(-20, -40);
        yRandom = transform.localRotation.y + Random.Range(-40, 40);
        zRandom = Random.Range(-120, 120);
        trailRenderer.enabled = false;
       
        // if(hp){
        //     Invoke("resetBullet", 10f);
        // }else{
        //     //trailRenderer.enabled = false;
        // }


    }

    void Update()
    {     
        if(hp){    
            if(target && hp.hp > 0 && canDemage){
                Vector3 relativePos  = (target.position + Vector3.up * 0.5f) - transform.position ;
                Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, Time.deltaTime * curveSpeed);
            }

            if(!canDemage){
                transform.Translate(0f, 0f, speed * Time.deltaTime);
            }else{
                transform.Translate(0f, 0f, speed * Time.deltaTime * 2f);
            }
        }else{
            if(!canDemage){
                transform.Translate(0f, 0f, speed * Time.deltaTime);
                Vector3 relativePos  = (target.position + Vector3.up * 0.5f) - transform.position ;
                Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, Time.deltaTime * curveSpeed);           
            }else if(target){
                transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * 20f);
                transform.rotation = target.rotation;       
            }
          
        }

    }
    // private void OnCollisionEnter(Collision other) {
    //      if(other.transform.tag != "Player" && other.transform.tag != "Friend"){
    //         HpSystem hpSystem = other.transform.GetComponent<HpSystem>();
    //         if(hpSystem){
    //              hpSystem.giveDemage(demage);
    //         }        
    //     }        
    //   Destroy(this.gameObject);   
    // }

    private void OnTriggerEnter(Collider other) {
        if(other.transform.tag != "Player" && other.transform.tag != "Friend"){
            if(other.transform.TryGetComponent(out HpSystem hpSystem)){
                hpSystem.giveDemage(demage);
            }
            // if(hpSystem){
            //      hpSystem.giveDemage(demage);
            // } 
            //Destroy(this.gameObject);       
            resetBullet();
        }           
    }

    public void setCanDemage(){
        canDemage = true;
        trailRenderer.enabled = true;
    }


    public void resetBullet() {
        transform.parent = parent;
        gameObject.SetActive(false);
        canDemage = false;
        trailRenderer.Clear();
        transform.localPosition = Vector3.zero;
        transform.localEulerAngles = Vector3.zero;

    }

    public void activeBullet()
    {
        gameObject.SetActive(true);
        Invoke("resetBullet", 10f);
        //rb.isKinematic = false;
        transform.parent = null;
    }
}
