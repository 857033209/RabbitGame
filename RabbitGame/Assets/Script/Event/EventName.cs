using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventName {

    public static string blackHoleOpen = "blackHoleOpen";  //黑洞开启是触发
    public static string createEnemy = "createEnemy";  //产生敌人
    public static string destroyAll = "destroyAll";  //销毁道具和敌人
    public static string initBoard = "initBoard"; //初始化木板
    public static string rabbitBallSend = "rabbitBallSend"; //兔子被发誓时触发
    public static string rabbitBtnColor = "rabbitBtnColor"; //点击兔子按钮类型时触发
    public static string createRabbit = "createRabbit";  //产生兔子
    public static string boardDestroy = "boardDestroy";  //木板被销毁
    public static string initUITask = "initUITask";  //初始化任务收集面板
    public static string updateUITask = "updateUITask";  //初始化任务收集面板
    public static string initChapterInteface = "initChapterInteface";  //初始化关卡界面
    public static string gameStart = "gameStart";  //开始游戏
    public static string gameWin = "gameWin";  //胜利
    public static string updateBlackHole = "updateBlackHole";  //更新黑洞状态
}
