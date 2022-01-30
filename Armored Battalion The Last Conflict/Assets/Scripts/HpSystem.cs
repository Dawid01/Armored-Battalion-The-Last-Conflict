using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using Photon.Pun;

public class HpSystem : MonoBehaviourPunCallbacks, IPunObservable
{

    public List<Image> hpImages;

    public float maxHp;
    public float hp;
    public bool isEnemy = true;
    private Rigidbody turretRb;
    public GameObject turret;
    public ParticleSystem explosion;
    public TankAIMovement tankAI;
    public NavMeshAgent agent;
    private bool isDeath = false;
    public GameObject playerCanvas;
    public GameObject itemCanvas;
    private ClassicGame classicGame;
    private TankMovement tankMovement;
    private DynamicCamera dynamicCamera;
    public bool isShield;

    public Material destroyMaterial;
    public MeshRenderer[] meshes;
    private PhotonView photonView;

    void Start()
    {
        hp = maxHp;
        foreach (Image img in hpImages)
        {
            img.fillAmount = 1;
        }
        explosion.Stop();
        classicGame = FindObjectOfType<ClassicGame>();
        if(!isEnemy){
            tankMovement = FindObjectOfType<TankMovement>();
            dynamicCamera = FindObjectOfType<DynamicCamera>();
        }
        photonView = GetComponent<PhotonView>();


    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(hp);
            foreach (Image img in hpImages)
            {
                stream.SendNext(img.fillAmount);
            }
            
        }
        else {
            hp = (int)stream.ReceiveNext();
            foreach (Image img in hpImages)
            {
                img.fillAmount = (int)stream.ReceiveNext();
            }
            
        }
    }


    void Update()
    {
        
    }

    public void giveMultiplayerDemage(float demage) {
            photonView.RPC("giveDemage", RpcTarget.AllBufferedViaServer, demage);
    }

    [PunRPC]
    public void giveDemage(float demage){
        if(!isShield){
            hp = hp - demage;
            foreach (Image img in hpImages) {
                img.fillAmount = hp / maxHp;
            }
            if(hp <= 0 && !isDeath){
                isDeath = true;
                death();
            }
        }
    }

    public void death(){

        if(itemCanvas){
            itemCanvas.SetActive(false);
        }
        explosion.Play();
        turretRb = turret.AddComponent<Rigidbody>();
        turretRb.useGravity = true;
        turretRb.isKinematic = false;
        turretRb.angularDrag = 0.05f;
        turretRb.mass = 10f;
        turretRb.AddForce(Vector3.up * 40f, ForceMode.Impulse);
        //turretRb.AddTorque(new Vector3(Random.Range(-80f, 80f),Random.Range(-80f, 80f),Random.Range(-80f, 80f)), ForceMode.Impulse);
        turretRb.AddTorque(new Vector3(-80f, 60f, 70f), ForceMode.Impulse);



        if(isEnemy){
            tankAI.StopAllCoroutines();
            tankAI.enabled = false;
            agent.enabled = false;
            Destroy(transform.parent.gameObject, 1.5f);
            foreach (Image img in hpImages)
            {
                Destroy(img.transform.parent.gameObject);

            }

            switch (classicGame.gameType){
                case 0:
                    break;
                case 1:
                    DestroyAllEnemies destroyAllEnemies = (DestroyAllEnemies)classicGame;
                    destroyAllEnemies.KillEnemy();
                    break;    
            }
            
        }else{
             tankMovement.enabled = false;
             dynamicCamera.enabled = false;
             Destroy(playerCanvas);
             classicGame.Defeat();
        }

        if(destroyMaterial){
            foreach(MeshRenderer mesh in meshes){
               // mesh.materials[0] = destroyMaterial;
               mesh.material = destroyMaterial;
            }
        }

    }

    private void OnTriggerEnter(Collider other) {
        if(!isEnemy && other.tag == "Reperain" && hp < maxHp){
            hp += 30;
            if(hp > maxHp){
                hp = maxHp;
            }
            giveDemage(0);
            Destroy(other.gameObject);
        }
    }
}
