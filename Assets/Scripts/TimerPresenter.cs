using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerPresenter : MonoBehaviour
{
    [SerializeField] private Timer timer;
    [SerializeField] private TextMeshProUGUI timerText;
    
    private void Start()
    {
        timer.onEverySecond += UpdateUI;
        //timer.onLevelTimeEnd += UpdateUI;
        UpdateUI();
    }

    private void UpdateUI()
    {
        timerText.text = $"Timer: {timer.GetCurrentTime()}";
    }
}
