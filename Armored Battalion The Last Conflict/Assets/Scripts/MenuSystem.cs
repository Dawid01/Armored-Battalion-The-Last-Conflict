using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Cinemachine;

public class MenuSystem : MonoBehaviour
{

    public Toggle shaderToggle;
    public List<Transform> targets;
    public CinemachineVirtualCamera cmMenu; 
    public CinemachineVirtualCamera cmLvl; 
    public CinemachineVirtualCamera cmSettings; 

    void Start()
    {
        int i = PlayerPrefs.GetInt("shaders", 1);
        if(i == 1){
           shaderToggle.isOn = true;
        }else{
            shaderToggle.isOn = false;
        }
    }

    void Update()
    {
        
    }


    public void ChangeCameraTarget(int index){

        switch(index){
            case 0:
                cmMenu.Priority = 11;
                cmLvl.Priority = 10;
                cmSettings.Priority = 10;
                break;
            case 1:
                cmMenu.Priority = 10;
                cmLvl.Priority = 11;
                cmSettings.Priority = 10;
                break;
            case 2:
                cmMenu.Priority = 0;
                cmLvl.Priority = 0;
                cmSettings.Priority = 1;
                break;        
        }

    //    if(index == 0){
    //        cmMenu.Priority = 11;
    //        cmLvl.Priority = 10;
    //    }else{
    //       cmMenu.Priority = 10;
    //       cmLvl.Priority = 11; 
    //    }
    }

    public void loadLevel(int number){
        SceneManager.LoadScene(number);
    }



    public void changeShaders(){

        int i = PlayerPrefs.GetInt("shaders", 1);
        if(i == 1){
            i = 0;
        }else{
            i = 1;
        }
        PlayerPrefs.SetInt("shaders", i);
    }
}
