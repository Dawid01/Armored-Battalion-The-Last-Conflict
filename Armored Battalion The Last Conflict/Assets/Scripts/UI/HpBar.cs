using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HpBar : MonoBehaviour
{

    Transform _camera;
    void Start()
    {
    
    }

    void LateUpdate()
    {
        if(_camera == null){
            try{
            _camera = GameObject.FindWithTag("MainCamera").transform;
            }catch{

            }
        }else{
            transform.LookAt(transform.position + _camera.forward);
        }
    }
}
