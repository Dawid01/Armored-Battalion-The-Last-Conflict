using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindEnemyLocation : MonoBehaviour
{
    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;

    public LayerMask targetMask;
    public LayerMask obstacleMask;
    public Transform gun;

    public List<Transform> visibleTargets = new List<Transform>();
    public List<Transform> targets = new List<Transform>();
    public float gunAngle = 0f;

    void Start()
    {
        StartCoroutine("FindTargetsWithDelay", 0.5f);
    }

    void Update()
    {
        if (visibleTargets.Count != 0) { 

            Transform target = visibleTargets[0];
            Vector3 rotation = Quaternion.LookRotation((target.position + Vector3.up) - gun.position).eulerAngles;
            rotation.y = gun.eulerAngles.y;
            rotation.z = gun.eulerAngles.z;
            gun.rotation = Quaternion.Euler(rotation);
        }
        else {

            gun.localEulerAngles = new Vector3(90, 0f, 0f);
        }
       // FindVisibleTargets();
    }

    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    void FindVisibleTargets()
    {
        visibleTargets.Clear();
        targets.Clear();
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            targets.Add(target);
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            float angleY = Vector3.Angle(transform.forward, dirToTarget);
            float angleX = degreesBetweenVectors(transform.position + transform.forward, target.position);
           // Quaternion angleX = Quaternion.FromToRotation(transform.forward, dirToTarget);
            float angle = Vector3.SignedAngle(dirToTarget, transform.forward, Vector3.right);
 
            if (i == 0)
            {
                Debug.Log("Angle: " + angle + " AngleY: " + angleY);
            }

            if (Mathf.Abs(angle) < viewAngle && angleY <= 20f)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                {
                    visibleTargets.Add(target);
                   
                }
            }
        }
    }


    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    float degreesBetweenVectors(Vector3 vA, Vector3 vB)
    {
        vA.Normalize();
        vB.Normalize();
        float ADotB = Vector3.Dot(vA, vB);
        float radians = Mathf.Acos(ADotB);
        return radians * Mathf.Rad2Deg;
    }
}
