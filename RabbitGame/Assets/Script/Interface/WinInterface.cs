using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinInterface : MonoBehaviour {

    public GameObject start1;  
    public GameObject start2;
    public GameObject start3;
    public GameObject CommomRabit; //兔子的父物体
    public GameObject LevelInterface; //关卡界面
    public GameObject selfInterface; //自身界面
    // Use this for initialization
    void Start () {
        Messenger.AddListener(EventName.gameWin, InitInface);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void InitInface()
    {
        int num = CommomRabit.transform.GetChildCount(); //根据剩余的兔子批数计算等分等级
        if(num==0)
        {
            start1.SetActive(true);
            start2.SetActive(false);
            start3.SetActive(false);
        }
        else if(num == 1)
        {
            start1.SetActive(true);
            start2.SetActive(true);
            start3.SetActive(false);
        }
        else
        {
            start1.SetActive(true);
            start2.SetActive(true);
            start3.SetActive(true);
        }
        selfInterface.SetActive(true);
    }
    public void NextButton()  //下一关
    {
        Chapter.currentChapter = Chapter.currentChapter + 1;
        Messenger.Broadcast(EventName.initChapterInteface);
        LevelInterface.SetActive(true);
        selfInterface.SetActive(false);
    }

    public void AgainButton()  //再玩一次
    {
        Messenger.Broadcast(EventName.gameStart);
        selfInterface.SetActive(false);
    }

    public void ExitButton() //退出游戏
    {
        Application.Quit();
    }




}
