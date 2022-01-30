using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ControlRocketBullet : MonoBehaviour
{
    public float speed;
    public float curveSpeed;

    public Joystick joystick;

    public GameObject _canvas;
    [HideInInspector] public Transform startCamPosition;
    [HideInInspector] public GameObject playerCam;
    [HideInInspector] public GameObject playerCanvas;
    private Transform cam;
    public List<Transform> gunTargetsList;

    [HideInInspector] public List<GameObject> bullets;

    public RectTransform verticalInfo;

    public TMP_Text heightText;
    public TMP_Text degressText;

    private Camera _camera;
    [HideInInspector] public Camera _playerCamera;

    private float controlTime = 10f;
    public Image powerImage;


    void Start()
    {
       _camera = cam.GetComponent<Camera>(); 
       
    }

    void Update()
    {
        _camera.fieldOfView = Mathf.MoveTowards(_camera.fieldOfView, 100f, Time.deltaTime * 50f);

        verticalInfo.localEulerAngles = new Vector3(0f, 0f, -transform.localEulerAngles.z);
        degressText.rectTransform.localEulerAngles = new Vector3(0f, 0f, transform.localEulerAngles.z);
        Vector3 right = transform.right;
        right.y = 0;
        Vector3 fwd = Vector3.Cross(right, Vector3.up).normalized;
        float pitch = Vector3.Angle(fwd, transform.forward) * Mathf.Sign(transform.forward.y);
        degressText.text = "" + Mathf.RoundToInt(10f * Mathf.RoundToInt(pitch / 10f));
        heightText.text = "" + Mathf.RoundToInt(transform.position.y * 5f) + " [m]";

        cam.localPosition = Vector3.Lerp(cam.localPosition, Vector3.zero, Time.deltaTime * 3f);
        cam.localRotation = Quaternion.Lerp(cam.localRotation, Quaternion.Euler(0f,0f,0f), Time.deltaTime * 10f);

        transform.Translate(0f, 0f, speed * Time.deltaTime);
        transform.Rotate(-joystick.getVertical() * curveSpeed * Time.deltaTime, joystick.getHorizontal() * curveSpeed * Time.deltaTime, -joystick.getHorizontal() * curveSpeed * Time.deltaTime);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(transform.localEulerAngles.x, transform.localEulerAngles.y, 0f), Time.deltaTime * 3f);
   
        bullets.RemoveAll(item => item.active == false);
        if(bullets.Count == 0){
            Finish(0);
        }

        controlTime -= Time.deltaTime;
        powerImage.fillAmount = controlTime / 10f; 
        if(controlTime <= 0f){
            Finish(0f);
        }
    }

    public void StartControl(){
       // playerCam.SetActive(false);
        playerCanvas.SetActive(false);
        _playerCamera.enabled = false;
        cam = transform.GetChild(0).GetChild(0);
        cam.position = startCamPosition.position;
        cam.rotation = startCamPosition.rotation;
        
    }

    public void Finish(float timetoDestroy){
        
        _playerCamera.enabled = true;
        Destroy(_canvas);
        cam.gameObject.SetActive(false);
        playerCanvas.SetActive(true);
        Destroy(this.gameObject, timetoDestroy);
        // foreach(GameObject b in bullets){
        //     Destroy(b);
        // }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag != "Player"){
           //  Finish();
        }
    }
}
