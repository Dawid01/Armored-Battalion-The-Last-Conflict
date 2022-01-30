using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireGun : MonoBehaviour
{
    private ParticleSystem particle;
    public bool openFire;
    public float demage = 5f;
    public float maxTime;
    public float time;

    public Image reloadImage;


    private void Awake() {
        particle = GetComponent<ParticleSystem>();
        time = maxTime;
        particle.Stop();
    }

    void Update()
    {
        reloadImage.fillAmount = time/maxTime;
        if(openFire){
            if(time > 0){
                time = Mathf.MoveTowards(time, 0f, Time.deltaTime);
                particle.Play();
            }else{
                particle.Stop();
                openFire = false;
            }
        }else{
            time = Mathf.MoveTowards(time, maxTime, Time.deltaTime);
            particle.Stop();
        }
    }


    public void SetFire(bool fire){
        openFire = fire;
    }

    public void ChangeFire(){
        openFire = !openFire;
    }

    private void OnParticleCollision(GameObject other) {
          if(other.transform.tag != "Player" && other.transform.tag != "Friend"){
            if(other.transform.TryGetComponent(out HpSystem hpSystem)){
                hpSystem.giveDemage(demage);
            }
        }
    }
}
