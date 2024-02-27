using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalPresenter : MonoBehaviour
{
    public GameObject catModel;
    public GameObject catModelShadow;
    public GameObject antModel;
    public GameObject antModelShadow;

    public AudioManager audioManager;

    public Scan scan;

    private string wordCat = "cat";
    private string wordAnt = "ant";

    private bool hasCorrectWord = false;

    void Start()
    {
        hasCorrectWord = false;

        Debug.Log("**TorF" + hasCorrectWord);

        Scan.onFullScan += CheckCatSameLetters;
        Scan.onFullScan += CheckAntSameLetters;
        
        Debug.Log("***TorF" + hasCorrectWord);

        catModelShadow.SetActive(false);
        catModel.SetActive(false);
        antModelShadow.SetActive(false);
        antModel.SetActive(false);
    }

    void CheckCatSameLetters()
    {
        Debug.Log("TorF" + hasCorrectWord);
        if (hasCorrectWord == false)
        {
            Debug.Log("TorF Entered");
            for (int i = 0; i < scan.displayText.Length; i++)
            {
                for (int j = 0; j < wordCat.Length; j++)
                {
                    // Make this an another function
                    if (scan.displayText[i] == wordCat[j])
                    {
                        catModelShadow.SetActive(true);
                        Debug.Log("Cat shadow model " + i + "show.");
                    }
                }
            }

            if (string.Equals(scan.displayText, wordCat) == true)
            {
                catModelShadow.SetActive(false);
                catModel.SetActive(true);

                audioManager.PlaySFX(audioManager.correctAnimal);

                hasCorrectWord = true;
            }
        }
        else
        {
            catModelShadow.SetActive(false);
        }

    }

    void CheckAntSameLetters()
    {
        if (hasCorrectWord == false)
        {
            for (int i = 0; i < scan.displayText.Length; i++)
            {
                for (int j = 0; j < wordAnt.Length; j++)
                {
                    // Make this an another function
                    if (scan.displayText[i] == wordAnt[j])
                    {
                        antModelShadow.SetActive(true);
                    }
                }
            }

            if (string.Equals(scan.displayText, wordAnt) == true)
            {
                antModelShadow.SetActive(false);
                antModel.SetActive(true);

                audioManager.PlaySFX(audioManager.correctAnimal);

                hasCorrectWord = true;
            }
        }
        else
        {
            antModelShadow.SetActive(false);
        }
    }
}
