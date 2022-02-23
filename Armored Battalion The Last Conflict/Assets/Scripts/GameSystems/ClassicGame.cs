using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class ClassicGame : MonoBehaviour
{

    public GameObject winCanvas;
    public GameObject tryAgainCanvas;
    public GameObject defeatCanvas;
    public GameObject pauseButton;
    public TankSelectSystem tankSelectSystem;

    public int gameType;
    [HideInInspector] public GameObject playerCanvas;
    [HideInInspector] public TankMovement tankMovement;


    public bool isGameStarted = false;

    void Start()
    {
        //tankSelectSystem = FindObjectOfType<TankSelectSystem>();

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
        if (tankSelectSystem.isGameOver())
        {
            defeatCanvas.SetActive(true);
            tryAgainCanvas.SetActive(false);
        }
        else {
            defeatCanvas.SetActive(false);
            tryAgainCanvas.SetActive(true);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public abstract void StartGame();
}
