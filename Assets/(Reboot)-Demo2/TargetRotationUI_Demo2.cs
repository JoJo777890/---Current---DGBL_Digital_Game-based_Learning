using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TargetRotationUI_Demo2 : MonoBehaviour
{
    public GameObject imageTargetA;
    public GameObject imageTargetM;

    public TextMeshProUGUI infoTextA;
    public TextMeshProUGUI infoTextM;

    void Update()
    {

        if (imageTargetA.activeInHierarchy)
        {
            Vector3 rotA = imageTargetA.transform.localEulerAngles;

            infoTextA.text = $"[Target A]\nRot: X: {360 - rotA.x:F1}\nRot: Y: {rotA.y:F1}\nRot: Z: {360 - rotA.z:F1}";
        }

        if (imageTargetM.activeInHierarchy)
        {
            Vector3 rotM = imageTargetM.transform.localEulerAngles;

            infoTextM.text = $"[Target M]\nRot: X: {360 - rotM.x:F1}\nRot: Y: {rotM.y:F1}\nRot: Z: {360 - rotM.z:F1}";
        }


    }
}
