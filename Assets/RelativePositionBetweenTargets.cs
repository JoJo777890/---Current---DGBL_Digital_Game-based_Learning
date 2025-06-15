using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelativePositionBetweenTargets : MonoBehaviour
{
    public GameObject targetA; // assign in inspector
    public GameObject targetB; // assign in inspector

    void Update()
    {
        if (targetA.activeInHierarchy && targetB.activeInHierarchy)
        {
            Vector3 positionA = targetA.transform.position;
            Vector3 positionB = targetB.transform.position;

            Vector3 relativePosition = positionB - positionA;

            //Vector3 localRelativePosition = targetA.transform.InverseTransformPoint(targetB.transform.position);

            float deltaX = relativePosition.x;
            float deltaY = relativePosition.y;
            float deltaZ = relativePosition.z;

            float totalDistance = relativePosition.magnitude;

            Debug.Log($"£GX: {deltaX:F3}, £GY: {deltaY:F3}, £GZ: {deltaZ:F3}, Total Distance: {totalDistance:F3} units");
        }
    }
}
