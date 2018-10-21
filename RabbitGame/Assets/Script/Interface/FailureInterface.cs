using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailureInterface : MonoBehaviour
{
    public GameObject LevelInterface;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayGameAgain()
    {
        gameObject.SetActive(false);
        LevelInterface.SetActive(true);
        Chapter.isCanSendBall = true;
        Task.isInBlackHole = false;
    }
    public void GameExit() //退出游戏
    {
        Application.Quit();
    }
}
