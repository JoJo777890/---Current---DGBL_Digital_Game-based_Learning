using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TargetRotationUI_Demo2 : MonoBehaviour
{
    public GameObject imageTargetA;
    public GameObject imageTargetC;

    public TextMeshProUGUI infoTextA;
    public TextMeshProUGUI infoTextC;

    void Update()
    {
        if (imageTargetA.activeInHierarchy)
        {
            Vector3 rotA = imageTargetA.transform.eulerAngles;

            infoTextA.text = $"[Target A]\nRot: X: {360 - rotA.x:F1}\nRot: Y: {360 - rotA.z:F1}\nRot: Z: {rotA.y:F1}";
        }

        if (imageTargetC.activeInHierarchy)
        {
            Vector3 rotC = imageTargetC.transform.eulerAngles;

            infoTextC.text = $"[Target C]\nRot: X: {360 - rotC.x:F1}\nRot: Y: {360 - rotC.z:F1}\nRot: Z: {rotC.y:F1}";
        }
    }
}
