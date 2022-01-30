using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{

    private Image bgImg;
    private Image joystickImg;
    public Vector3 inputVector;
    public float joyDist = 2f;
    public float delta;
    private bool isDrag;



    void Start()
    {

        bgImg = GetComponent<Image>();
        joystickImg = transform.GetChild(0).GetComponent<Image>();
    }

    void Update()
    {
    }

    public virtual void OnDrag(PointerEventData ped)
    {

        Vector2 position = Vector2.zero;

        if (ped != null)
        {
            try{
                if (RectTransformUtility.ScreenPointToLocalPointInRectangle(bgImg.rectTransform, ped.position, ped.pressEventCamera, out position))
                {
                    position.x = (position.x / bgImg.rectTransform.sizeDelta.x);
                    position.y = (position.y / bgImg.rectTransform.sizeDelta.y);
                    inputVector = new Vector3(position.x * 2, 0, position.y * 2);
                    inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

                    delta = Vector2.Distance(new Vector2(position.x, position.y), new Vector3(inputVector.x * (bgImg.rectTransform.sizeDelta.x / joyDist)
                                    , inputVector.z * (bgImg.rectTransform.sizeDelta.y / joyDist))) / 100f;

                    joystickImg.rectTransform.anchoredPosition =
                        new Vector3(inputVector.x * (bgImg.rectTransform.sizeDelta.x / joyDist)
                                    , inputVector.z * (bgImg.rectTransform.sizeDelta.y / joyDist));

                } 
            }catch{}
        isDrag = true;
        }
    }


    public virtual void OnPointerDown(PointerEventData ped)
    {
        OnDrag(ped);
    }

    public virtual void OnPointerUp(PointerEventData ped)
    {
        inputVector = Vector3.zero;
        joystickImg.rectTransform.anchoredPosition = Vector3.zero;
        delta = 0f;
        isDrag = false;

    }

    public void ActiveJoystick()
    {
        PointerEventData ped = new PointerEventData(EventSystem.current);
        OnPointerDown(ped);
    }

    public float getVertical()
    {
        if (!inputVector.Equals(Vector3.zero))
            return inputVector.z;
        else
            return Input.GetAxis("Vertical");
    }

    public float getHorizontal()
    {
        if (!inputVector.Equals(Vector3.zero))
            return inputVector.x;
        else
            return Input.GetAxis("Horizontal");
    }

    public float getVerticalNormalized()
    {
        return inputVector.normalized.z;

    }

    public float getHorizontalNormalized()
    {
        return inputVector.normalized.x;
    }

    public bool IsDrag()
    {
        return isDrag;
    }

    public void SetDrag(bool drag)
    {
        isDrag = drag;
    }
}
