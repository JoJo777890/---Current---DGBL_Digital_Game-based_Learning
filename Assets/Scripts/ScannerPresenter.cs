using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScannerPresenter : MonoBehaviour
{
    //[SerializeField] ScannerManager scannerManager;
    //[SerializeField] Button clickScreenButton;
    [SerializeField] private TextMeshProUGUI combinedWordText;

    public GameObject[] scanners;
    
    // private void Start()
    // {
    //     //clickScreenButton.onClick.AddListener(MoveScanner);
    // }

    // private void MoveScanner()
    // {
    //     
    // }
    
    private void Start()
    {
        scanners = GameObject.FindGameObjectsWithTag("Scanners");
    }

    private void Update()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        combinedWordText.text = "";
        foreach (GameObject scanner in scanners)
        {
            //Debug.Log("Scanner name: " + scanner.name);
            combinedWordText.text += scanner.GetComponent<Scanner>().combinedWord + "\n";
        }
    }
}
