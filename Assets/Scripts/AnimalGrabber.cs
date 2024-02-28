using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class AnimalGrabber : MonoBehaviour
{
    private SoundEffectPlayer soundEffectPlayer;
    private GameObject audioSource;
    private GameObject[] animals;
    private Scanner scanner;
    private string resultWord;
    private void Start()
    {
        audioSource = GameObject.FindGameObjectWithTag("Audio");
        soundEffectPlayer = audioSource.GetComponent<SoundEffectPlayer>();
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
            }
        }
    }
}
