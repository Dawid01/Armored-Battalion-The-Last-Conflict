using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonMenu : MonoBehaviour
{
    public UnityEvent _event;

    public void Click(){
        _event.Invoke();
    }
}
