using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Old_Scan : MonoBehaviour
{
    public float speed = 1.0f; // 横向移动速度
    public Text logText; // 指向场景中的UI Text组件，用于显示物体名称
    public int maxLogCount = 10; // 最大记录数量
    public Model modelScript; // Model 脚本的引用

    private List<string> touchedObjects = new List<string>(); // 存储碰到的物体的名称
    private bool isMoving = false; // 判断物体是否应该移动
    private bool isScheduledForDestruction = false; // 判断物体是否已经安排销毁

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
                isScheduledForDestruction = true;
            }
        }

        // 如果 isMoving 为 true，则向右移动物体
        if (isMoving)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
}

    void OnTriggerEnter(Collider other)
    {
        // 记录碰到的物体的名称
        touchedObjects.Add(other.gameObject.name);

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
        string displayText = string.Join("", touchedObjects);

        // 更新UI Text组件的内容
        logText.text = displayText;

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

        else
        {
            modelScript.HideCatModel();
            modelScript.HideFoxModel();
            modelScript.HideBearModel();
            modelScript.HideWolfModel();
            modelScript.HideTigerModel();
            modelScript.HideEagleModel();
            modelScript.HideRhinoModel();

        }
        

    }

        void DestroyObject()
    {
        // 销毁物体
        Destroy(gameObject);
    }
}
