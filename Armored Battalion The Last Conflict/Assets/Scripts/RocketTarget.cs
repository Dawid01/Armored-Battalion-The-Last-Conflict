using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RocketTarget : MonoBehaviour
{

    public float viewRadius;
	[Range(0,360)]
	public float viewAngle;
	public LayerMask targetMask;
	public LayerMask obstacleMask;
	 public List<Transform> visibleTargets = new List<Transform>();
	public int maxTargetCount = 3;
	public GameObject dynamicTarget;
	public Transform canvas;
	public RectTransform canvasRec;
	private List<RectTransform> dynamicTargets = new List<RectTransform>();
	[HideInInspector] public List<Transform> gunOutList = new List<Transform>();
	[HideInInspector] public List<PoolingShooting> PoolingShootings = new List<PoolingShooting>();

	public Transform gunOuts;
	public GameObject rocket;
	public TowerSystem towerSystem;
	public float reloadTime = 5f;
    public float timer = 0f;
    public Image reloadRight;
	public Image reloadLeft;
	public int amo = 10;
	public TMP_Text amoText;
	public GameObject controllRocket;
	public Transform controllRocketOut;
	private Camera _camera;

	public GameObject joystick;


	void Start() {
		StartCoroutine ("FindTargetsWithDelay", 2f);
		_camera = transform.parent.GetComponent<Camera>();	
		for(int i = 0; i < maxTargetCount; i++){
			GameObject d = Instantiate(dynamicTarget, canvas.position, canvas.rotation);
			d.transform.parent = canvas;
			d.SetActive(false);
			dynamicTargets.Add(d.transform.GetComponent<RectTransform>());
		}

		for(int i = 0; i < gunOuts.childCount; i++){
			gunOutList.Add(gunOuts.GetChild(i));
			PoolingShootings.Add(gunOuts.GetChild(i).GetComponent<PoolingShooting>());
		}
		amoText.text = "" + amo;
	}

    void Update() {
       // FindVisibleTargets();
	    transform.LookAt(towerSystem.gunTargetPosition);

		if (timer > 0f)
        {
            timer -= Time.deltaTime;
            float amount = 1f - timer/reloadTime;
            reloadRight.fillAmount = amount;
			reloadLeft.fillAmount = amount;
        }
        else {
            reloadRight.fillAmount = 1f;
			reloadLeft.fillAmount = 1f;
        }

	 	foreach(RectTransform t in dynamicTargets){
			t.gameObject.SetActive(false);
		}

		for(int i = 0; i < maxTargetCount; i++){
				RectTransform dTarget = null;
				if(i < visibleTargets.Count){
					dTarget = dynamicTargets[i];
				}
				if(i < visibleTargets.Count){
					dTarget = dynamicTargets[i];
					GameObject enemy = visibleTargets[i].gameObject;
					dTarget.gameObject.SetActive(true);   
					Vector2 ViewportPosition = Camera.main.WorldToViewportPoint(enemy.transform.position);
					Vector2 WorldObject_ScreenPosition = new Vector2(
					((ViewportPosition.x * canvasRec.sizeDelta.x) - (canvasRec.sizeDelta.x * 0.5f)),
					((ViewportPosition.y * canvasRec.sizeDelta.y) - (canvasRec.sizeDelta.y * 0.5f)));
					dTarget.anchoredPosition = WorldObject_ScreenPosition;
			}
		}
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
			if(target.GetComponent<HpSystem>()){
				if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2f) {
					float dstToTarget = Vector3.Distance (transform.position, target.position);

					if (!Physics.Raycast (transform.position, dirToTarget, dstToTarget, obstacleMask)) {
							if(visibleTargets.Count < maxTargetCount){
								if(target.GetComponent<HpSystem>().hp > 0){
									visibleTargets.Add(target);
								}
							}                         
					}
				}
			}
		}

	}


	public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal) {
		if (!angleIsGlobal) {
			angleInDegrees += transform.eulerAngles.y;
		}
		return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad),0,Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
	}


	public void Shoot(){

		if(timer <= 0f && visibleTargets.Count > 0 && amo > 0){
			amo--;
			amoText.text = "" + amo;
			timer = reloadTime;
			int bulletForTarget = gunOutList.Count / visibleTargets.Count;
			int targetIndex = 0;
			for(int i = 0; i < gunOutList.Count; i++){
				Transform gunOut = gunOutList[i];
				//RocketBullet bullet = Instantiate(rocket, gunOut.position, gunOut.rotation).GetComponent<RocketBullet>();
				PoolingShooting poolingShooting = PoolingShootings[i];
				poolingShooting.Shoot();
				RocketBullet bullet = poolingShooting.currentBullet;
				bullet.target = visibleTargets[targetIndex];
				bullet.hp = bullet.target.GetComponent<HpSystem>();
				bullet.Invoke("setCanDemage", 0.2f);
				if((i + 1) % bulletForTarget == 0){
					targetIndex++;
				}
			}
		}

	}

	public void ShootControlRocket(){
		if(timer <= 0f && amo > 0){
			amo--;
			amoText.text = "" + amo;
			joystick.SetActive(false);
			timer = reloadTime;
			controllRocketOut.LookAt(towerSystem.gunTargetPosition);
			ControlRocketBullet rocketBullet = Instantiate(controllRocket, controllRocketOut.position, controllRocketOut.rotation).GetComponent<ControlRocketBullet>();
			rocketBullet.playerCanvas = canvas.gameObject;
			rocketBullet.startCamPosition = transform.parent;
			rocketBullet._playerCamera = _camera;
			for(int i = 0; i < gunOutList.Count; i++){
				Transform gunOut = gunOutList[i];
				//RocketBullet bullet = Instantiate(rocket, gunOut.position, gunOut.rotation).GetComponent<RocketBullet>();
				PoolingShooting poolingShooting = PoolingShootings[i];
				poolingShooting.Shoot();
				RocketBullet bullet = poolingShooting.currentBullet;
				rocketBullet.bullets.Add(bullet.gameObject);
				bullet.speed = rocketBullet.speed;
				bullet.target = rocketBullet.gunTargetsList[i];
				bullet.Invoke("setCanDemage", 1f);
			}
			rocketBullet.StartControl();
		}
	}


}
