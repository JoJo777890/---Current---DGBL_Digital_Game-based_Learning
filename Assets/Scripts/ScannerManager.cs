using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScannerManager : MonoBehaviour
{
    public GameObject[] scanners;
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

                isFirstClick = false;
            }
        }
    }
}
