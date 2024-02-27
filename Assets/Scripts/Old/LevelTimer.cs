using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelTimer : MonoBehaviour
{
    public AudioManager audioManager;

    private float timer;
    public float timerLength = 10f;

    public TextMeshProUGUI levelTimerText;

    public static event Action onTimeIsUp;

    void Awake()
    {
        onTimeIsUp = null;
    }

    void Start()
    {
        timer = timerLength;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        levelTimerText.text = string.Format("Timer: {0:0}", timer);

        if (timer < 0)
        {
            audioManager.PlaySFX_Retry(audioManager.Retry);

            timer = timerLength;
            Debug.Log("Stop!");
            onTimeIsUp?.Invoke();
        }
    }
}
