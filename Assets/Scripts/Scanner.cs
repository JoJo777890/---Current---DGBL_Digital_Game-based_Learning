using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Scanner : MonoBehaviour
{
    //[SerializeField] ScannerManager scannerManager;
    [SerializeField] float speed = 1.0f;

    private bool isMoving = false;
    private List<string> touchedLetters = new List<string>();
    
    private void Start()
    {
        //scannerManager.onScreenClick += MoveScanner;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isMoving = true;
        }
        
        if (isMoving)
        {
            // Move forwards right.
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("g Letter Detected! " + other.name);
        Debug.Log("g Letter: " + other.GetComponent<TMP_Text>().text);

        if (other.tag == "Letters")
        {
            Debug.Log(other.GetComponent<TMP_Text>().text);
        }
    }

    public void  MoveScanner()
    {
        Debug.Log("Scanner Moving...");

        //isMoving = true;
    }
}
