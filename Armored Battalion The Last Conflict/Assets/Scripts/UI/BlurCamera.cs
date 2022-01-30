using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlurCamera : MonoBehaviour
{

    public Camera blurCamera;
    public Material blurMaterial;
    private bool setTexture = false;

    void Start()
    {

    }

    void Update()
    {

    }

    public void SetBlur(int i){
        if(!blurCamera){
            try{
                blurCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
            }catch{}     
        }
        if(blurCamera.targetTexture != null){
                blurCamera.targetTexture.Release();
        }        
        if(i == 1){    
            blurCamera.targetTexture = new RenderTexture(Screen.width, Screen.height, 24, RenderTextureFormat.ARGB32, 1);
            blurMaterial.SetTexture("_RenTex", blurCamera.targetTexture);
        }else{
            blurMaterial.SetTexture("_RenTex", null);
            blurCamera.targetTexture = null;
        }
    
    }
}
