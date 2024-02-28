using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GoodJobImageShower : MonoBehaviour
{
    public RawImage goodJobRawImage;

    private void Start()
    {
        goodJobRawImage.enabled = false;
    }

    public void StartShowGoodJobRawImage()
    {
        StartCoroutine("ShowGoodJobRawImage");
    }

    private IEnumerator ShowGoodJobRawImage()
    {
        goodJobRawImage.enabled = true;
        Debug.Log("AAAAA");
        yield return new WaitForSeconds(3);
        Debug.Log("AAABB");
        goodJobRawImage.enabled = false;
    }
}
