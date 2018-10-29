using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 挂每个几何体上
/// </summary>
public class Enemy : MonoBehaviour 
{
    public myType.emenyType enemyType;
    Text number; //声明数字

    private float attackTimer  =0;
    public float attackTime = 2000.0f;
    private void Start()
    {
      //  Messenger.AddListener(EventName.destroyAll, DestroySelf);
        number = GetComponentInChildren<Text>(); //找到子物体(数字)
    }
    private void Update()
    {
        if (Convert.ToInt32(number.text) < 1) //如果数字小于1时
        {
            Destroy(gameObject); //销毁几何体自身
            Task.TaskCollectAdd(enemyType);
        }

        if (enemyType == myType.emenyType.BigBass)
        {
            //计时器
            if (attackTimer > 0)
                attackTimer -= Time.deltaTime;
            if (attackTimer < 0)
                attackTimer = 0;
            if (attackTimer == 0)
            {
                SendBettlut();
                attackTimer = attackTime;
            }
        }
    }

    public void SendBettlut()   //每隔2S执行一次
    {
        GameObject ballut = Instantiate(Resources.Load("Prefab/Bullet/bullet"), Vector3.zero, Quaternion.identity) as GameObject;
        ballut.transform.parent = gameObject.transform;
        ballut.transform.position = gameObject.transform.position;
        //ballut.transform.GetComponent<bullet>().startPosition = gameObject.transform;
    }

    private void DestroySelf() //销毁自己
    {
       // Messenger.RemoveListener(EventName.destroyAll, DestroySelf);
        Destroy(gameObject); //销毁几何体自身
    }
}
