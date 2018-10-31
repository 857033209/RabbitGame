using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Aim : MonoBehaviour //挂枪口Muzzle上
{
    public GameObject ballParent; //把Balls拖进去   
    public static Rigidbody2D[] allBall; //声明一个数组用来管理所有小球
    LineRenderer aimLine; //声明瞄准线
    public Transform CriticalPointLeft; //把左边界拖进去
    public Transform CriticalPointRight; //把右边界拖进去
    public float shootingSpeed = 1000f; //小球发射速度
    public GameObject levelPanel; //把LevelPanel拖进去
    public static Rabbit ball; //要发射的小球
    public static GameState gameState = GameState.Ready; //游戏状态
    void Start()
    {
        Time.timeScale = 1; //游戏时间正常      
        allBall = ballParent.GetComponentsInChildren<Rigidbody2D>();//初始化(获取当前所有小球)
        aimLine = GetComponent<LineRenderer>(); //获取枪口上的LineRenderer组件
    }
    void Update()
    {
        //当游戏状态为活着时
        if (levelPanel.GetComponent<LevelMove>().levelState == LevelState.life)
        {
            if (Chapter.isCanSendBall) //小球是否可以发射
            {         
                allBall = ballParent.GetComponentsInChildren<Rigidbody2D>(); //再次获取所有小球
                AimLaunch(); //关卡上升完成后可进行瞄准发射
            }
        }
    }
    //刷新小球
    public void RefreshBalls()
    {
        allBall = ballParent.GetComponentsInChildren<Rigidbody2D>(); //再次获取所有小球
    }

    void AimLaunch() //瞄准发射
    {
        if (ClickPositonIsInRange(Camera.main.ScreenToWorldPoint(Input.mousePosition), CriticalPointLeft, CriticalPointRight) == false)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0)) //点击鼠标左键
        {
            aimLine.SetPosition(0, transform.position); //在枪口处生成瞄准线起点
        }
        if (Input.GetMouseButton(0)) //按住鼠标左键不放
        {
            //获取鼠标坐标
            Vector3 v = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //限制瞄准范围
            v = DirectionRestriction(v, CriticalPointLeft, CriticalPointRight);
            //将被限制过的鼠标坐标实时给瞄准线结束点
            aimLine.SetPosition(1, new Vector2(v.x, v.y));
        }
        if (Input.GetMouseButtonUp(0)) //抬起鼠标左键
        {
            //StartCoroutine(LineLaunch(transform.position)); //启动协程发射小球
            StartCoroutine(LineLaunch2(transform.position));
            aimLine.SetPosition(1, transform.position); //让结束点和起点重合(撤销瞄准线) 
        }
    }

    IEnumerator LineLaunch(Vector3 muzzlePos) //用协程排队发射小球
    {
        Chapter.isCanSendBall = false;
        Vector3 pos1 = aimLine.GetPosition(1);//获取瞄准线结束点坐标
        Vector3 directionAttack = (pos1 - muzzlePos).normalized;//获取瞄准结束点与枪口的方向向量

        for (int i = 0; i < allBall.Length; i++) //挨个发射小球
        {  
            //被发射的小球变为战斗状态
            allBall[i].GetComponent<BallMove>().state = BallState.Battle;
            //球往瞄准结束点方向寻路移动  
            allBall[i].AddForce(directionAttack * shootingSpeed * Time.deltaTime);
            // allBall[i].velocity = directionAttack * shootingSpeed;
            yield return new WaitForSeconds(0.1f); //每隔0.1秒发射一个
        }
    }

    private GameObject CreatBall(myType.rabitType type)
    {
        string ballPath = "";
        if (type == myType.rabitType.CommonRabbit)
        {
            ballPath = "Prefab/Ball";

        }
        GameObject newBall = Instantiate(Resources.Load(ballPath), Vector3.zero, Quaternion.identity) as GameObject;
        return newBall;
    }

    IEnumerator LineLaunch2(Vector3 muzzlePos) //用协程排队发射小球
    {
        gameState = GameState.Battle;
        Chapter.isCanSendBall = false;
        Vector3 pos1 = aimLine.GetPosition(1);//获取瞄准线结束点坐标
        Vector3 directionAttack = (pos1 - muzzlePos).normalized;//获取瞄准结束点与枪口的方向向量  
        Messenger.Broadcast<int>(EventName.rabbitBallSend, ball.ID);
        for (int i = 0; i < ball.num; i++) //挨个发射小球
        {
            GameObject cloneObj = CreatBall(ball.type);// Instantiate(newBall) as GameObject;
            cloneObj.transform.parent = ballParent.transform;
            cloneObj.transform.position = ballParent.transform.position;
            cloneObj.transform.GetComponent<Rigidbody2D>().gravityScale = 0f;
            //记录添加的小球
            Chapter.ballCount = Chapter.ballCount + 1;
            //被发射的小球变为战斗状态
            cloneObj.GetComponent<BallMove>().state = BallState.Battle;
            //球往瞄准结束点方向寻路移动  
            cloneObj.transform.GetComponent<Rigidbody2D>().AddForce(directionAttack * shootingSpeed * Time.deltaTime);
            yield return new WaitForSeconds(0.2f); //每隔0.1秒发射一个
        }
    }


    //限定枪口瞄准方向
    Vector3 DirectionRestriction(Vector3 v, Transform left, Transform right)
    {
        //最左不能左过左边界
        if (v.x < left.position.x)
            v.x = left.position.x;
        //最右不能右过右边界
        if (v.x > right.position.x)
            v.x = right.position.x;
        //高度不能超过边界
        if (v.y > left.position.y)
            v.y = left.position.y;
        return v; //返回被限制后的坐标
    }

    //限定枪口瞄准方向
    bool ClickPositonIsInRange(Vector3 v, Transform left, Transform right)
    {
        //最左不能左过左边界
        if (v.x < left.position.x)
            return false;
        //最右不能右过右边界
        if (v.x > right.position.x)
            return false;
        //高度不能超过边界
        if (v.y > left.position.y)
            return false;
        return true; //返回被限制后的坐标
    }

    public enum GameState //游戏状态
    {
        Ready, //准备阶段
        Battle, //战斗阶段
        InBlackHole, //黑洞阶段
    }
}
