using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ModelViewer : MonoBehaviour , IDragHandler, IPointerUpHandler, IPointerDownHandler
{

	public Transform rotateObj;
    Vector2 inputVector;
    float deltaY;
	float deltaX;
    Vector2 oldPos;
	Quaternion objTouchRot;
	bool isTouched;
	void Update () {

		if(!isTouched && rotateObj.localRotation != objTouchRot)
        {
			rotateObj.localRotation = Quaternion.Lerp(rotateObj.localRotation, objTouchRot, Time.deltaTime * 2f);

		}
    }

    public virtual void OnDrag(PointerEventData ped)
    {
        deltaY = oldPos.x - ped.position.x;
	    deltaX = oldPos.y - ped.position.y;
        rotateObj.localRotation = Quaternion.Euler(objTouchRot.eulerAngles.x + (deltaX/2), objTouchRot.eulerAngles.y + (deltaY/2), 0f);
		isTouched = true;
    }

    public virtual void OnPointerDown(PointerEventData ped)
    {
        oldPos = ped.position;
        OnDrag(ped);
		isTouched = true;
    }

    public virtual void OnPointerUp(PointerEventData ped)
    {
        inputVector = Vector3.zero;
		objTouchRot = Quaternion.Euler(0f,0f,0f);
		isTouched = false;
    }
}
