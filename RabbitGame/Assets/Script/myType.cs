using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myType : MonoBehaviour {

    //敌人类型
   public enum emenyType
    {
        None=0,
        Triangle = 1,
        Square = 2,
        Polygon = 3,
        Diamond = 4,
        Hexaton = 5,
        Circle = 6,
        Boss=7,
    }
    //道具类型
    public enum propType
    {
        None = 0,
        BigProp = 1,
        CopyProp = 2,
    }

}
