using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class Scan : MonoBehaviour
{
    public float speed = 3.0f; // 横向移动速度
    //public Text logText; // 指向场景中的UI Text组件，用于显示物体名称
    public int maxLogCount = 40; // 最大记录数量
    public Model modelScript; // Model 脚本的引用
    public string displayText;

    public static event Action onFullScan;

    private List<string> touchedObjects = new List<string>(); // 存储碰到的物体的名称
    private bool isMoving = false; // 判断物体是否应该移动
    private bool isScheduledForDestruction = false; // 判断物体是否已经安排销毁

    void Awake()
    {
        onFullScan = null;
    }

    private void Start()
    {
        modelScript.HideCatModel();
        modelScript.HideFoxModel();
        modelScript.HideBearModel();
        modelScript.HideWolfModel();
        modelScript.HideTigerModel();
        modelScript.HideEagleModel();
        modelScript.HideRhinoModel();
    }
    void Update()
    {
        // 如果点击鼠标左键，则切换移动状态
        if (Input.GetMouseButtonDown(0))
        {
            isMoving = true;

            if (isMoving && !isScheduledForDestruction)
            {
                // 启动计时器，在3秒后调用DestroyObject方法
                Invoke("DestroyObject", 3f);
                Invoke("CheckSameLetter", 1.5f);// 越長的英文單字，可能會 Scan的時間不夠
                isScheduledForDestruction = true;
            }
        }

        // 如果 isMoving 为 true，则向左移动物体
        if (isMoving)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
            //transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // 记录碰到的物体的名称
        Debug.Log("Detected!");

        //如果 Trigger到的 Object的 Tag等於 "Letters"的話
        //紀錄碰到物體的名稱
        if (other.tag == "Letters")
        {
            touchedObjects.Add(other.gameObject.name);
        }
        
        

        // 限制记录的碰撞物体名称数量为最多 maxLogCount 个
        if (touchedObjects.Count > maxLogCount)
        {
            touchedObjects.RemoveAt(0); // 移除最早的一个碰撞物体名称
        }

        // 更新UI Text组件的内容
        UpdateLogText();

        // 销毁被撞到的物体
        Destroy(other.gameObject);
    }

    void UpdateLogText()
    {
        // 构建要显示的文本
        displayText = string.Join("", touchedObjects);

        // (Old) 反轉紀錄的字串
        //displayText = ReverseString(displayText);

        // (Old) 更新UI Text组件的内容
        //logText.text = displayText;
        //logText.text = logText.text + displayText;

        // 检查是否记录的文字等于 "cat"，如果是则显示 "m" 上的 "cat" 模型
        if (displayText == "cat")
        {
            modelScript.ShowCatModel();
        }
        else if (displayText == "fox")
        {
            modelScript.ShowFoxModel();
        }
        else if (displayText == "bear")
        {
            modelScript.ShowBearModel();
        }
        else if (displayText == "wolf")
        {
            modelScript.ShowWolfModel();
        }
        else if (displayText == "tiger")
        {
            modelScript.ShowTigerModel();
        }
        else if (displayText == "eagle")
        {
            modelScript.ShowEagleModel();
        }
        else if (displayText == "rhino")
        {
            modelScript.ShowRhinoModel();
        }


    }

    void CheckSameLetter()
    {
        //Debug.Log("***************");
        onFullScan?.Invoke();
    }

    //反轉字串(需要？)
    string ReverseString(string input)
    {
        char[] charArray = input.ToCharArray();
        System.Array.Reverse(charArray);
        return new string(charArray);
    }

    void DestroyObject()
    {
        // 销毁物体
        Destroy(gameObject);
    }
}
