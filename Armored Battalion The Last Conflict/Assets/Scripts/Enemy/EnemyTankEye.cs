using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTankEye : MonoBehaviour {

	public float viewRadius;
	[Range(0,360)]
	public float viewAngle;
	public LayerMask targetMask;
	public LayerMask obstacleMask;
	public List<Transform> visibleTargets = new List<Transform>();
	public TankAIMovement enemyAI;
	public HpSystem enemyHp;

	void Start() {
		StartCoroutine ("FindTargetsWithDelay", 1f);	
	}

	private void OnEnable() {
		StopAllCoroutines();
		StartCoroutine ("FindTargetsWithDelay", 1f);
	}

    void Update() {

        if (enemyHp.hp != enemyHp.maxHp) {
            viewAngle = 360f;
            viewRadius = 200f;
        }
	//	FindVisibleTargets();

    }

	IEnumerator FindTargetsWithDelay(float delay) {
		while (true) {
			yield return new WaitForSeconds (delay);
			FindVisibleTargets ();
		}
	}

	void FindVisibleTargets() {
		visibleTargets.Clear();
		Collider[] targetsInViewRadius = Physics.OverlapSphere (transform.position, viewRadius, targetMask);

		for (int i = 0; i < targetsInViewRadius.Length; i++) {
			Transform target = targetsInViewRadius [i].transform;
			Vector3 dirToTarget = (target.position - transform.position).normalized;
			if (Vector3.Angle (transform.forward, dirToTarget) < viewAngle/ 2f) {
				float dstToTarget = Vector3.Distance (transform.position, target.position);

				if (!Physics.Raycast (transform.position, dirToTarget, dstToTarget, obstacleMask)) {
					visibleTargets.Add(target);
                    viewAngle = 360f;
                    viewRadius = 200f;
                }
            }
		}

		if(visibleTargets.Count != 0){
			enemyAI.target = visibleTargets[visibleTargets.Count - 1];
			//enemyAI.target = visibleTargets[Random.Range(0, visibleTargets.Count - 1)];
		}
	}


	public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal) {
		if (!angleIsGlobal) {
			angleInDegrees += transform.eulerAngles.y;
		}
		return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad),0,Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
	}


}
