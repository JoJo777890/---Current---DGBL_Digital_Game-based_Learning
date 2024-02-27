using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class AnimalGrabber : MonoBehaviour
{
    private Scanner scanner;
    private string resultWord;
    private void Start()
    {
        scanner = GetComponent<Scanner>();
        scanner.onFetchedResultWord += fetchResultWord;
    }

    void fetchResultWord()
    {
        this.resultWord = scanner.resultWord;
        Debug.Log("New Single Result Word: " + resultWord);
    }
}
