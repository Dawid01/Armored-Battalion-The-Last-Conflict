using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Compass : MonoBehaviour
{

    public RawImage compassImage;
    public Transform target;
    public TMP_Text degressText;


    void Start () {
		
	}
	
	void Update () {

        compassImage.uvRect = new Rect(target.eulerAngles.y / 360, 0, 1, 1);
        int angle = Mathf.RoundToInt( 10f * Mathf.RoundToInt(target.eulerAngles.y / 10f));
        degressText.text = "" + angle;

        switch(angle){
            case 0:
                degressText.text = "N";
                break;
            case 360:
                degressText.text = "N";
                break;
            case 90:
                degressText.text = "E";
                break;
            case 180:
                degressText.text = "S";
                break;
            case 270:
                degressText.text = "W";
                break;
            default:
                degressText.text = "" + angle;
                break;                                        
        }

    }

}
