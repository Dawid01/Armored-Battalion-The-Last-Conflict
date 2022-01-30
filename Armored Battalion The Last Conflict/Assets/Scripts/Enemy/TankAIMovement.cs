using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TankAIMovement : MonoBehaviour
{
    public float speed = 2f;
    public float turetRotationSpeed = 3f;
    public Transform target;
    public Transform startTarget;
    public Transform turet;
    public Transform barrel;
    public Transform barrelRot;
    public NavMeshAgent agent;
    public LayerMask playerMask;
    private Rigidbody rb;
    private Vector3 movement = Vector3.zero;
    public Truck truck;
    public float gunMaxDeltRot;

    private float distanceToPlayer;
    private Vector3 randomPosition;

    public PoolingShooting poolingShooting;
    public ParticleSystem sparks;
    private AudioSource audioSource;
    public AudioClip shootAudio;
    public float minReloadTime;
    public float maxReloadTime;
    bool canShoot = false;
    float reloadTime;

    public HpSystem hpSystem;

    private Vector3 randomTarget;

    bool firstHit = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        agent.speed = speed;
        //agent.angularSpeed = 1000f;   
        StartCoroutine("RandomPos", 4f);
        StartCoroutine("RandomTargetPos", 3f);
        audioSource = GetComponent<AudioSource>();
        reloadTime = Random.Range(minReloadTime, maxReloadTime);
        randomTarget = new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), Random.Range(-3f, 3f));
        agent.stoppingDistance = 0.1f;
        try{
            if(hpSystem.isEnemy){
                target = FindObjectOfType<TankPatchAI>().transform;
            }
        }catch{}
    
    }

    void Update()
    {
        if(target){
            distanceToPlayer = Vector3.Distance(transform.position, target.position);
            Attack();
        }else if(startTarget){
            if(startTarget.gameObject.active){
                 agent.SetDestination(startTarget.position);
            }
        }

        if(hpSystem.hp < hpSystem.maxHp && !firstHit){
            target = FindObjectOfType<TankMovement>().transform;
            firstHit = true;
        }

         if(transform.localRotation.x * 100f < -15f){
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(-14f, transform.localEulerAngles.y, transform.localRotation.z), 5f * Time.deltaTime);
        }else if(transform.localRotation.x * 100f > 15f){
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(14f, transform.localEulerAngles.y, transform.localRotation.z), 5f * Time.deltaTime);  
        }

        movement.z = speed;
        transform.localPosition = new Vector3(0f, transform.localPosition.y, 0f);
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 0f, transform.localEulerAngles.z);
        Vector3 nextPos = agent.desiredVelocity;
        float angle = Mathf.Abs(Vector3.AngleBetween(transform.forward, nextPos)) * 100f;
       // Debug.Log(angle);
        if(angle < 120f){
            agent.speed = speed;
        }else{
            agent.speed = 0.1f;
        }
      
        truck.rightTruckSpeed = speed * agent.velocity.magnitude;
        truck.leftTruckSpeed = speed * agent.velocity.magnitude;
        TuretSystem();

    }

    void Patrol(){
        
    }

    void Attack(){

        if(distanceToPlayer > 30f){
             agent.SetDestination(target.position);
        }else if(distanceToPlayer <= 3f){
            agent.SetDestination(transform.position);
        }
    }

    void TuretSystem(){

        if(target){
            Vector3 relativePos = (target.position + randomTarget) - turet.position;
            Quaternion turetRotation = Quaternion.LookRotation(relativePos);
            turet.localRotation = Quaternion.RotateTowards(turet.localRotation, Quaternion.Euler(0f, turetRotation.eulerAngles.y - transform.eulerAngles.y, 0f), Time.deltaTime * turetRotationSpeed);

            barrelRot.LookAt(target);
            float gunAngle = ClampGunAngle(barrelRot.localRotation.eulerAngles.x, gunMaxDeltRot) + 90f;

            Quaternion targeGunRot = Quaternion.Euler(gunAngle, barrelRot.localRotation.y, barrelRot.localRotation.z);
            barrel.localRotation = Quaternion.RotateTowards(barrel.localRotation, targeGunRot, Time.deltaTime * 50f);

            // Quaternion targetRot = Quaternion.Euler(transform.localRotation.x, barrelRot.localRotation.eulerAngles.y, transform.localRotation.z);
            // transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetRot, Time.deltaTime * turetRotationSpeed);
            ShootSystem();

        }else{
            turet.localRotation = Quaternion.RotateTowards(turet.localRotation,Quaternion.Euler(0f, 0f, 0f), Time.deltaTime * turetRotationSpeed);
        }
    }

    void ShootSystem(){

        if(!canShoot){
           // Invoke("reload", Random.Range(minReloadTime, maxReloadTime));
           reloadTime -= Time.deltaTime;
           if(reloadTime <= 0f){
               canShoot = true;
           }
        }else{
            canShoot = false;
            poolingShooting.Shoot();
            audioSource.clip = shootAudio;
            audioSource.Play();
            reloadTime = Random.Range(minReloadTime, maxReloadTime);
        }
    }

    float ClampGunAngle(float gunAngle, float delta) {
        float negativeAnge = 360f - delta;
        if (gunAngle > delta && gunAngle < negativeAnge)
            gunAngle = delta;
        else if (gunAngle < negativeAnge && gunAngle > delta) 
            gunAngle = negativeAnge;
        return gunAngle;
    }

    private IEnumerator RandomPos(float waitTime)
    {
        while (true)
        {
            if(distanceToPlayer <= 20f && target){
               // agent.SetDestination(new Vector3(target.position.x + Random.Range(-10, 10), target.position.y, target.position.z + Random.Range(-10, 10)));
                agent.SetDestination(transform.right * Random.Range(-5f, 5f));

            }
            yield return new WaitForSeconds(waitTime);

        }
    }

    private IEnumerator RandomTargetPos(float waitTime)
    {
        while (true)
        {
            if(target){
                randomTarget = new Vector3(Random.Range(-1f, 1f), Random.Range(-3f, 3f), Random.Range(-1f, 1f));
            } 
            yield return new WaitForSeconds(waitTime);

        }
    }

}
