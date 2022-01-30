using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Truck : MonoBehaviour
{
    Renderer rend;
    public float rightTruckSpeed;
    public float leftTruckSpeed;

    private float rightValue = 0;
    private float leftValue = 0;

    public float scale = 1f;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {   
        rightTruckSpeed = Mathf.Clamp(rightTruckSpeed, -0.001f, 0.001f);
        leftTruckSpeed = Mathf.Clamp(leftTruckSpeed, -0.001f, 0.001f);
        
        rightValue += rightTruckSpeed * Time.deltaTime * 500f * scale;
        leftValue += leftTruckSpeed * Time.deltaTime * 500f * scale;

        rend.materials[1].SetTextureOffset("_BaseMap", new Vector2(0f, rightValue));
        rend.materials[0].SetTextureOffset("_BaseMap", new Vector2(0f, leftValue));
    }
}
