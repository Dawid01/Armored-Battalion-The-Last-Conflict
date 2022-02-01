using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PoolingShooting : MonoBehaviourPun
{
    public GameObject bulletPrefab;
    public bool isMultiplayer = false;
    public int bulletCount = 5;
    public List<Bullet> bullets;
    public bool isRocket = false;
    [HideInInspector] public List<RocketBullet> rocketBullets;
    [HideInInspector] public RocketBullet currentBullet;
    public float demage = 20f;

    public float bulletForce = 25f;

    //public PhotonView photonView;

    private void Awake()
    {
        if (!isMultiplayer)
        {
            for (int i = 0; i < bulletCount; i++)
            {
                GameObject b = Instantiate(bulletPrefab, transform.position, transform.rotation);
                b.transform.parent = transform;
                if (!isRocket)
                {
                    Bullet bullet = b.GetComponent<Bullet>();
                    bullet.demage = demage;
                    bullet.parent = transform;
                    bullet.resetBullet();
                    bullets.Add(bullet);
                }
                else
                {
                    RocketBullet rocketBullet = b.GetComponent<RocketBullet>();
                    rocketBullet.parent = transform;
                    rocketBullet.resetBullet();
                    rocketBullets.Add(rocketBullet);
                }
                // b.GetComponent<Rigidbody>().AddForce(b.transform.forward * 100f, ForceMode.Impulse);
            }
        }
        else {
            SpawnNetworkBullets();
        }     
    }

    private void Start() {
        if(isMultiplayer){
           // photonView = GetComponent<PhotonView>();
           /* if (photonView.IsMine)
            {
                photonView.RPC("SpawnNetworkBullets", RpcTarget.AllBuffered);
            }*/
        }
    }


    [PunRPC]
    void SpawnNetworkBullets()
    {
        if(isMultiplayer){
            for (int i = 0; i < bulletCount; i++)
            {   
                GameObject b = Instantiate(bulletPrefab, transform.position, transform.rotation);
                b.transform.parent = transform;
                if(!isRocket){
                    Bullet bullet = b.GetComponent<Bullet>();
                    bullet.demage = demage;
                    bullet.parent = transform;
                    bullet.resetBullet();
                    bullets.Add(bullet);
                }else{
                    RocketBullet rocketBullet = b.GetComponent<RocketBullet>();
                    rocketBullet.parent = transform;
                    rocketBullet.resetBullet();
                    rocketBullets.Add(rocketBullet);
                }
            // b.GetComponent<Rigidbody>().AddForce(b.transform.forward * 100f, ForceMode.Impulse);
            }
        }
    }
    

    public void Shoot() {

        if(!isRocket){
            Bullet bullet = bullets[0];
            bullet.resetBullet();
            bullet.activeBullet(bulletForce);
            bullets.RemoveAt(0);
            bullets.Add(bullet);
        }else{
            RocketBullet rocketBullet = rocketBullets[0];
            currentBullet = rocketBullet;
            rocketBullet.resetBullet();
            rocketBullet.activeBullet();
            rocketBullets.RemoveAt(0);
            rocketBullets.Add(rocketBullet);
        }
    }
}
