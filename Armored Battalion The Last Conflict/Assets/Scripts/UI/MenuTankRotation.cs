using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuTankRotation :  MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{

    [HideInInspector] public float deltaX;
    [HideInInspector] public float deltaY;
    private Vector2 oldPos;
    public Transform tanks;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void OnDrag(PointerEventData ped)
    {
        deltaX = (oldPos.x - ped.position.x);
        oldPos = ped.position;
        tanks.rotation = Quaternion.Euler(0f, tanks.eulerAngles.y + (deltaX / 6) * 1.33f, 0f);

    }

    
    public virtual void OnPointerDown(PointerEventData ped)
    {
        oldPos = ped.position;
    }

    public virtual void OnPointerUp(PointerEventData ped)
    {
        deltaX = 0f;
    }
}
