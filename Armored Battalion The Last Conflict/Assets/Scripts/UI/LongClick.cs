using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class LongClick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{

    public UnityEvent onClickEvent;
    public UnityEvent onLongClickEvent;

    public RectTransform cancelButton;

    public float timeToLongClick = 0.2f;
    private float timer = 0f;
    private bool isClicked = false;
    private float startY = 0;

    private RectTransform rect;


    private void Awake() {
        rect = GetComponent<RectTransform>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        if(timer >= 0){
            timer -= Time.deltaTime;
        }
        if(timer <= 0 && isClicked){
            cancelButton.gameObject.SetActive(true);
        }
    }


    public virtual void OnDrag(PointerEventData ped)
    {
      // float distance = cancelButton.rect.position.y - ped.position.y;
    //    float distance = ped.position.y - rect.position.y;
    //    float distance = cancelButton.position.y - rect.position.y;
    //    Debug.Log("Y: " + distance);
    }


    public virtual void OnPointerDown(PointerEventData ped)
    {
        timer = timeToLongClick;
        isClicked = true;
        startY = ped.position.y;
        OnDrag(ped);
    }

    public virtual void OnPointerUp(PointerEventData ped)
    {
       isClicked = false; 
       cancelButton.gameObject.SetActive(false);     
       float distance = cancelButton.position.y - rect.position.y;
       float delta = ped.position.y - rect.position.y;
        if(delta < (distance - cancelButton.rect.size.y/2)){
            isClicked = false;
            if(timer <= 0){
                 onLongClickEvent.Invoke();
            }else{
                onClickEvent.Invoke();
            }
        }
    }
}
