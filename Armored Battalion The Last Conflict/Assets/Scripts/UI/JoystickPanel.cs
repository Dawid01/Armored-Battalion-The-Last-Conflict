using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JoystickPanel : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    public Vector3 inputVector;
    public GameObject joyObcjet;
    private Joystick joystick;
    public Image joyImage;

    void Start()
    {
        joystick = joyObcjet.GetComponent<Joystick>();
        joyImage = joyObcjet.GetComponent<Image>();
        joyObcjet.SetActive(false);
    }

    public virtual void OnDrag(PointerEventData ped)
    {
        inputVector = ped.position;
        joystick.OnDrag(ped);
    }

    public virtual void OnPointerDown(PointerEventData ped)
    {
        joyObcjet.SetActive(true);
        joyImage.rectTransform.anchoredPosition = GetLocalPed(ped);
        joystick.OnDrag(ped);
        inputVector = ped.position;
        OnDrag(ped);
    }

    public virtual void OnPointerUp(PointerEventData ped)
    {
        joystick.inputVector = Vector2.zero;
        joystick.SetDrag(false);
        joyObcjet.SetActive(false);
    }


    Vector2 GetLocalPed(PointerEventData ped)
    {
        Vector2 localPed;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(GetComponent<RectTransform>(), ped.position, ped.pressEventCamera, out localPed);
        return localPed;
    }

}

