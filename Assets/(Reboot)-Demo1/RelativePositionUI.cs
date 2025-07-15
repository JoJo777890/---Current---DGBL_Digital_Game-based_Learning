using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RelativePositionUI : MonoBehaviour
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

            infoText.text = $"[Raletive Position]\n£GX: {delta.x:F3}\n£GY: {delta.z:F3}\n£GZ: {delta.y:F3}\nTotal Distance: {distance:F3} units";
        }
        else
        {
            infoText.text = "Tracking lost...";
        }
    }
}
