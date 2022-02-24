using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TouchPanel : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler,  IPointerClickHandler
{
    public Transform cameraPath;
    public Transform cameraY;

    public float upRotClamp;
    public float downRotClamp;
    [HideInInspector] public float deltaX;
    [HideInInspector] public float deltaY;
    private Vector2 oldPos;
    private int touchCount = 0;
    private PointerEventData firstPonter;

    private Camera cam;
    private float fieldOfView = 60f;
    private float zoom = 20f;
    public bool isZoom = false;

    float lastTimeClick = -100f;
    private float sensitive = 1f;

    private void Start() {
        cam = cameraY.GetChild(0).GetChild(0).GetComponent<Camera>();
        sensitive = 2f - PlayerPrefs.GetFloat("ResolutionScale", 1f);
    }

    void Update() {

        if(!isZoom){
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, fieldOfView, Time.deltaTime * 10f);
        }else{
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, zoom, Time.deltaTime * 10f);
        }
  
    }

    public virtual void OnPointerClick(PointerEventData eventData){
       float currentTimeClick = eventData.clickTime;
        if (Mathf.Abs(currentTimeClick - lastTimeClick) < 0.2f){
            isZoom = !isZoom;
            lastTimeClick = -100f;
        }
        else{
            lastTimeClick = currentTimeClick;
        }
    }

    public virtual void OnDrag(PointerEventData ped)
    {
        deltaX = oldPos.x - firstPonter.position.x;
        if(isZoom){
            deltaX = deltaX / 2f;
        }
        deltaY = oldPos.y - firstPonter.position.y;
       
        float newCamerRotX = cameraY.eulerAngles.x + (deltaY / 6) * 1.33f * sensitive;
        if (newCamerRotX > 180 && newCamerRotX < 360 - upRotClamp){
            newCamerRotX = 360 - upRotClamp;
        }
        else if (newCamerRotX < 180 && newCamerRotX > downRotClamp){
            newCamerRotX = downRotClamp;
        }
        cameraY.localRotation = Quaternion.Euler(newCamerRotX, 0f, 0f); 
        cameraPath.rotation = Quaternion.Euler(0f, cameraPath.eulerAngles.y - (deltaX / 6) * 1.33f * sensitive, 0f);
        oldPos = firstPonter.position;

    }

    public virtual void OnPointerDown(PointerEventData ped)
    {
        if (touchCount == 0)
        {
            firstPonter = ped;
            oldPos = ped.position;
            OnDrag(firstPonter);
            touchCount++;
        }

    }

    public virtual void OnPointerUp(PointerEventData ped)
    {
        deltaX = 0f;
        deltaY = 0f;
        touchCount = 0;
    }


}
