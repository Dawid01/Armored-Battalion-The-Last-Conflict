using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using Photon.Pun;
using Photon.Realtime;


public class ConnectingSystem : MonoBehaviourPunCallbacks
{

    public TMP_InputField nickName;
    public TMP_InputField roomName;
    public TMP_InputField roomId;


    void Start()
    {
        nickName.text = PlayerPrefs.GetString("nickname");
    }

    public void CreateRoom(){
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 5;
        PhotonNetwork.CreateRoom(roomName.text, roomOptions, null);
        SetNickname();

    }

    public void JoinRoom(){
        PhotonNetwork.JoinRoom(roomId.text);
        SetNickname();
    }
    public void JoinRoom(String roomId)
    {
        PhotonNetwork.JoinRoom(roomId);
        SetNickname();
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("MultiGame");
    }


    public void LoadLevel(int level){
        SceneManager.LoadScene(level);
    }


    public void SetNickname(){
        PlayerPrefs.SetString("nickname", nickName.text);
        PhotonNetwork.NickName = nickName.text;
        PlayerPrefs.SetString("nickname", photonView.Owner.NickName);

    }
}
