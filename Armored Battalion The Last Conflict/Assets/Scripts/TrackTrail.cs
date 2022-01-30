using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackTrail : MonoBehaviour
{

    public LineRenderer line;

    void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    void Update()
    {
        Vector3[] positions = new Vector3[line.positionCount + 1];
        line.GetPositions(positions);
        positions[line.positionCount + 1] = transform.position;
        line.SetPositions(positions);
    }
}
