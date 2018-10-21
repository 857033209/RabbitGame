using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Board : MonoBehaviour {

    public GameObject self;
    Text number; //声明数字

    public GameObject BlackHole;

    private void Start()
    {
        number = GetComponentInChildren<Text>(); //找到子物体(数字)
        Messenger.AddListener<int>(EventName.initBoard, InitBoard);

    }
    private void Update()
    {
        if (Convert.ToInt32(number.text) < 1) //如果数字小于1时
        {
           // Destroy(gameObject); //销毁几何体自身
            self.SetActive(false);
            Task.isInBlackHole = true;
            BlackHole.SetActive(true);
            Messenger.Broadcast<BallState>(EventName.blackHoleOpen, BallState.Ready);
        }

    }
    public void InitBoard(int bloodNum)
    {
        self.SetActive(true);
        number.text = bloodNum.ToString();
    }
}
