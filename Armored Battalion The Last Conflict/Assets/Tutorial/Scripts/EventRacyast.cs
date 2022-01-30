using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventRacyast : MonoBehaviour
{
    public TutorialSystem tutorialSystem;

    void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, 100f)){
            if(hit.transform.tag == "EventWall"){
                tutorialSystem.DoNextEvent();
                this.enabled = false;
            }
        }
    }
}
