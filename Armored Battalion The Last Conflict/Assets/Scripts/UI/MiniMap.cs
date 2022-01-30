using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{

    public GameObject miniMap;
    public GameObject normalMap;


    public void ChangeMap(){
        miniMap.SetActive(!miniMap.active);
        normalMap.SetActive(!normalMap.active);
    }
}
