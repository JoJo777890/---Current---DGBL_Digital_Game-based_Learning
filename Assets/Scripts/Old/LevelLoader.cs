using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;

    public float transitionTime = 1f;
    public float reloadTransitionTime = 10f;
    private int click = 0;
    private int sceneIndex;

    public GameObject RetryUI;

    void Start()
    {
        RetryUI.SetActive(false);
        LevelTimer.onTimeIsUp += LoadCurrentLevel;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            click++;
            if (click == 2)
            {
                // 原本程式
                LoadNextLevel();

                // 修改程式 (為了方便重新整理 "Level 1")
                //SceneManager.LoadScene(0);
            }
        }
    }


    public void LoadNextLevel()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if(sceneIndex >= 4)
        {
            sceneIndex = 0;
            StartCoroutine(LoadLevel(sceneIndex));
        }
        else
        {
            StartCoroutine(LoadLevel(sceneIndex));
        }
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }

    public void LoadCurrentLevel()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(sceneIndex);
        //StartCoroutine(ReloadLevel(sceneIndex));
    }

    IEnumerator ReloadLevel(int levelIndex)
    {
        // I turned off "Retry UI" for now.
        //RetryUI.SetActive(true);

        Debug.Log("RE: " + reloadTransitionTime);

        yield return new WaitForSeconds(reloadTransitionTime);

        Debug.Log("RE: end");

        SceneManager.LoadScene(levelIndex);
    }
}
