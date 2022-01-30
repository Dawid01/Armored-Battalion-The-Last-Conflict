using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System; 

public class GraphicsOptions : MonoBehaviour {


    public GameObject[] stamps;
    private float resolutionValue = 1f;
    int width = 0;
    int height = 0;
    //public Text resolutionText;
    //public Slider resolutionSlider;

    public TMP_Dropdown graphicsDropDown;
    public UnityEngine.Rendering.RenderPipelineAsset[] renderLevels;


    void Start () {

        graphicsDropDown.value = QualitySettings.GetQualityLevel();

        width = PlayerPrefs.GetInt("WidthValue", 0);
        height = PlayerPrefs.GetInt("HeightValue", 0);
        if (width == 0)
        {
            width = Display.main.systemWidth;
            height = Display.main.systemHeight;
            PlayerPrefs.SetInt("WidthValue", width);
            PlayerPrefs.SetInt("HeightValue", height);

        }
        Debug.Log(width + " : " + height);

        Screen.SetResolution(width, height, true);

        setGraphics(PlayerPrefs.GetInt("Graphics", 1));
        // resolutionValue = PlayerPrefs.GetFloat("ResolutionValue", 10);
        // resolutionSlider.value = resolutionValue;
        // resolutionText.text = "Resolution: " + (int)(resolutionValue / 10f * width) + " X " + (int)(resolutionValue / 10f * height);

    }
    

    void Update () {
		
	}

    public void ChangeGraphicsLevel(int value){
        QualitySettings.SetQualityLevel(value);
        QualitySettings.renderPipeline = renderLevels[value];
    }



    public void setGraphics(int graphicsIndex) {

        PlayerPrefs.SetInt("Graphics", graphicsIndex);

        for (int i = 0; i < stamps.Length; i++) {

            GameObject stamp = stamps[i];

            if (i == graphicsIndex)
            {

                stamp.SetActive(true);
            }
            else {

                stamp.SetActive(false);

            }
        }

    }

    // public void ChangeResolutionValue(Slider resolution) {

    //     resolutionValue = resolution.value;
    //     resolutionText.text = "Resolution: " + (int)(resolutionValue / 10f * width) + " X " + (int)(resolutionValue / 10f * height);
    //     PlayerPrefs.SetFloat("ResolutionValue", resolutionValue);
    // }
}
