using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 挂LevelPanel上
/// </summary>
public class LevelMove : MonoBehaviour
{
    //需要做一个菜单，游戏死亡时弹出，在编辑器把死亡菜单拖进来
    public GameObject deathPanel;
    public Transform BossParent;
    public Transform rabitBtnParent;
    //游戏初始状态为存活
    public LevelState levelState = LevelState.life;
    //声明一个关卡集合用来管理每层关卡
    List<Transform> lineList = new List<Transform>();
    LevelCreate levelcreate; //声明创建物体的类
    public bool[,] array;//用于记录某个关卡是否有挂点
    int rows;  //关卡行数
    int columns; //关卡列数
    int count=0;//已使用的单元格数
    List<Transform> leftHoles = new List<Transform>(); //左边的兔子洞的挂点
    List<Transform> rightHoles = new List<Transform>(); //左边的兔子洞的挂点
    private void Awake()
    {
        Messenger.AddListener(EventName.createEnemy, CreatTaskEnemy);
        Messenger.AddListener<int>(EventName.createRabbit, CreatCommonRabbitBall);
    }
    private void Start()
    {
        levelcreate = GetComponent<LevelCreate>(); //获取创建关卡类
        lineList = GetAllChild(transform); //获取第一层子物体(关卡)添加进关卡集合中   
        //CreateLevel(); //游戏开始时创建一次底层的物体
        rows = lineList.Count;
        columns = GetAllChild(lineList[0]).Count;
        array = new bool[rows, columns];
        for (int i=0; i< rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                array[i, j] = false;
            }
        }


        CreateLevelBoss(myType.emenyType.BigBass, 1);
        //CreateLevelEnemy(myType.emenyType.Circle, 3);
        //CreateLevelEnemy(myType.emenyType.Hexaton, 1);
        //CreateLevelEnemy(myType.emenyType.Polygon,2);
        CreateLevelProp(myType.propType.BigProp, 1);
    }
    public  void CreatTaskEnemy()//生成敌人
    {
        levelTask lt = Chapter.chapters[Chapter.currentChapter];
        CreateLevelEnemy(lt.task1.type,lt.task1.targetNum);
        CreateLevelEnemy(lt.task2.type, lt.task2.targetNum);
        CreateLevelEnemy(lt.task3.type, lt.task3.targetNum);
    }

    public void CreatCommonRabbitBall(int rabbitRank)//生成兔子
    {
        for (int j = 0; j < 3; j++)
        {
            GameObject rbbitBtn = Instantiate(Resources.Load("Prefab/RabbitBtn/CommonRabbit"), Vector3.zero, Quaternion.identity) as GameObject;
            rbbitBtn.transform.parent = rabitBtnParent.transform;
            for (int i = 0; i < 3; i++)
            {
                string id = j + "" + i;
                Rabbit rb = new Rabbit(int.Parse(id), myType.rabitType.CommonRabbit, rabbitRank);
                rbbitBtn.GetComponent<RabitBtn>().myball = rb;
            }
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))  //测试用
        {
            CreateLevelBoss(myType.emenyType.BigBass, 1);
            CreateLevelEnemy(myType.emenyType.Cabbage, 3);
           // CreateLevelProp(myType.propType.BigProp, 3);
        }
        //在游戏运行时按Esc
        if (levelState == LevelState.life && Input.GetKeyDown(KeyCode.Escape))
        {
            levelState = LevelState.pause; //游戏状态变为暂停 
            deathPanel.SetActive(true); //启用菜单
            Time.timeScale = 0; //游戏暂停
        }
        //在暂停状态时按Esc
        else if (levelState == LevelState.pause && Input.GetKeyDown(KeyCode.Escape))
        {
            levelState = LevelState.life; //游戏状态变为运行
            deathPanel.SetActive(false); //禁用菜单
            Time.timeScale = 1; //游戏恢复
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

     //产生敌人
    void CreateLevelEnemy(myType.emenyType enemyType, int enemycount) //敌人类型、数量
    {
        for (int i = 0; i < enemycount; i++)
        {
            RowAndColumns rowcolumn = _CreatePosition();
            Transform row = lineList[rowcolumn.row]; //获取底层关卡,物体将从该层产生
            List<Transform> sonList = GetAllChild(row); //获取底层所有小方格                                                         //生成一个几何体（每次创建关卡至少有一个几何体）
            Transform enemy = levelcreate.CreateEnemy(1, enemyType);
            if(enemy==null)
            {
                return;
            }
            enemy.position = row.GetChild(rowcolumn.column).position; //将几何体创建在该格子内
            enemy.parent = row.GetChild(rowcolumn.column); //几何体作为该格子的子物体可随关卡层移动
        }
    }
    //产生boss
    void CreateLevelBoss(myType.emenyType enemyType, int bosscount=1) 
    {
        for (int i = 0; i < bosscount; i++)
        {                                                  
            Transform enemy = levelcreate.CreateEnemy(1, enemyType);
            if (enemy == null)
            {
                return;
            }
            enemy.position = _CreateBossPosition(); //将几何体创建在该格子内
            enemy.parent = BossParent; //几何体作为该格子的子物体可随关卡层移动
        }
    }
    //产生道具
    void CreateLevelProp( myType.propType propType, int propcount) //道具类型、数量
    {
        for (int i = 0; i < propcount; i++)
        {
            RowAndColumns rowcolumn = _CreatePosition();
            Transform row = lineList[rowcolumn.row]; //获取底层关卡,物体将从该层产生
            List<Transform> sonList = GetAllChild(row); //获取底层所有小方格                                                         //生成一个几何体（每次创建关卡至少有一个几何体）
            Transform enemy = levelcreate.CreateProp( propType);
            if (enemy == null) { return; }
            enemy.position = row.GetChild(rowcolumn.column).position; //将几何体创建在该格子内
            enemy.parent = row.GetChild(rowcolumn.column); //几何体作为该格子的子物体可随关卡层移动
        }
    }

    private RowAndColumns _CreatePosition()//随机产生道具的位置
    {
        int i = 0;
        while (i<10000)
        {
            i++;
            int row = Random.Range(0, rows-1);
            int column = Random.Range(0, columns-1);
            if(array[row, column]==false)
            {
                array[row, column] = true;
                count++;
                return new RowAndColumns(row, column);
            }
        }
        for(int j=0;j<rows;j++)
        {
            for(int k=0;k<columns;k++)
            {
                if (array[j, k] == false)
                {
                    array[j, k] = true;
                    count++;
                    return new RowAndColumns(j, k);
                }
            }
        }
        return  new RowAndColumns(0,0);
    }
    public Vector3 _CreateBossPosition()
    {
        int i = 0;
        while (i < 10000)
        {
            i++;
            int row = Random.Range(0, rows - 2);
            int column = Random.Range(0, columns - 2);
            if (array[row, column] == false && array[row, column+1] == false && array[row+1, column] == false && array[row + 1, column+1] == false)
            {
                array[row, column] = true;
                array[row, column + 1] = true; 
                array[row + 1, column] = true;
                array[row + 1, column + 1] = true;
                List<Transform> sonList1 = GetAllChild(lineList[row]); //获取底层所有小方格 
                float x=(sonList1[column].position.x+ sonList1[column+1].position.x)/2; //获取底层关卡,物体将从该层产生
                List<Transform> sonList2 = GetAllChild(lineList[row+1]); //获取底层所有小方格 
                float y = (sonList1[column].position.y + sonList2[column].position.y) / 2; //获取底层关卡,物体将从该层产生
                return new Vector3(x, y, 0);
            }
        }
        for (int j = 0; j < rows-1; j++)
        {
            for (int k = 0; k < columns-1; k++)
            {
                if (array[j, k] == false)
                {
                    if (array[j, k] == false && array[j, k + 1] == false && array[j + 1, k] == false && array[j + 1, k + 1] == false)
                    {
                        array[j, k] = true;
                        array[j, k + 1] = true;
                        array[j + 1, k] = true;
                        array[j + 1, k + 1] = true;
                        List<Transform> sonList1 = GetAllChild(lineList[j]); //获取底层所有小方格 
                        float x = (sonList1[k].position.x + sonList1[k + 1].position.x) / 2; //获取底层关卡,物体将从该层产生
                        List<Transform> sonList2 = GetAllChild(lineList[j + 1]); //获取底层所有小方格 
                        float y = (sonList1[k].position.y + sonList2[k].position.y) / 2; //获取底层关卡,物体将从该层产生
                        return new Vector3(x, y, 0);
                    }
                }
            }
        }
        return new Vector3(0, 0, 0);
    }
}


public struct RowAndColumns
{
    public int row;  //行
    public int column; //列
    public RowAndColumns(int r,int c)
    {
        this.row = r;
        this.column = c;
     }
}

