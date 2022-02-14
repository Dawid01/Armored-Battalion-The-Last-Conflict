using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderOptions : MonoBehaviour
{

    void Awake() {
        if(PlayerPrefs.GetInt("shaders", 1) == 0){
            //Screen.SetResolution(Screen.currentResolution.width/2, Screen.currentResolution.height/2, FullScreenMode.ExclusiveFullScreen);
        }else{
            // Screen.SetResolution(Mathf.RoundToInt(Screen.currentResolution.width * 0.9f), Screen.currentResolution.height/2, FullScreenMode.ExclusiveFullScreen);
        }
        // int width = PlayerPrefs.GetInt("WidthValue", 0);
        // int height = PlayerPrefs.GetInt("HeightValue", 0);
        // Screen.SetResolution(width, height, FullScreenMode.ExclusiveFullScreen);
        
    }
    void Start()
    {
        int width = PlayerPrefs.GetInt("WidthValue", 0);
        int height = PlayerPrefs.GetInt("HeightValue", 0);
        if (width == 0)
        {
            width = Display.main.systemWidth;
            height = Display.main.systemHeight;
            PlayerPrefs.SetInt("WidthValue", width);
            PlayerPrefs.SetInt("HeightValue", height);

        }

        Screen.SetResolution(width, height, true);
        if(PlayerPrefs.GetInt("shaders", 1) == 0){
            gameObject.SetActive(false);
        }
        
    }

    void Update()
    {
        
    }
}
