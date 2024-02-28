using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScannerManager : MonoBehaviour
{
    public GameObject[] scanners;
    private List<string> resultWords = new List<string>();
    private bool isFirstClick = true;
    
    //public event Action onScreenClick;

    private void Start()
    {
        scanners = GameObject.FindGameObjectsWithTag("Scanners");
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isFirstClick == true)
            {
                foreach (GameObject scanner in scanners)
                {
                    Debug.Log("Scanner name: " + scanner.name);
                    scanner.GetComponent<Scanner>().MoveScanner();
                    scanner.GetComponent<Scanner>().StartDestroyObject();
                }
                
                // Fetch a List of ResultWord.
                Invoke("FetchCombinedWords", 2.5f);
                isFirstClick = false;
            }
        }
    }

    private void FetchCombinedWords()
    {
        foreach (GameObject scanner in scanners)
        {
            resultWords.Add(scanner.GetComponent<Scanner>().combinedWord);
            //Debug.Log("result word: " + scanner.GetComponent<Scanner>().combinedWord);
        }
        
        // Debug Purpose
        foreach (string word in resultWords)
        {
            Debug.Log("result word: " + word);
        }
    }
}
