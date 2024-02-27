using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScannerManager : MonoBehaviour
{
    //public event Action onScreenClick;
    public GameObject[] scanners;

    private void Start()
    {
        scanners = GameObject.FindGameObjectsWithTag("Scanners");
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            foreach (GameObject scanner in scanners)
            {
                Debug.Log("Scanner name: " + scanner.name);
                scanner.GetComponent<Scanner>().MoveScanner();
            }
        }
    }
}
