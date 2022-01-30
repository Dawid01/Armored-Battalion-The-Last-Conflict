using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Spawner : MonoBehaviour
{
    public GameObject player;
    public Transform[] spawners; 

    void Start()
    {

        Transform spawner = spawners[Random.RandomRange(0, spawners.Length - 1)];
        Vector3 spawnPoint = spawner.position + new Vector3(Random.RandomRange(-5f, 5f), spawner.position.y, Random.RandomRange(-5f, 5f));
        PhotonNetwork.Instantiate(player.name, spawnPoint, spawner.rotation);
    }

    void Update()
    {
        
    }
}
