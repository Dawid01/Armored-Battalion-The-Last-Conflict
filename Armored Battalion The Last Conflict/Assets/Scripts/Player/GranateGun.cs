using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranateGun : MonoBehaviour
{
    public Rigidbody bulletRb;
	public GameObject bullet;
	public Transform target;

	public float h = 1;
	public float gravity = -10;
	public LineRenderer lineRenderer;

	void Start() {
	}

	void Update() {
		DrawPath();	
	}

	private void OnEnable() {
		lineRenderer.enabled = true;
	}


	public void Shoot() {
		var b = Instantiate(bullet, transform.position, transform.rotation);
		Rigidbody rb = b.GetComponent<Rigidbody>();
		rb.velocity = CalculateLaunchData ().initialVelocity;
		lineRenderer.enabled = false;

	}

	LaunchData CalculateLaunchData() {
		float displacementY = target.position.y - transform.position.y;
		Vector3 displacementXZ = new Vector3 (target.position.x - transform.position.x, 0, target.position.z - transform.position.z);
		float time = Mathf.Sqrt(-2*h/gravity) + Mathf.Sqrt(2*(displacementY - h)/gravity);
		Vector3 velocityY = Vector3.up * Mathf.Sqrt (-2 * gravity * h);
		Vector3 velocityXZ = displacementXZ / time;

		return new LaunchData(velocityXZ + velocityY * -Mathf.Sign(gravity), time);
	}

	void DrawPath() {
		LaunchData launchData = CalculateLaunchData ();
		Vector3[] positions = new Vector3[31];
		positions[0] = transform.position;
		lineRenderer.positionCount = 31;
		int resolution = 30;
		for (int i = 1; i <= resolution; i++) {
			float simulationTime = i / (float)resolution * launchData.timeToTarget;
			Vector3 displacement = launchData.initialVelocity * simulationTime + Vector3.up *gravity * simulationTime * simulationTime / 2f;
			Vector3 drawPoint = transform.position + displacement;
			positions[i] = drawPoint;
		}
		lineRenderer.SetPositions(positions);
	}

	struct LaunchData {
		public readonly Vector3 initialVelocity;
		public readonly float timeToTarget;

		public LaunchData (Vector3 initialVelocity, float timeToTarget)
		{
			this.initialVelocity = initialVelocity;
			this.timeToTarget = timeToTarget;
		}
		
	}
    

}
