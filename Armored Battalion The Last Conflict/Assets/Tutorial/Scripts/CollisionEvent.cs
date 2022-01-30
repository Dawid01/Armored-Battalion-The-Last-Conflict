using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionEvent : MonoBehaviour
{
    public TutorialSystem tutorialSystem;

    private void OnTriggerEnter(Collider other) {
        tutorialSystem.DoNextEvent();
    }
}
