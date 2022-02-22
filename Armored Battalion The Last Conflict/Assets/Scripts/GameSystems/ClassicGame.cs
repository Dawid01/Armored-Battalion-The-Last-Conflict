using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class ClassicGame : MonoBehaviour
{

    public GameObject winCanvas;
    public GameObject defeatCanvas;
    public GameObject pauseButton;

    public int gameType;
    [HideInInspector] public GameObject playerCanvas;
    [HideInInspector] public TankMovement tankMovement;


    public bool isGameStarted = false;

    void Start()
    {
        

    }

    void Update()
    {
        
    }

    public void Win(){
        winCanvas.SetActive(true);
        pauseButton.SetActive(false);
        playerCanvas.SetActive(false);
    }

    public void Defeat(){
        tankMovement.movement = Vector3.zero;
        defeatCanvas.SetActive(true);
        pauseButton.SetActive(false);
        playerCanvas.SetActive(false);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public abstract void StartGame();
}
