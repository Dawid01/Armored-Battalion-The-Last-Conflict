using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankResetSystem : MonoBehaviour
{
    [System.Obsolete]
    public void ResetObject()
    {
        UnityEditor.PrefabUtility.ResetToPrefabState(this.gameObject);
    }
}


