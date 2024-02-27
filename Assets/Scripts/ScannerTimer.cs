using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScannerTimer : MonoBehaviour
{
    private float currentTime = 3;

    public event Action onEverySecond;
    public event Action onLevelTimeEnd; 

    private void Awake()
    {
        StartCoroutine(TimerCountdown());
    }

    public float GetCurrentTime()
    {
        return currentTime;
    }

    private IEnumerator TimerCountdown()
    {
        while (true)
        {
            if (onEverySecond != null)
            {
                onEverySecond();
            }
            
            if (currentTime <= 0)
            {
                if (onLevelTimeEnd != null)
                {
                    onLevelTimeEnd(); // TODO
                }
                break; // TODO
            }
            
            yield return new WaitForSeconds(1);
            currentTime -= 1;
        }
    }
}
