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
        float scale = PlayerPrefs.GetFloat("ResolutionScale", 1f);
        float width = Display.main.systemWidth * scale;
        float height = Display.main.systemHeight * scale;
        Screen.SetResolution((int)width, (int)height, true);
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
