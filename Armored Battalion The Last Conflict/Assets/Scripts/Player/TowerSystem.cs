using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerSystem : MonoBehaviour
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
    [HideInInspector] public Vector3 hitPosition;
    public bool isRocketTower;


    void Start()
    {

        startRotation = transform.localEulerAngles;
        canvasRec = canvas.GetComponent<RectTransform>();

        cameraRaycastPostion = _camera.position + _camera.forward * 100f;
        gunTargetPosition = gun.position + gun.forward * 100f;
        bulletCollision_UI.gameObject.SetActive(false);
        FindObjectOfType<ClassicGame>().playerCanvas = canvas.gameObject;

    }

    void Update()
    {
        //cameraRaycastPostion = _camera.position + _camera.forward * 100f;

        RaycastHit hit;

        if (Physics.Raycast(_camera.position, _camera.forward, out hit, 100f, shootDetectionMask) && !isRocketTower) {
            cameraRaycastPostion = hit.point;
        }
        else
        {
            cameraRaycastPostion = _camera.position + _camera.forward * 100f;
        }

        if (Physics.Raycast(gun.position, gun.forward, out hit, 100f, shootDetectionMask) && !isRocketTower)
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
            if (hit.distance <= 10f && bulletCollision_UI)
            {
                Vector3 collisionPosition = gun.position + gun.forward * hit.distance;
                Vector2 ViewportPositionCollision = Camera.main.WorldToViewportPoint(collisionPosition);
                Vector2 WorldObject_ScreenPositionCollision = new Vector2(
                ((ViewportPositionCollision.x * canvasRec.sizeDelta.x) - (canvasRec.sizeDelta.x * 0.5f)),
                ((ViewportPositionCollision.y * canvasRec.sizeDelta.y) - (canvasRec.sizeDelta.y * 0.5f)));
                bulletCollision_UI.anchoredPosition = Vector2.Lerp(bulletCollision_UI.anchoredPosition, WorldObject_ScreenPositionCollision, Time.deltaTime * 10f);
                bulletCollision_UI.gameObject.SetActive(true);
            }
            else
            {
                bulletCollision_UI.gameObject.SetActive(false);
            }
        }
        else
        {
            if (bulletCollision_UI)
            {
                bulletCollision_UI.gameObject.SetActive(false);
            }

        }


        Vector2 ViewportPosition = Camera.main.WorldToViewportPoint(gunTargetPosition);
        Vector2 WorldObject_ScreenPosition = new Vector2(
        ((ViewportPosition.x * canvasRec.sizeDelta.x) - (canvasRec.sizeDelta.x * 0.5f)),
        ((ViewportPosition.y * canvasRec.sizeDelta.y) - (canvasRec.sizeDelta.y * 0.5f)));
        target_UI.anchoredPosition = Vector2.Lerp(target_UI.anchoredPosition, WorldObject_ScreenPosition, Time.deltaTime * 10f);

        barrelRot.LookAt(cameraRaycastPostion);
        float gunAngle = ClampGunAngle(barrelRot.localRotation.eulerAngles.x, gunMaxDeltRot) + 90f;

        Quaternion targeGunRot = Quaternion.Euler(gunAngle, barrelRot.localRotation.y, barrelRot.localRotation.z);
        gun.localRotation = Quaternion.RotateTowards(gun.localRotation, targeGunRot, Time.deltaTime * 50f);

        //Quaternion targetRot = Quaternion.Euler(transform.localRotation.x, barrelRot.localRotation.eulerAngles.y, transform.localRotation.z);
        Quaternion targetRot = Quaternion.Euler(startRotation.x, barrelRot.localRotation.eulerAngles.y, startRotation.z);
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetRot, Time.deltaTime * rotationSpeed);

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
