using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class ShootSystem : MonoBehaviourPun
{

    public PoolingShooting poolingShooting;
    public Transform shootOut;
    public GameObject bullet;
    public ShakeCamera shakeCamera;
    public Transform gunBody;
    public float reloadTime = 1f;
    private float timer = 0f;
    public Image leftReload;
    public Image rightReload;
    public ParticleSystem sparks;
    public float pushPower = 20f;

    private Rigidbody tankBody;

    public AudioSource audioSource;
    public AudioClip shootSound;
    public bool isMultiplayer = false;
    public PhotonView photonView;


    private void Awake()
    {
        sparks.Stop();
    }

    void Start()
    {
        tankBody = transform.parent.parent.GetComponent<Rigidbody>();
        if (isMultiplayer)
        {
            photonView = GetComponent<PhotonView>();
            /*if (photonView.IsMine)
            {
                photonView.RPC("Shoot", RpcTarget.AllBuffered);
            }*/
        }
    }

    void Update()
    {
        if (timer > 0f)
        {
            timer -= Time.deltaTime;
            float amount = 1f - timer/reloadTime;
            //leftReload.fillAmount = amount;
            rightReload.fillAmount = amount;
        }
        else {

           // leftReload.fillAmount = 1f;
            rightReload.fillAmount = 1f;
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }

    }



    public void MultiplayerShoot() {
        if (photonView.IsMine)
        {
            photonView.RPC("Shoot", RpcTarget.AllBuffered);
        }
    }

    [PunRPC]
    public void Shoot() {

        if (timer <= 0f)
        {
            timer = reloadTime;
            sparks.Play();
            audioSource.clip = shootSound;
            audioSource.Play();
            poolingShooting.Shoot();
            StartCoroutine(shakeCamera.Shake(0.06f, 0.05f));
            StartCoroutine(GunShake(0.06f, 0.003f));
            tankBody.AddTorque(-transform.right * pushPower * 10f, ForceMode.Impulse);
        }

    }


    public IEnumerator GunShake(float duration, float magnitude)
    {
        Vector3 orginalPos = gunBody.localPosition;
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            gunBody.localPosition = new Vector3(orginalPos.x, orginalPos.y, -magnitude);
            elapsed += Time.deltaTime;
            yield return null;
        }
        gunBody.localPosition = orginalPos;
    }
}
