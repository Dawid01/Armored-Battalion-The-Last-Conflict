using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioOptions : MonoBehaviour
{
    public Slider musicSlider;
    public Slider soundSlider;
    public AudioMixer audioMixer;

    void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVol", 20f);
        audioMixer.SetFloat("musicVol", Mathf.Log10(musicSlider.value) * 20f);
        soundSlider.value = PlayerPrefs.GetFloat("soundsVol", 20f);
        audioMixer.SetFloat("soundsVol", Mathf.Log10(soundSlider.value) * 20f);
        audioMixer.SetFloat("masterVol", 0f);
    }

    void Update()
    {
        
    }


    public void ChangeSound(float value){
        audioMixer.SetFloat("soundsVol", Mathf.Log10(value) * 20f);
        PlayerPrefs.SetFloat("soundsVol", value);
    }

    public void ChangeMusic(float value){
        audioMixer.SetFloat("musicVol", Mathf.Log10(value) * 20f);
        PlayerPrefs.SetFloat("musicVol", value);
    }

}
