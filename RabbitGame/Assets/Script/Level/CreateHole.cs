using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateHole : MonoBehaviour {

    public Transform leftHoleParent;  //洞的父物体
    public Transform rightHoleParent; //洞的父物体
    public LevelCreate levelcreate; //声明创建物体的类
    List<Transform> leftHoles = new List<Transform>(); //左边的兔子洞的挂点
    List<Transform> rightHoles = new List<Transform>(); //左边的兔子洞的挂点
    public bool[] leftarray;//用于记录某个关卡是否有挂点
    public bool[] rightarray;//用于记录某个关卡是否有挂点
    private void Start()
    {
        leftHoles = GetAllChild(leftHoleParent); //获取第一层子物体(关卡)添加进关卡集合中   
        rightHoles = GetAllChild(rightHoleParent); //获取第一层子物体(关卡)添加进关卡集合中  
        leftarray = new bool[leftHoles.Count];
        rightarray = new bool[rightHoles.Count];
        for (int i = 0; i < rightHoles.Count; i++)
        {
            leftarray[i] = false;
            rightarray[i] = false;
        }
        CreateLevelEnemy(3);
    }


    //产生敌人
    void CreateLevelEnemy(int holecount) //敌人类型、数量
    {
        if (holecount < 2) holecount = 2;
        int leftholecount = 1;
        int rightholecount = 1;
        if (holecount % 2 == 0)
        {
            leftholecount = holecount / 2;
            rightholecount = holecount / 2;

        }
        else
        {
            if(Random.Range(0,1)==1)
            {
                leftholecount = (holecount-1) / 2;
                rightholecount = (holecount - 1) / 2+1;
            }
            else
            {
                leftholecount = (holecount - 1) / 2+ 1;
                rightholecount = (holecount - 1) / 2 ;
            }
        }
        for (int i = 0; i < leftholecount; i++)
        {
            int leftindex= _CreateLeftHolePosition();
            leftarray[leftindex] = true;
            Transform parentposition = leftHoles[leftindex]; //获取底层关卡,物体将从该层产生
            Transform enemy = levelcreate.CreateHole();
            if (enemy == null) { return; }
            enemy.position = parentposition.position; //将几何体创建在该格子内
            enemy.parent = parentposition; //几何体作为该格子的子物体可随关卡层移动
        }
        for (int i = 0; i < rightholecount; i++)
        {
            int leftindex = _CreateRightHolePosition();
            rightarray[leftindex] = true;
            Transform parentposition = rightHoles[leftindex]; //获取底层关卡,物体将从该层产生
            Transform enemy = levelcreate.CreateHole();
            if (enemy == null) { return; }
            enemy.position = parentposition.position; //将几何体创建在该格子内
            enemy.parent = parentposition; //几何体作为该格子的子物体可随关卡层移动
        }
    }
    List<Transform> GetAllChild(Transform fatherObj) //获取子物体
    {
        //声明一个集合放所有子物体
        List<Transform> sonList = new List<Transform>();
        int number = fatherObj.childCount; //获取第一层子物体数量
        for (int i = 0; i < number; i++)
        {
            //将所有子物体添加进集合中
            sonList.Add(fatherObj.GetChild(i));
        }
        return sonList; //返回子物体集合
    }

    private int _CreateLeftHolePosition()//随机产生洞的位置
    {
        int i = 0;
        while (i < 10000)
        {
            int index = Random.Range(0, leftHoles.Count - 1);
            if(leftarray[index] ==false)
            {
                return index;
            }
        }
        for (int j = 0; j < leftHoles.Count; j++)
        {
            if (leftarray[j] == false)
            {
                return j;
            }
            
        }
        return 0;
    }
    private int _CreateRightHolePosition()//随机产生洞的位置
    {
        int i = 0;
        while (i < 10000)
        {
            int index = Random.Range(0, leftHoles.Count - 1);
            if (rightarray[index] == false)
            {
                return index;
            }
        }
        for (int j = 0; j < leftHoles.Count; j++)
        {
            if (rightarray[j] == false)
            {
                return j;
            }

        }
        return 0;
    }
}
