using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalCollection : MonoBehaviour
{
    // List of every animals.
    public List<Transform> animalTransforms = new List<Transform>();

    private void Awake()
    {
        // Find all animals in AnimalCollection.
        foreach (Transform child in transform)
        {
            animalTransforms.Add(child);
            Debug.Log("child: " + child.name);
        }
        
        Debug.Log("children count: " + animalTransforms.Count);
    }
}
