using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTankTowerSystem : MonoBehaviour
{
    public Transform lookAtObject;
    public Transform _camera;
    public Transform tank;
    public float rotationSpeed = 5f;
    public float gunMaxDeltRot = 15f;
    public LayerMask layerMask;

    public Transform gun;
    public Transform gunRot;
    public Transform barrelRot;

    Vector3 cameraRaycastPostion;
    [HideInInspector] public Vector3 gunTargetPosition;
    public RectTransform target_UI;
    public RectTransform bulletCollision_UI;

    public Canvas canvas;
    RectTransform canvasRec;
    public LayerMask shootDetectionMask;

    private Vector3 startRotation;

    public Joystick joystick;
    public Truck truck;

    private Rigidbody rb;

    private TankMovement tankMovement;

    private Quaternion oldRot;

    void Start()
    {

        startRotation = transform.eulerAngles;
        canvasRec = canvas.GetComponent<RectTransform>();

        cameraRaycastPostion = _camera.position + _camera.forward * 100f;
        gunTargetPosition = gun.position + gun.forward * 100f;
        bulletCollision_UI.gameObject.SetActive(false);
        rb = GetComponent<Rigidbody>();
        oldRot = rb.transform.localRotation;
        tankMovement = GetComponent<TankMovement>();
                FindObjectOfType<ClassicGame>().playerCanvas = canvas.gameObject;


    }

    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(_camera.position, _camera.forward, out hit, 100f, shootDetectionMask))
        {
            cameraRaycastPostion = hit.point;
        }
        else
        {
            cameraRaycastPostion = _camera.position + _camera.forward * 100f;
        }

        if (Physics.Raycast(gun.position, gun.forward, out hit, 100f, shootDetectionMask))
        {
            gunTargetPosition = hit.point;

        }
        else
        {
            gunTargetPosition = gun.position + gun.forward * 100f;
        }
        CharacterController charContr = GetComponent<CharacterController>();
        Vector3 p1 = gun.position;
        Vector3 p2 = p1 + gun.up * 10f;

        if (Physics.CapsuleCast(p1, p2, 0.5f * 0.3f, gun.forward, out hit, 10, shootDetectionMask))
        {
            if (hit.distance <= 10f)
            {
                Vector3 collisionPosition = gun.position + gun.forward * hit.distance;
                Vector2 ViewportPositionCollision = Camera.main.WorldToViewportPoint(collisionPosition);
                Vector2 WorldObject_ScreenPositionCollision = new Vector2(
                ((ViewportPositionCollision.x * canvasRec.sizeDelta.x) - (canvasRec.sizeDelta.x * 0.5f)),
                ((ViewportPositionCollision.y * canvasRec.sizeDelta.y) - (canvasRec.sizeDelta.y * 0.5f)));
                bulletCollision_UI.anchoredPosition = WorldObject_ScreenPositionCollision;
                bulletCollision_UI.gameObject.SetActive(true);
            }
            else
            {
                bulletCollision_UI.gameObject.SetActive(false);
            }
        }
        else
        {
            bulletCollision_UI.gameObject.SetActive(false);

        }


        Vector2 ViewportPosition = Camera.main.WorldToViewportPoint(gunTargetPosition);
        Vector2 WorldObject_ScreenPosition = new Vector2(
        ((ViewportPosition.x * canvasRec.sizeDelta.x) - (canvasRec.sizeDelta.x * 0.5f)),
        ((ViewportPosition.y * canvasRec.sizeDelta.y) - (canvasRec.sizeDelta.y * 0.5f)));
        target_UI.anchoredPosition = Vector2.Lerp(target_UI.anchoredPosition, WorldObject_ScreenPosition, Time.deltaTime * 20f);

        barrelRot.LookAt(cameraRaycastPostion);
        float gunAngle = ClampGunAngle(barrelRot.localRotation.eulerAngles.x, gunMaxDeltRot) + 90f;

        Quaternion targeGunRot = Quaternion.Euler(gunAngle, barrelRot.localRotation.y, barrelRot.localRotation.z);
        gun.localRotation = Quaternion.RotateTowards(gun.localRotation, targeGunRot, Time.deltaTime * 50f);

        float deltaRot = rb.transform.localRotation.y - oldRot.y;
        oldRot = rb.transform.localRotation;
        deltaRot = Mathf.Clamp(deltaRot, -0.01f, 0.01f);

        if(!joystick.IsDrag()){
            Quaternion targetRot = Quaternion.Euler(transform.eulerAngles.x, barrelRot.rotation.eulerAngles.y, transform.eulerAngles.z);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, Time.deltaTime * rotationSpeed);
            tankMovement.movement.z = 1f;
            tankMovement.movement.x = 5f;
    
            truck.rightTruckSpeed = deltaRot;
            truck.leftTruckSpeed = -deltaRot;
            
        }

    }

    float ClampGunAngle(float gunAngle, float delta) {

        float negativeAnge = 360f - delta;
        if (gunAngle > delta && gunAngle < negativeAnge)
        {
            gunAngle = delta;
        }
        else if (gunAngle < negativeAnge && gunAngle > delta) {

            gunAngle = negativeAnge;
        }
        return gunAngle;
    }

}
