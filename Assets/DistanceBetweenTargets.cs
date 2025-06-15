using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceBetweenTargets : MonoBehaviour
{
    public GameObject targetA; // assign in inspector
    public GameObject targetB; // assign in inspector

    void Update()
    {
        if (targetA.activeInHierarchy && targetB.activeInHierarchy)
        {
            float distance = Vector3.Distance(targetA.transform.position, targetB.transform.position);
            Debug.Log("Distance between targets: " + distance + " units");
        }

        //Debug.DrawLine(targetA.transform.position, targetB.transform.position, Color.red);
    }
}
