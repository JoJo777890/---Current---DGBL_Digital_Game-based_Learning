using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTitle : MonoBehaviour
{
    public GameObject levelTitle;

    private void Start()
    {
        levelTitle.SetActive(true);
        Invoke("DisableLevelTitle", 3f);
    }

    void DisableLevelTitle()
    {
        levelTitle.SetActive(false);
    }
}
