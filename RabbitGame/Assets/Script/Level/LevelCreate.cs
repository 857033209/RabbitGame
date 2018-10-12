using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 挂LevelPanel上
/// </summary>
public class LevelCreate : MonoBehaviour
{
    //几何体数组
   public static Transform[] enemys;
    //道具数组
    public static Transform[] stunts;
    //兔子洞
    public static Transform hole;
    void Awake()
    {
        //将所有几何体和道具加载到数组中
        enemys = LoadPrefab("Prefab/EnemyPrefab"); 
        stunts= LoadPrefab("Prefab/PropPrefab");
        hole = (Resources.Load("Prefab/Hole/hole") as GameObject).transform;
    }
    //加载预制件体返回数组
    public Transform[] LoadPrefab(string path)
    {
        Object[] obj = Resources.LoadAll(path);
        Transform[] th = new Transform[obj.Length];
        for (int i = 0; i < th.Length; i++)
        {
            th[i] = (obj[i] as GameObject).transform;
        }
        return th;
    }

    //生成兔子洞
    public Transform CreateHole()
    {
        return Instantiate(hole);
    }

    //根据类型生成道具
    public  Transform CreateProp(myType.propType type)
    {
        string name = _GetPropName(type);
        //根据名称找出道具
        foreach (Transform tram in stunts)
        {
            if (tram.name == name)
            {
                return Instantiate(tram);
            }
        }
        return null;
    }
    //根据类型生成敌人
    public Transform CreateEnemy(int level, myType.emenyType type)
    {
        string name =GetEmemyName(type);
        //根据名称找出敌人
        foreach (Transform tram in enemys)
        {
            if(tram.name== name)
            {
                Transform enemy = Instantiate(tram);
                //给几何体赋一个随机颜色
                enemy.GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value);
                //给几何体一个随机旋转角度
                enemy.rotation = Quaternion.Euler(0, 0, Random.Range(0, 90));
                //获取几何体子物体数字的Transform组件
                Transform tf = enemy.GetComponentInChildren<Text>().transform;
                //子物体不旋转
                tf.rotation = Quaternion.Euler(0, 0, 0);
                //获取血条
                enemy.GetComponentInChildren<Text>().text = _Blood(level);
                return enemy;
            }
        }
        return null;
    }
    private string _Blood(int rank)
    {
        if(rank>1)
        {
            return Random.Range(rank-1 * 10, rank * 10).ToString();
        }
        else if(rank ==-1)
        {
            return "";
        }
        else
        {
            return Random.Range(1, 10).ToString();
        }
    }
    public  static string GetEmemyName(myType.emenyType type)
    {
        switch(type)
        {
            case myType.emenyType.Circle: return "Circle"; 
            case myType.emenyType.Diamond: return "Diamond"; 
            case myType.emenyType.Hexaton: return "Hexaton";
            case myType.emenyType.Polygon: return "Polygon"; 
            case myType.emenyType.Square: return "Square";
            case myType.emenyType.Triangle: return "Triangle";
            case myType.emenyType.Boss: return "Boss";
            default: return "Circle"; 
        }
    }
    private string _GetPropName(myType.propType type)
    {
        switch (type)
        {
            case myType.propType.BigProp: return "BigProp";
            case myType.propType.CopyProp : return "CopyProp";
            default: return "BigProp";
        }
    }
}
