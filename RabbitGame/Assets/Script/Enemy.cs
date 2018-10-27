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

    }
    private void DestroySelf() //销毁自己
    {
       // Messenger.RemoveListener(EventName.destroyAll, DestroySelf);
        Destroy(gameObject); //销毁几何体自身
    }
}
