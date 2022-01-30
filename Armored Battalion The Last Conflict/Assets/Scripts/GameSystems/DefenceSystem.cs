using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenceSystem : ClassicGame
{

    public List<GameObject> stertObjects;
    public List<TimeEvent> events;
    private float time = 0f;
    void Start()
    {
        foreach(GameObject g in stertObjects){
            g.SetActive(false);
        }
    }

    void Update()
    {
        if(isGameStarted){
            time += Time.deltaTime;
            foreach(TimeEvent timeEvent in events){
                if(timeEvent.time <= time){
                    timeEvent.ev.Invoke();
                    events.Remove(timeEvent);
                }
            }
        }
    }

    public override void StartGame(){
        foreach(GameObject g in stertObjects){
            g.SetActive(true);
        }

        isGameStarted = true;   
    }

}
