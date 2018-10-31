using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chapter : MonoBehaviour {
    public static List<levelTask> chapters = new List<levelTask>(); //关卡
    public static int currentChapter=0; //目前所在的关卡
    public static int ballCount = 0; //小球開始的數量
    public static int rabbitRank = 3; //兔子等级
    public Text chapterText;  //章节名称
    public Text task1Text;   //任务1数量 -界面要显示的数量
    public Image task1Iamge;   //任务1图片 
    public Text task2Text;   //任务2数量 -界面要显示的数量
    public Image task2Iamge;  //任务2图片 
    public Text task3Text;   //任务2数量 -界面要显示的数量
    public Image task3Iamge;  //任务2图片 
    public GameObject backgroudimg;   //章节背景图片 
    public static bool isCanSendBall=false;  //是否可以发射球
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
        chapter1.BoardBlood = 10;
        chapters.Add(chapter1);

        levelTask chapter2 = new levelTask();
        chapter2.chapterName = "第二章";
        TaskTarget k1 = new TaskTarget(3, myType.emenyType.Luobo, 2);
        chapter2.task1 = k1;
        TaskTarget k2 = new TaskTarget(3, myType.emenyType.Mushroom, 2);
        chapter2.task2 = k2;
        TaskTarget k3 = new TaskTarget(3, myType.emenyType.Cabbage, 2);
        chapter2.task3 = k3;
        chapter2.BoardBlood = 10;
        TaskBigBoss bg2 = new TaskBigBoss(1,myType.emenyType.SmallBoss, 3);
        chapter2.bigBoss = bg2;
        chapters.Add(chapter2);

        levelTask chapter3 = new levelTask();
        chapter3.chapterName = "第三章";
        TaskTarget gg1 = new TaskTarget(3, myType.emenyType.Pepper, 3);
        chapter3.task1 = gg1;
        TaskTarget gg2 = new TaskTarget(3, myType.emenyType.Mushroom, 3);
        chapter3.task2 = gg2;
        TaskTarget gg3 = new TaskTarget(3, myType.emenyType.Eggplant, 3);
        chapter3.task3 = gg3;
        TaskBigBoss bg3 = new TaskBigBoss(1,myType.emenyType.BigBass, 3);
        chapter3.bigBoss = bg3;
        chapter3.BoardBlood = 10;
        chapters.Add(chapter3);
    }
    public void InitChapterInterface()
    {
        levelTask lt1 = chapters[currentChapter];
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
        InitChapterInterface();
        Messenger.AddListener(EventName.gameStart, GameStart);
        Messenger.AddListener(EventName.initChapterInteface, InitChapterInterface);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GameStart()//游戏开始
    {
        InitTask();//初始化任务
        Messenger.Broadcast(EventName.initUITask);//初始化任务收集面板
        Messenger.Broadcast(EventName.destroyAll); //销毁所有的游戏物体
        Messenger.Broadcast(EventName.createEnemy);//生成敌人
        Messenger.Broadcast<int>(EventName.createRabbit, rabbitRank);//创建兔子
        backgroudimg.SetActive(false);//关闭章节界面
      //  StartCoroutine(backgroudimgSetActive()); //允许发射小球
    }

    public void InitTask()
    {
        levelTask lt = chapters[currentChapter];
        Task.TaskClear();
        Task.TaskAdd(lt.task1.type, lt.task1.targetNum);
        Task.TaskAdd(lt.task2.type, lt.task2.targetNum);
        Task.TaskAdd(lt.task3.type, lt.task3.targetNum);
        Messenger.Broadcast<int>(EventName.initBoard, lt.BoardBlood);
    }

    IEnumerator backgroudimgSetActive() //允许发射小球
    {
        yield return new WaitForSeconds(0.2f);
       //  isCanSendBall = true;
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
