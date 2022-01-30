using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEnemies : MonoBehaviour
{
    public TutorialSystem tutorialSystem;

    void Update()
    {
        if(transform.childCount == 0){
            tutorialSystem.DoNextEvent();
            Destroy(this.gameObject);
        }
    }
}
