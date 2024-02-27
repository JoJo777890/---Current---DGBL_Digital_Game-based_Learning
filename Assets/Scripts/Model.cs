using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model : MonoBehaviour
{
    public GameObject catModel; // "Model" 上的 cat 模型
    public GameObject foxModel;
    public GameObject bearModel;
    public GameObject wolfModel;
    public GameObject tigerModel;
    public GameObject eagleModel;
    public GameObject rhinoModel;

    void Start()
    {
        // 初始状态下隐藏模型
        catModel.SetActive(false);
        foxModel.SetActive(false);
        bearModel.SetActive(false);
        wolfModel.SetActive(false);
        tigerModel.SetActive(false);
        eagleModel.SetActive(false);
        rhinoModel.SetActive(false);
    }

    public void ShowCatModel()  //貓
    {
        catModel.SetActive(true);
    }
    public void HideCatModel()
    {
        catModel.SetActive(false);
    }

    public void ShowFoxModel()  //狐狸
    {
        foxModel.SetActive(true);
    }
    public void HideFoxModel()
    {
        foxModel.SetActive(false);
    }

    public void ShowBearModel() //熊
    {
        bearModel.SetActive(true);
    }
    public void HideBearModel()
    {
        bearModel.SetActive(false);
    }

    public void ShowWolfModel() //狼
    {
        wolfModel.SetActive(true);
    }
    public void HideWolfModel()
    {
        wolfModel.SetActive(false);
    }

    public void ShowTigerModel() //老虎
    {
        tigerModel.SetActive(true);
    }
    public void HideTigerModel()
    {
        tigerModel.SetActive(false);
    }

    public void ShowEagleModel() //老鷹
    {
        eagleModel.SetActive(true);
    }
    public void HideEagleModel()
    {
        eagleModel.SetActive(false);
    }

    public void ShowRhinoModel() //犀牛
    {
        rhinoModel.SetActive(true);
    }
    public void HideRhinoModel()
    {
        rhinoModel.SetActive(false);
    }
}
