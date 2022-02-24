using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System; 

public class GraphicsOptions : MonoBehaviour {


    public GameObject[] stamps;

    public TextMeshProUGUI resolutionText;

    public TMP_Dropdown graphicsDropDown;
    public UnityEngine.Rendering.RenderPipelineAsset[] renderLevels;
    public Slider resolutionSlider;




    void Start () {

        graphicsDropDown.value = QualitySettings.GetQualityLevel();
        setGraphics(PlayerPrefs.GetInt("Graphics", 1));
        float scale = PlayerPrefs.GetFloat("ResolutionScale", 1f);
        float width = Display.main.systemWidth * scale;
        float height = Display.main.systemHeight * scale;
        Screen.SetResolution((int)width, (int)height, true);
        resolutionSlider.value = scale;
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

     public void ChangeResolutionValue(Slider resolution) {

         float resolutionScale = resolution.value;
         resolutionText.text = "Resolution " + (int)(resolution.value * 100f) + "%";
         PlayerPrefs.SetFloat("ResolutionScale", resolutionScale);
         float width = Display.main.systemWidth * resolutionScale;
         float height = Display.main.systemHeight * resolutionScale;
         Screen.SetResolution((int)width, (int)height, true);
         //resolutionSlider.value = resolutionScale;

    }

    public void UpdateResolutionValue(Slider resolution)
    {
        float resolutionScale = resolution.value;
        resolutionText.text = "Resolution " + (int)(resolution.value * 100f) + "%";
    }
}
