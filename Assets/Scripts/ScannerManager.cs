using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScannerManager : MonoBehaviour
{
    public GameObject[] scanners;
    private List<string> resultWords = new List<string>();
    private int clickTimes = 0;
    
    //public event Action onScreenClick;

    private void Start()
    {
        scanners = GameObject.FindGameObjectsWithTag("Scanners");
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (clickTimes == 1)
            {
                // Debug.Log("Click: !");
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            if (clickTimes == 0)
            {
                foreach (GameObject scanner in scanners)
                {
                    Debug.Log("Scanner name: " + scanner.name);
                    scanner.GetComponent<Scanner>().MoveScanner();
                    scanner.GetComponent<Scanner>().StartDestroyObject();
                }
                
                // Fetch a List of ResultWord.
                Invoke("FetchCombinedWords", 2.5f);
                clickTimes++;
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
