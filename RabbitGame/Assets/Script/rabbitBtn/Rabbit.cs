using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit
{

    public myType.rabitType type;  //兔子类型
    public int num;  //兔子数量
    public int ID;   //兔子ID

    public Rabbit(int _Id, myType.rabitType _type, int _num)
    {
        type = _type;
        num = _num;
        ID = _Id; //兔子ID
    }
}
