using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class Menu3DButton : MonoBehaviour, IPointerClickHandler
{

    public Camera _camera;
    public void OnPointerClick(PointerEventData pointerEventData)
    {
    //    RaycastHit hit;
    //     Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        
    //     if (Physics.Raycast(ray, out hit)) {
    //         Button button = hit.transform.GetComponent<Button>();
    //         if(button){
    //             button.Click();
    //         }
    //     }
    }

    private void Update() {
        
     if (Input.touchCount > 0)
        {
            RaycastHit hit;
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out hit)) {
                ButtonMenu button = hit.transform.GetComponent<ButtonMenu>();
                if(button){
                    button.Click();
                }
            }
        }
    }

}
