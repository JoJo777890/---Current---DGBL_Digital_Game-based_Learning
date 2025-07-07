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

    private Queue<float> recentYPositions = new Queue<float>();
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

        float currentY = gestureTarget.transform.position.z;

        recentYPositions.Enqueue(currentY);
        if (recentYPositions.Count > smoothFrameCount)
            recentYPositions.Dequeue();

        float avgY = 0f;
        foreach (var y in recentYPositions)
            avgY += y;

        avgY /= recentYPositions.Count;

        float deltaY = currentY - avgY;

        uiText.text = $"[Up Trigger]\nCurrent Y: {currentY:F3}\nΔY: {deltaY:F3}\nUp Trigger: ";
        if (recentYPositions.Count == smoothFrameCount)
        {
            if (deltaY > moveThreshold)
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
        recentYPositions.Clear();
        upFrames = 0;
    }
}
