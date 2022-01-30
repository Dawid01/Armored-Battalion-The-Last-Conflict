using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using TMPro;

public class RoomIndex : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI name;
    [SerializeField]
    private TextMeshProUGUI players;

    public RoomInfo roomInfo { get; private set;}
    private ConnectingSystem connectingSystem;

    private void Start()
    {
        connectingSystem = FindObjectOfType<ConnectingSystem>();
    }

    public void SetRoomInfo(RoomInfo roomInfo) {
        this.roomInfo = roomInfo;
        name.text = roomInfo.Name;
        players.text = roomInfo.PlayerCount + " / " + roomInfo.MaxPlayers;     
    }

    public void loadRoom() {
        connectingSystem.JoinRoom(roomInfo.Name);
    }
}
