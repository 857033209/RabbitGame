using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    private Transform balllparent; //球的父物体
    private Transform endleft; //结束时的坐标
    private Transform endright; //结束时的坐标
    private Transform send; ////枪口的坐标
    public float shootingSpeed = 100000f; //小球发射速度
    private void Start()
    {
        balllparent = GameObject.Find("BallParent").transform;
        endleft = GameObject.Find("EndPositiong/left").transform;
        endright = GameObject.Find("EndPositiong/right").transform;
        send = GameObject.Find("Send").transform;
    }
    private void OnCollisionEnter2D(Collision2D collision)//被小球碰到时调用
    {
        //获取小球transform组件
        Transform tf = collision.transform;
        //在小球位置加载一个新小球
        GameObject newBall = Instantiate(Resources.Load("Prefab/Ball"), tf.position, tf.rotation) as GameObject;
        newBall.transform.parent = tf.parent;
        collision.transform.position = new Vector3(99999, 999999);// balllparent.position;
        newBall.transform.position = new Vector3(99999, 999999);// balllparent.position;
        collision.transform.GetComponent<Rigidbody2D>().gravityScale = 0f;
        newBall.GetComponent<BallMove>().state = BallState.Ready;
        collision.transform.GetComponent<BallMove>().state = BallState.Ready; 
        Rigidbody2D[] Balls=new Rigidbody2D[2];
        Balls[0]= newBall.GetComponent<Rigidbody2D>();
        Balls[1] = collision.transform.GetComponent<Rigidbody2D>();
        StartCoroutine(LineLaunch(Balls));
        //记录添加的小球
        Chapter.ballCount = Chapter.ballCount + 1;
    }



    IEnumerator LineLaunch(Rigidbody2D[] Balls) //用协程排队发射小球
    {
        float c = Random.Range(0f, 1f);
        Vector3 endpostion = new Vector3(endleft.position.x+c*(endright.position.x - endleft.position.x), endleft.position.y, 0);//获取瞄准结束点与枪口的方向向量
        Vector3 directionAttack = (endpostion - send.position).normalized;
        for (int i = 0; i < Balls.Length; i++) //挨个发射小球
        {
            //球往瞄准结束点方向寻路移动  
            Balls[i].GetComponent<Transform>().position = balllparent.position;
            Balls[i].GetComponent<BallMove>().state = BallState.Battle;
            //Balls[i].GetComponent<Rigidbody2D>().gravityScale = 100f;
            Balls[i].AddForce(directionAttack * shootingSpeed * Time.deltaTime);
            yield return new WaitForSeconds(0.2f); //每隔0.1秒发射一个
        }
    }
}
