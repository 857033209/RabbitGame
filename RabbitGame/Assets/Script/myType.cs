using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myType : MonoBehaviour {

    //敌人类型
   public enum emenyType
    {
        None=0,
        Pepper = 1,
        Mushroom = 2,
        Luobo = 3,
        Eggplant = 4,
        ChineseCabbage = 5,
        Cabbage = 6,
        BigBass = 7,
        SmallBoss = 8,
    }
    //道具类型
    public enum propType
    {
        None = 0,
        BigProp = 1,
        CopyProp = 2,
    }

    //发射兔子的类型
    public enum rabitType
    {
        CommonRabbit = 0,
        RocketRabbit = 1,
    }
}
