using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class Menu : MonoBehaviourPunCallbacks
{

    public GameObject mainButtons;
    public GameObject settings;

    public void ActiveSettings(){
        mainButtons.SetActive(!mainButtons.active);
        settings.SetActive(!settings.active);
    }

    public void loadMultiplayerLoby()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
        SceneManager.LoadScene("Connecting");
    }

    public void loadLevel(int number){
        Time.timeScale = 1f;
        SceneManager.LoadScene(number);
    }

    public void ExitGame(){
        Application.Quit();
    }

}
