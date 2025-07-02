using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GestureTracker_Demo2 : MonoBehaviour
{
    public GameObject gestureTarget; // Assign the moving target
    public TextMeshProUGUI uiText;   // Assign a UI Text element

    public float moveThreshold = 0.03f;    // Originally "0.03f"
    public int smoothFrameCount = 5;    // Originally "5"
    public int requiredFrames = 3;      // Originally "3"
    public float cooldownTime = 0.25f;  // Originally "1f"

    private Queue<float> recentZPositions = new Queue<float>();
    private int upFrames = 0;
    private float cooldownTimer = 0f;

    void Update()
    {
        if (!gestureTarget.activeInHierarchy)
        {
            ResetTracking();
            uiText.text = "Target not visible.";
            return;
        }

        if (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
            return;
        }

        float currentZ = gestureTarget.transform.position.z;

        recentZPositions.Enqueue(currentZ);
        if (recentZPositions.Count > smoothFrameCount)
            recentZPositions.Dequeue();

        float avgZ = 0f;
        foreach (var y in recentZPositions)
            avgZ += y;

        avgZ /= recentZPositions.Count;

        float deltaZ = currentZ - avgZ;

        uiText.text = $"[Up Trigger]\nCurrent Y: {currentZ:F3}\nΔY: {deltaZ:F3}\nUp Trigger: "; // "Current Y" is actually "Z Axis"

        if (recentZPositions.Count == smoothFrameCount)
        {
            if (deltaZ > moveThreshold)
            {
                upFrames++;
            }
            else
            {
                upFrames = 0;
            }

            if (upFrames >= requiredFrames)
            {
                uiText.text += "⬆️ UP Triggered!";
                PerformUpAction();
                cooldownTimer = cooldownTime;
                upFrames = 0;
            }
        }
    }

    void PerformUpAction()
    {
        Debug.Log("Up action triggered!");
        // Add your game logic here
    }

    void ResetTracking()
    {
        recentZPositions.Clear();
        upFrames = 0;
    }
}
