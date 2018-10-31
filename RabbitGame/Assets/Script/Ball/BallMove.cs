
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 挂小球上
/// </summary>
public class BallMove : MonoBehaviour
{
    float timer; //计时用
    //初始状态为准备状态
    public BallState state = BallState.Ready;
    //小球的碰撞体
    private Rigidbody2D selfRigidbody;
    //黑洞的位置
    public GameObject blackHole;

    //碰撞时调一次，用于打击几何体（敌人）
    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (state == BallState.Battle) //如果在战斗阶段
       {
          GetComponent<Rigidbody2D>().gravityScale = 100f; //碰到东西后重力为1
          if (collision.gameObject.tag == "Enemy") //如果碰到敌人    
          {
             //获取敌人数字
             Text enemyNumber = collision.transform.GetChild(0).GetComponent<Text>();
             //获取当前分数
             Text Score = GameObject.Find("ScoreText").GetComponent<Text>(); 
             if (tag == "BigBall") //如果自己是大球
             {
                //敌人数字-2
                enemyNumber.text = ((System.Convert.ToInt32(enemyNumber.text)) - 2).ToString();
                //当前分数+2
                Score.text = ((System.Convert.ToInt32(Score.text)) + 2).ToString();
             }
             else //如果自己是小球
             {
                //敌人数字-1
                enemyNumber.text = ((System.Convert.ToInt32(enemyNumber.text)) - 1).ToString();
                //当前分数+1
                Score.text = ((System.Convert.ToInt32(Score.text)) + 1).ToString();
             }
          }
          else if(collision.gameObject.tag == "Board")
            {
                Text enemyNumber = collision.transform.GetChild(0).GetComponent<Text>();
                int num= System.Convert.ToInt32(enemyNumber.text) -1;
                enemyNumber.text = num.ToString();
                if (num<1)
                {
                    Messenger.Broadcast(EventName.boardDestroy);
                }
            }
       }
    }
    //碰撞时持续调用,防止小球被卡住
    private void OnCollisionStay2D(Collision2D collision) 
    {
       if (collision.gameObject.tag == "Enemy") //如果碰撞的是敌人
       {
          timer += Time.deltaTime; //开始计时
          if (timer > 1) //一秒后还停留在那上面
          {
             switch (Random.Range(0, 4)) //随机方向弹开
             {
                case 0:
                   GetComponent<Rigidbody2D>().AddForce(transform.up * 10000f);
                   break;
                case 1:
                   GetComponent<Rigidbody2D>().AddForce(-transform.up * 10000f);
                   break;
                case 2:
                   GetComponent<Rigidbody2D>().AddForce(transform.right * 10000f);
                   break;
                case 3:
                   GetComponent<Rigidbody2D>().AddForce(-transform.up * 10000f);
                   break;
             }
          }
       }
    }
    //离开碰撞时调一次
    private void OnCollisionExit2D(Collision2D collision)
    {
        timer = 0; //计时归零
    }
    private void Awake()
    {
        Messenger.AddListener<BallState>(EventName.blackHoleOpen, _UpdateBallState);
    }

    private void Start()
    {
        selfRigidbody = GetComponent<Rigidbody2D>();
       
    }
    private void Update()
    {
        if(Task.isInBlackHole)
        {
            if (blackHole == null)
            {
                blackHole = GameObject.Find("EndPositiong/BlackHole");
            }
            selfRigidbody.velocity = Vector2.zero;      
            Vector3 directionMove = (blackHole.transform.position - gameObject.transform.position).normalized;//获取瞄准结束点与枪口的方向向量
            selfRigidbody.transform.Translate(directionMove * Time.deltaTime*1000);
        }

        transform.Rotate(0, 0, 0.0001f); //物体处于非完全静止状态,持续碰撞才会生效
        switch (state)
        {
            case BallState.Bore: //上膛阶段
                GetComponent<Rigidbody2D>().gravityScale = 0; //重力变为0
                break;
            case BallState.Ready: //准备阶段
                GetComponent<CapsuleCollider2D>().isTrigger = false; //关闭触发
                GetComponent<Rigidbody2D>().Sleep(); //小球停止不动
                break;
        }
    }
    private void _UpdateBallState(BallState ballstate) //设置小球的状态
    {
        state = ballstate;
    }
}