using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicCamera : MonoBehaviour
{
    Transform _camera;
    public float maxDistance = 5f;
    void Start()
    {
        _camera = transform.GetChild(0);
    }

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.forward * maxDistance, out hit))
        {
            if (hit.distance <= maxDistance)
            {
                _camera.position = hit.point + transform.forward * 0.25f;
            }
            else {
                _camera.position = transform.position - transform.forward * maxDistance;
            }
        }
        else {

            _camera.position = transform.position - transform.forward * maxDistance;
        }
    }
}
