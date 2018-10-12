using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chapter : MonoBehaviour {
    public static List<levelTask> chapters = new List<levelTask>(); //关卡
    public static int currentChapter=0; //目前所在的关卡
    public Text chapterText;
    public Text task1Text;
    public Text task2Text;
    public Text task3Text;
    public GameObject backgroudimg;
    public static bool isCanSendBall=false;
    void Awake()
    {
        levelTask chapter1 = new levelTask();
        chapter1.chapterName = "第一章";
        TaskTarget t1 = new TaskTarget(3,myType.emenyType.Diamond,1);
        chapter1.task1 = t1;
        TaskTarget t2 = new TaskTarget(3, myType.emenyType.Hexaton, 1);
        chapter1.task2 = t2;
        TaskTarget t3= new TaskTarget(3, myType.emenyType.Circle, 1);
        chapter1.task3 = t3;
        chapters.Add(chapter1);

        levelTask chapter2 = new levelTask();
        chapter2.chapterName = "第二章";
        TaskTarget k1 = new TaskTarget(1, myType.emenyType.Diamond, 2);
        chapter2.task1 = k1;
        TaskTarget k2 = new TaskTarget(2, myType.emenyType.Hexaton, 2);
        chapter2.task2 = k2;
        TaskTarget k3 = new TaskTarget(3, myType.emenyType.Circle, 2);
        chapter2.task3 = k3;
        chapters.Add(chapter2);
    }
    public void InitChapterInterface(int chaper)
    {

        levelTask lt1 = chapters[chaper];

        chapterText.text = lt1.chapterName;
        task1Text.text= lt1.task1.targetNum.ToString();
        Transform enemy1 =  CreateEnemy(lt1.task1.type);
        enemy1.transform.position = task1Text.transform.parent.position;
        enemy1.transform.parent = task1Text.transform.parent.transform;
        task2Text.text = lt1.task2.targetNum.ToString();
        Transform enemy2 =  CreateEnemy(lt1.task2.type);
        enemy2.transform.position = task2Text.transform.parent.position;
        enemy2.transform.parent = task2Text.transform.parent.transform;
        task3Text.text = lt1.task3.targetNum.ToString();
        Transform enemy3 =  CreateEnemy(lt1.task3.type);
        enemy3.transform.position = task3Text.transform.parent.position;
        enemy3.transform.parent = task3Text.transform.parent.transform;
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

    public Transform CreateEnemy(myType.emenyType type)
    {
        string name = LevelCreate.GetEmemyName(type);
        //根据名称找出敌人
        foreach (Transform tram in LevelCreate.enemys)
        {
            if (tram.name == name)
            {
                Transform enemy = Instantiate(tram);
                //给几何体赋一个随机颜色
                enemy.GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value);
                enemy.GetComponent<Renderer>().sortingOrder = 1;
                //  enemy.GetComponentInChildren<Text>().text = "";
                return enemy;
            }
        }
        return null;
    }
}
