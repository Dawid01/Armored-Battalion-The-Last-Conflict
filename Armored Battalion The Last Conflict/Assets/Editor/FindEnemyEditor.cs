using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[UnityEditor.CustomEditor(typeof(FindEnemyLocation))]
public class FindEnemyEditor : Editor
{

    void OnSceneGUI()
    {
        FindEnemyLocation fow = (FindEnemyLocation)target;
        Handles.color = Color.white;
       // Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360, fow.viewRadius);
        //Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.right, 360, fow.viewRadius);

        //Handles.DrawWireArc(fow.transform.position, fow.transform.up, fow.transform.right, fow.viewAngle, fow.viewRadius);

      //  Vector3 viewAngleA = fow.DirFromAngle(-fow.viewAngle / 2, false);
        //Vector3 viewAngleB = fow.DirFromAngle(fow.viewAngle / 2, false);

       // Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleA * fow.viewRadius + fow.transform.forward);
       // Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleB * fow.viewRadius + fow.transform.forward);

        Handles.color = Color.red;
        foreach (Transform visibleTarget in fow.visibleTargets)
        {
            Handles.DrawLine(fow.transform.position, visibleTarget.position + Vector3.up);

        }
        Handles.color = Color.yellow;

        foreach (Transform targets in fow.targets)
        {
            Handles.DrawLine(fow.transform.position, targets.position);

        }
    }
}
