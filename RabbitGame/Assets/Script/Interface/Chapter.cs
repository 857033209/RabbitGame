using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chapter : MonoBehaviour {
    public static List<levelTask> chapters = new List<levelTask>(); //关卡
    public static int currentChapter=0; //目前所在的关卡
    public static int ballCount = 1; //小球開始的數量
    public Text chapterText;
    public Text task1Text;
    public Image task1Iamge;
    public Text task2Text;
    public Image task2Iamge;
    public Text task3Text;
    public Image task3Iamge;
    public GameObject backgroudimg;
    public static bool isCanSendBall=false;
    void Awake()
    {
        levelTask chapter1 = new levelTask();
        chapter1.chapterName = "第一章";
        TaskTarget t1 = new TaskTarget(3,myType.emenyType.Cabbage,1);
        chapter1.task1 = t1;
        TaskTarget t2 = new TaskTarget(3, myType.emenyType.ChineseCabbage, 1);
        chapter1.task2 = t2;
        TaskTarget t3= new TaskTarget(3, myType.emenyType.Eggplant, 1);
        chapter1.task3 = t3;
        chapters.Add(chapter1);

        levelTask chapter2 = new levelTask();
        chapter2.chapterName = "第二章";
        TaskTarget k1 = new TaskTarget(1, myType.emenyType.Luobo, 2);
        chapter2.task1 = k1;
        TaskTarget k2 = new TaskTarget(2, myType.emenyType.Mushroom, 2);
        chapter2.task2 = k2;
        TaskTarget k3 = new TaskTarget(3, myType.emenyType.Cabbage, 2);
        chapter2.task3 = k3;
        chapters.Add(chapter2);
    }
    public void InitChapterInterface(int chaper)
    {

        levelTask lt1 = chapters[chaper];
        chapterText.text = lt1.chapterName;
        task1Text.text= lt1.task1.targetNum.ToString();
        task1Iamge.sprite = LoadImg(lt1.task1.type);
        task2Text.text = lt1.task2.targetNum.ToString();
        task2Iamge.sprite = LoadImg(lt1.task2.type);
        task3Text.text = lt1.task3.targetNum.ToString();
        task3Iamge.sprite = LoadImg(lt1.task3.type);
    }
    // Use this for initialization
    void Start () {
        InitChapterInterface(currentChapter);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GameStart()//游戏开始
    {
        Messenger.Broadcast(EventName.destroyAll); //销毁已存在的道具
        Messenger.Broadcast(EventName.createEnemy);
        backgroudimg.SetActive(false);
        StartCoroutine(backgroudimgSetActive());
    }

    IEnumerator backgroudimgSetActive() //允许发射小球
    {
        yield return new WaitForSeconds(0.2f);
        isCanSendBall = true;
    }
    public void GameExit() //退出游戏
    {
        Application.Quit();
    }



    public static Sprite LoadImg(myType.emenyType type)
    {
        string name =LevelCreate.GetEmemyName(type);
        Sprite sprite;
        sprite = Resources.Load("Img/"+name, typeof(Sprite)) as Sprite;
        return sprite;
    }
}
