using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RoomList : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Transform content;
    [SerializeField]
    private RoomIndex roomIndexPrefab;

    private List<RoomIndex> roomIndexList = new List<RoomIndex>();

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (RoomInfo room in roomList) {

            if (room.RemovedFromList)
            {
                int index = roomIndexList.FindIndex(x => x.roomInfo.Name == room.Name);
                if (index != -1) {
                    RoomIndex roomIndex = roomIndexList[index];
                    roomIndexList.RemoveAt(index);
                    Destroy(roomIndex.gameObject);
                }
                
            }
            else
            {
                int index = roomIndexList.FindIndex(x => x.roomInfo.Name == room.Name);
                if (index != -1)
                {
                    roomIndexList[index].SetRoomInfo(room);
                }
                else {
                    RoomIndex roomIndex = Instantiate(roomIndexPrefab, content);
                    roomIndex.SetRoomInfo(room);
                    roomIndexList.Add(roomIndex);
                }

            }
        }
    }
}
