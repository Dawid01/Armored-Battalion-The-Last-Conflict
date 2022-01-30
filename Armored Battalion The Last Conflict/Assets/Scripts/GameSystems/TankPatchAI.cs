using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TankPatchAI : MonoBehaviour
{

    public Transform patch;
    public List<Transform> points;
    public NavMeshAgent navMeshAgent;
    public Transform target;

    public int index = 0;



    private void Start() {
    }


    private void OnEnable() {
        
        for(int i = 0; i < patch.childCount; i++){
                points.Add(patch.GetChild(i));
        }
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.destination = points[index].position;
        StartCoroutine ("CheckDistance", 0.5f);
    }
    
    void Update()
    {
        // if(Vector2.Distance(transform.position, navMeshAgent.destination) <= 0.1f){
        //     SetNextDestination();
        // }
    }


    IEnumerator CheckDistance(float delay) {
		while (true) {
			yield return new WaitForSeconds (delay);
			if(Vector3.Distance(transform.position, points[index].position) <= 1f){
              index++;
              SetNextDestination();
            }
		}
	}

    void SetNextDestination(){
        navMeshAgent.destination = points[index].position;
    }
}
