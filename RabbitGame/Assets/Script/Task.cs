using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Task 
{
    //任务收集
    public static List<TaskTarget> taskTargets = new List<TaskTarget>();

    //是否形成黑洞
    public static bool isInBlackHole = false;

    public static void TaskClear()   //任务收集归零
    {
        taskTargets.Clear();
    }

    public static void TaskAdd(myType.emenyType Type,int targerNum)   //添加任务
    {
        TaskTarget task = new TaskTarget(targerNum, Type, 0);
        taskTargets.Add(task);
    }

    public static void TaskCollectAdd(myType.emenyType Type)   //任务收集      
    {
        foreach (TaskTarget tt in taskTargets)
        {
            if (tt.type == Type)
            {
                tt.completenessNum = tt.completenessNum + 1;
                Messenger.Broadcast<myType.emenyType, int>(EventName.updateUITask, Type, tt.completenessNum);
                break;
            }
        }
        if (TaskFinishCheck())
        {
            Aim.gameState = Aim.GameState.Ready;
            Messenger.Broadcast(EventName.destroyAll); //销毁所有的游戏物体
            Messenger.Broadcast(EventName.updateBlackHole);//更新黑洞状态
            Messenger.Broadcast(EventName.gameWin);
            Messenger.Broadcast(EventName.stateNextBoard,false);
        }
    }

    public static bool TaskFinishCheck()   //任务是否完成
    {
        foreach (TaskTarget tt in taskTargets)
        {
            if (tt.completenessNum < tt.targetNum)
            {
                return false;
            }
        }
        return true;
    }
}
//关卡收集目标
public class TaskTarget
{
    public  int targetNum ;  //目标数量
    public myType.emenyType type;   //目标类型
    public int completenessNum=0;        //已完成数量
    public int rank;        //目标等级
    public  TaskTarget(int targetNum1, myType.emenyType type1, int rank1=1)
    {
        this.rank = rank1;
        this.targetNum = targetNum1;
        this.type = type1;
    }
}

//daboss
public class TaskBigBoss
{
    public myType.emenyType type;   //目标类型
    public int rank;        //目标等级
    public int targetNum;  //目标数量
    public TaskBigBoss(int targetNum1, myType.emenyType type1, int rank1 = 1)
    {
        this.targetNum = targetNum1;
        this.rank = rank1;
        this.type = type1;
    }
}

public class levelTask  //关卡任务
{
    public string chapterName;//章节名
    public int BoardBlood;//木板血量
    public TaskTarget task1;//任务1
    public TaskTarget task2;//任务2
    public TaskTarget task3;//任务3
    public TaskBigBoss bigBoss;  //boss
}