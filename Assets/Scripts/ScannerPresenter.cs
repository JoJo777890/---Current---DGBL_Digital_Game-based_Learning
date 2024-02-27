using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScannerPresenter : MonoBehaviour
{
    [SerializeField] ScannerManager scannerManager;
    [SerializeField] Button clickScreenButton;

    private void Start()
    {
        clickScreenButton.onClick.AddListener(MoveScanner);
    }

    private void MoveScanner()
    {
        
    }
}
