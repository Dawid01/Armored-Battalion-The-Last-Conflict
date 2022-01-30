using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTank : MonoBehaviour
{

    public GameObject[] tanks;


    private void Awake() {
        tanks[0].SetActive(false);
    }

    void Start()
    {
        int index = PlayerPrefs.GetInt("menu_tank", 0);
        tanks[index].SetActive(true);
    }

    void Update()
    {
        
    }
}
