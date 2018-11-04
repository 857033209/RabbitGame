using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class NextBoard : MonoBehaviour {

    public GameObject self;
    Text number; //声明数字
    private bool state = false;//木板是否处于可移动状态
    private float initPosX = 1.0f;//木板初始X轴的位置
    public float speed = 15.0f;//木板移动的速度
    private int boardLife = 0;

    public GameObject Board;

    private void Awake()
    {
        initPosX = self.transform.position.x;
        Messenger.AddListener<int>(EventName.initNextBoard, InitNextBoard);
        Messenger.AddListener<bool>(EventName.stateNextBoard, ChangeState);
    }
    private void Start()
    {
        number = GetComponentInChildren<Text>(); //找到子物体(数字)
    }

    private void Update()
    {
        if (state)
        {
            Vector3 moveDirection = transform.right;
            transform.position -= moveDirection * Time.deltaTime * speed;
            if (transform.position.x < 0)
            {
                boardLife = (int)(boardLife * 1.5f);
                Messenger.Broadcast<int>(EventName.initBoard, boardLife);
                InitNextBoard(boardLife);
            }
        }
    }
    private void ChangeState(bool isMove)
    {
        state = isMove;
    }
    
    public void InitNextBoard(int bloodNum)
    {
        self.SetActive(true);
        boardLife = bloodNum;
        number.text = bloodNum.ToString();
        self.transform.position = new Vector3(initPosX, self.transform.position.y, self.transform.position.z);
    }
}
