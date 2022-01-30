using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DestroyAllEnemies : ClassicGame
{

    public int enemiesCount = 0;
    [HideInInspector] public TankAIMovement[] enemies;

    public GameObject targets;

    void Start()
    {   
        gameType = 1; 
        enemies = FindObjectsOfType<TankAIMovement>();   
        enemiesCount = enemies.Length;
    }

    void Update()
    {
            
    }

    public void KillEnemy(){
        enemiesCount--;
        if(enemiesCount == 0){
            Win();
        }
    }

     public override void StartGame(){
        targets.SetActive(true);
    }
}

