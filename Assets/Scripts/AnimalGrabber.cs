using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class AnimalGrabber : MonoBehaviour
{
    private GoodJobImageShower goodJobImageShower;
    private SoundEffectPlayer soundEffectPlayer;
    private ScannerManager scannerManager;
    private GameObject[] animals;
    private Scanner scanner;
    private string resultWord;
    private void Start()
    {
        goodJobImageShower = GameObject.FindGameObjectWithTag("Canvas").GetComponent<GoodJobImageShower>();
        soundEffectPlayer = GameObject.FindGameObjectWithTag("Audio").GetComponent<SoundEffectPlayer>();
        scannerManager = GameObject.FindGameObjectWithTag("ScannerManager").GetComponent<ScannerManager>();
        scanner = GetComponent<Scanner>();
        scanner.onFetchedResultWord += FetchResultWord;
    }

    void FetchResultWord()
    {
        this.resultWord = scanner.resultWord;
        Debug.Log("New Single Result Word: " + resultWord);
        
        Invoke("MoveAnimalToBookSymbol", 0.5f);
    }

    void MoveAnimalToBookSymbol()
    {
        animals = GameObject.FindGameObjectsWithTag("Animals");

        foreach (GameObject animal in animals)
        {
            if (string.Equals(animal.name, resultWord))
            {
                animal.transform.SetParent(transform.parent, true);
                animal.transform.position = transform.parent.position;
                
                soundEffectPlayer.PlayYaySoundEffect();
                
                ShowGreatJobImage();
                scannerManager.isPassed = true;
            }
        }
    }

    void ShowGreatJobImage()
    {
        goodJobImageShower.StartShowGoodJobRawImage();
    }
}
