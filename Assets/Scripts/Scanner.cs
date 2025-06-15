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

    private int clickTimes = 0;
    private bool isScheduledForDestruction = false;
    private bool isMoving = false;
    private List<string> touchedLetters = new List<string>();

    public string combinedWord;
    public string resultWord;

    public event Action onFetchedResultWord;
    
    private void Start()
    {
        // scannerManager = GameObject.FindGameObjectWithTag("ScannerManager");
        // scannerManager.GetComponent<ScannerManager>().onScreenClick += MoveScanner;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Fetch Single ResultWord.
            if (clickTimes == 0)
            {
                Invoke("FetchCombinedWord", 2.5f);
                clickTimes++;
            }
        }

        if (isMoving)
        {
            // Move forwards right.
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }

        CombineLetters();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("g Letter Detected! " + other.name);
        
        // If touched a letter, add that letter to "touchedLetters".
        if (other.tag == "Letters")
        {
            Debug.Log("g Letter: " + other.GetComponent<TMP_Text>().text);
            
            touchedLetters.Add(other.GetComponent<TMP_Text>().text);
        }
    }
    
    public void  MoveScanner()
    {
        Debug.Log("Scanner Moving...");
        
        isMoving = true;
    }
    
    void CombineLetters()
    {
        combinedWord = string.Join("", touchedLetters);
        //Debug.Log("g letter combined: " + combinedWord);
    }

    void FetchCombinedWord()
    {
        resultWord = combinedWord;
        Debug.Log("Single Result Word: " + resultWord);

        onFetchedResultWord();
    }

    public void StartDestroyObject()
    {
        // Destroy this GameObject in 3 seconds.
        Invoke("DestroyObject", 3f);
    }
    
    void DestroyObject()
    {
        // 销毁物体
        Destroy(gameObject);
    }
}
