using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITask : MonoBehaviour
{
    public Text task1Text;   //任务1数量 -界面要显示的数量
    public Image task1Iamge;   //任务1图片 
    public myType.emenyType task1type;   //任务1类型 
    public int task1num;
    public Text task2Text;   //任务2数量 -界面要显示的数量
    public Image task2Iamge;  //任务2图片 
    public myType.emenyType task2type;   //任务1类型 
    public int task2num;
    public Text task3Text;   //任务2数量 -界面要显示的数量
    public Image task3Iamge;  //任务2图片 
    public myType.emenyType task3type;   //任务1类型 
    public int task3num;
    private string taskformat = "{0}\\{1}";
    // Use this for initialization
    void Awake()
    {
        Messenger.AddListener(EventName.initUITask, Init);
        Messenger.AddListener< myType.emenyType,int> (EventName.updateUITask, UpdateTaskText);
    }

    public void Init() //初始化界面
    {
        task1Text.color = Color.white;
        task2Text.color = Color.white;
        task3Text.color = Color.white;
        levelTask lt1 = Chapter.chapters[Chapter.currentChapter];
        task1Text.text = lt1.task1.targetNum.ToString() + "/0";
        task1num = lt1.task1.targetNum;
        task1Iamge.sprite = Chapter.LoadImg(lt1.task1.type);
        task1type = lt1.task1.type;
        task2Text.text = lt1.task2.targetNum.ToString() + "/0";
        task2num = lt1.task2.targetNum;
        task2Iamge.sprite = Chapter.LoadImg(lt1.task2.type);
        task2type = lt1.task2.type;
        task3Text.text = lt1.task3.targetNum.ToString() + "/0";
        task3num = lt1.task3.targetNum;
        task3Iamge.sprite = Chapter.LoadImg(lt1.task3.type);
        task3type = lt1.task3.type;
    }

    public void UpdateTaskText(myType.emenyType type, int finish)  //更新任务已完成数量
    {
        if (type == task1type)
        {
            task1Text.text = string.Format(taskformat, task1num, finish);
            if (finish == task1num) { task1Text.color = Color.yellow; }
        }
        else if (type == task2type)
        {
            task2Text.text = string.Format(taskformat, task2num, finish);
            if (finish == task2num) { task2Text.color = Color.yellow; }
        }
        else if (type == task3type)
        {
            task3Text.text = string.Format(taskformat, task3num, finish);
            if (finish == task3num) { task3Text.color = Color.yellow; }
        }
    }
}
