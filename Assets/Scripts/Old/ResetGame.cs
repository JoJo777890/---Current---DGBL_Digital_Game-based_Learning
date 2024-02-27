using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResetGame : MonoBehaviour
{
    private int click = 0;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            click++;
            if (click == 2)
            {
                Reset();
            }
        }
    }
    void Reset()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
}
