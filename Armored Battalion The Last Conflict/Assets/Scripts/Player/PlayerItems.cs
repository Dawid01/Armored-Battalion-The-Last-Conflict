using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PlayerItems : MonoBehaviour
{

    public GameObject shield;
    private Image shieldImage;
    private float shieldTime;
    private float sTime;

    private HpSystem hpSystem;
    public GameObject shieldButton;
    public GameObject granateGun;
    public GameObject granateButton;
    //public PoolingShooting poolingShooting;
    public GameObject speedButton;
    private Image speedImage;
    private float speedTime;
    private float spTime;
    private TankMovement tankMovement;
    private float tankSpeed;

    public PoolingShooting rickoshetShooting;
    public int rickoshetBullets;
    public GameObject rickoshetButton;
    public TextMeshProUGUI rickoshetAmoInfo;
    private Image rickoshetReload; 


    void Start()
    {
        tankMovement = GetComponent<TankMovement>();
        hpSystem = GetComponent<HpSystem>();
        tankSpeed = tankMovement.speed;
        shieldImage = shieldButton.GetComponent<Image>();
        speedImage = speedButton.GetComponent<Image>();
        rickoshetReload = rickoshetButton.GetComponent<Image>();
    }

    void Update()
    {
        if(shield.active){
            sTime -= Time.deltaTime;
            shieldImage.fillAmount = sTime/shieldTime;
        }

        if(speedButton.active && spTime > 0f){
            spTime -= Time.deltaTime;
            speedImage.fillAmount = spTime/speedTime;
        }
    }

    private void OnTriggerEnter(Collider other) {
        switch(other.tag){
            case "Shield":
                if(!shieldButton.active){
                    AddShield();
                    Destroy(other.gameObject);
                }
                break;
            case "Granate":
                if(!granateButton.active){
                    AddGranate();
                    Destroy(other.gameObject);
                }
                break;  
            case "Speed":
                if(!speedButton.active){
                    AddSpeed();
                    Destroy(other.gameObject);
                }
                break; 
             case "Rickoshet":
                    AddRickoshetAmmo();
                    Destroy(other.gameObject);
                break;   
        }
    }


    public void AddShield(){
        if(!shield.active){
            shieldButton.SetActive(true);
        }
    }

    public void ActiveShield(float time){
        if(!shield.active){
            shieldTime = time;
            sTime = time;
            shield.SetActive(true);
            hpSystem.isShield = true;
            Invoke("DesactiveShield", time);
        }
    }

    private void DesactiveShield(){
        shieldButton.SetActive(false);
        hpSystem.isShield = false;
        shield.SetActive(false);
        shieldImage.fillAmount = 1f;
    }

    public void AddGranate(){
        granateButton.SetActive(true);
        granateGun.SetActive(true);
    }

    public void ActiveGranate(){
        granateButton.SetActive(false);
        //granateGun.SetActive(false);
        //poolingShooting.Shoot();
        Invoke("DesactiveGranate", 1f);
    }

    private void DesactiveGranate(){
         granateGun.SetActive(false);
    }

    public void AddSpeed(){
        speedButton.SetActive(true);
    }

    public void ActiveSpeed(float time){
        if(spTime <= 0f){
            speedTime = time;
            spTime = time;
            tankMovement.speed = 8f;
            Invoke("DesactiveSpeed", time);
        }
    }

    private void DesactiveSpeed(){
        tankMovement.speed = tankSpeed;
        speedButton.SetActive(false);
        speedImage.fillAmount = 1f;
    }


    public void AddRickoshetAmmo(){
        rickoshetBullets += 5;
        rickoshetButton.SetActive(true);
        rickoshetAmoInfo.text = "" + rickoshetBullets;
    }


    public void ActiveRickoshet(){
        rickoshetButton.SetActive(!rickoshetButton.active);
    }

    public void ShootRickoshet(){
        if(rickoshetBullets > 0){
            rickoshetShooting.Shoot();
            rickoshetBullets--;
            rickoshetAmoInfo.text = "" + rickoshetBullets;
            if(rickoshetBullets == 0){
                rickoshetButton.active = false;
            }
        }
    }
    
}
