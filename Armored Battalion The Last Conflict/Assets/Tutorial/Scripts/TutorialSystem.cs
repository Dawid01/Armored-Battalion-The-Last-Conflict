using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class TutorialSystem : MonoBehaviour
{

    public List<TutorialEvent> tutEvents;
    public int eventIndex = 0;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void DoNextEvent(){
        List<UnityEvent> events = tutEvents[eventIndex].events;
        foreach(UnityEvent ev in events){
            ev.Invoke();
        }
        eventIndex++;
    }

    public void loadLevel(int number){
        Time.timeScale = 1f;
        SceneManager.LoadScene(number);
    }
}
