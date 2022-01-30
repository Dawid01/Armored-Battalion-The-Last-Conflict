using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;

public class TankMovement : MonoBehaviour
{

    public float speed = 5f;
    public float rotationSpeed = 1f;

    public Joystick joystick;

    [HideInInspector] public Vector3 movement = Vector3.zero;
    [HideInInspector] public Rigidbody rb;
    float moveZ = 0;
    public Transform cameraPath;
    public Transform cameraTarget;
    public Truck truck;

    private NavMeshObstacle obstacle;

    public AudioSource engineAudio;
    public AudioClip engineStop;
    public AudioClip engineDrive;

    public PhotonView photonView;

    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        obstacle = GetComponent<NavMeshObstacle>();
        FindObjectOfType<ClassicGame>().tankMovement = this;
        engineAudio = GetComponent<AudioSource>();
        engineAudio.clip = engineStop;
        engineAudio.Play();
        if(photonView || !photonView.IsMine){
            this.enabled = false;
         }
       
    }

    private void Update()
    {
    if(!photonView || photonView.IsMine){
        cameraPath.position = cameraTarget.position;
        if(!joystick.IsDrag()){
            obstacle.radius = 0f;
        }else{
            obstacle.radius = 0f;
        }

        if(joystick.IsDrag()){
            engineAudio.clip = engineStop;
        }else{
            engineAudio.clip = engineDrive;
        }
        if(!engineAudio.isPlaying){
            engineAudio.Play();
        }
    }
  
    }

    void FixedUpdate()
    {
        if(!photonView || photonView.IsMine){
            movement.x = joystick.getHorizontal() * speed;
            movement.z = joystick.getVertical() * speed;

            float driveDirection = 1;
            if (movement.z >= -0.5)
            {
                driveDirection = 1f;
            }
            else {
                driveDirection = -1f;
            }

            moveZ = Mathf.Lerp(moveZ, movement.z, Time.deltaTime * 3f);
            if(transform.localRotation.x * 100f < -25f){
                transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(-24f, transform.localEulerAngles.y, transform.localRotation.z), 5f * Time.deltaTime);
                moveZ = 0f;
            }else if(transform.localRotation.x * 100f > 25f){
            // moveZ = 0f;
                transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(24f, transform.localEulerAngles.y, transform.localRotation.z), 5f * Time.deltaTime);  
            }
            Vector3 forwardMove = transform.forward * moveZ;
            
            rb.MovePosition(transform.position + forwardMove * Time.deltaTime);
            Quaternion deltaRotation = Quaternion.Euler(new Vector3(0f, joystick.getHorizontal() * driveDirection * 60f, 0f) * Time.deltaTime * rotationSpeed);
            rb.MoveRotation(rb.rotation * deltaRotation);
            float truckScale = moveZ / speed;
            // truck.rightTruckSpeed = (movement.normalized.z - movement.normalized.x) / 1000f * driveDirection;
            // truck.leftTruckSpeed = (movement.normalized.z + movement.normalized.x) / 1000f * driveDirection;

            truck.rightTruckSpeed = ((joystick.getVertical() + joystick.getHorizontal()) / 1000f * driveDirection) * driveDirection;
            truck.leftTruckSpeed = ((joystick.getVertical() - joystick.getHorizontal()) / 1000f * driveDirection) * driveDirection;
        
        
            // float x = transform.localEulerAngles.x;
            
            // if(x < 330f && x > 45f){

            // }
        }
    }
}
