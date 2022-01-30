using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MenuPauseSystem : MonoBehaviour
{

    public GameObject buttons;
    public GameObject settings;

    public GameObject pauseBtn;
    public BlurCamera blurCamera;

    private GameObject playerCanvas;
    private GameObject joystick;

    public AudioMixer audioMixer;
    public AudioSource musicAudio;

    public GameObject askForRestert;
    public GameObject askForExit;


    void Start()
    {
        audioMixer.SetFloat("masterVol", 0f);
    }

    void Update()
    {
        
    }

    public void PauseGame(){
        if(!playerCanvas){
            playerCanvas = GameObject.FindWithTag("PlayerCanvas");
            joystick = playerCanvas.transform.GetChild(1).GetChild(0).gameObject;
        }
        joystick.SetActive(false);
        playerCanvas.SetActive(false);
        Time.timeScale = 0f;
        pauseBtn.SetActive(false);
        buttons.SetActive(true);
        audioMixer.SetFloat("masterVol", -80f);
        musicAudio.Pause();
       // blurCamera.SetBlur(1);
    }

    public void ResumeGame(){
        playerCanvas.SetActive(true);
        Time.timeScale = 1f;
        pauseBtn.SetActive(true);
        buttons.SetActive(false);
        audioMixer.SetFloat("masterVol", 0f);
        musicAudio.Play();
      //  blurCamera.SetBlur(0);
    }

    public void loadLevel(int number){
        Time.timeScale = 1f;
        SceneManager.LoadScene(number);
    }

    public void RestartLevel(){
        string currentSceneName = SceneManager.GetActiveScene().name;
        Time.timeScale = 1f;
        SceneManager.LoadScene(currentSceneName);
    }


    public void ActiveSettings(){
        buttons.SetActive(!buttons.active);
        settings.SetActive(!settings.active);
    }

    public void AskForRestert(){
        askForRestert.SetActive(!askForRestert.active);
    }

    public void AskForExit(){
        askForExit.SetActive(!askForExit.active);
    }
}
