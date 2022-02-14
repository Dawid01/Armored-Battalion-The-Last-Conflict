using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesPatch : MonoBehaviour
{
    public List<TankAIMovement> enemies;
    public void setEnemiesTarget(Transform target) {
        foreach (TankAIMovement enemy in enemies) {
            enemy.target = target;
        }
    }
}
