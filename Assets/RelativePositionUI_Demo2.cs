using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RelativePositionUI_Demo2 : MonoBehaviour
{
    public GameObject targetA;
    public GameObject targetB;

    public TextMeshProUGUI infoText; // Assign this in the Inspector

    void Update()
    {
        if (targetA.activeInHierarchy && targetB.activeInHierarchy)
        {
            Vector3 posA = targetA.transform.position;
            Vector3 posB = targetB.transform.position;

            Vector3 delta = posB - posA;

            float distance = delta.magnitude;

            infoText.text = $"[Raletive Position]\n£GX: {-1 * delta.x:F3}\n£GY: {-1 * delta.y:F3}\n£GZ: {delta.z:F3}\nTotal Distance: {distance:F3} units";
        }
        else
        {
            infoText.text = "Tracking lost...";
        }
    }
}
