using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RabitBtn : MonoBehaviour {

    public Rabbit myball;
    public Image img;
    public int ID;
    // Use this for initialization
    private void Awake()
    {
        Messenger.AddListener<int>(EventName.rabbitBallSend, DestroySelf);
        Messenger.AddListener<int>(EventName.rabbitBtnColor, ChangeCholor);
    }
    void Start () {
        UIEventTriggerListener.Get(gameObject).OnClick = ChangeAimBallType;

    }
	
    void ChangeAimBallType(GameObject go)  //按下小球时触发
    {
        Aim.destroyID = ID;
        Aim.sendType = Aim.SendType.rabbit;
        Chapter.isCanSendBall = true;
        Aim.ball = myball;
        img.color = new Color32(223, 243, 97, 255);
        Messenger.Broadcast<int>(EventName.rabbitBtnColor, myball.ID);
    }
    void ChangeCholor(int id)
    {
        if (id != myball.ID)
        {
            img.color = Color.white;
        }
    }
    private  void DestroySelf(int id) //销毁自己
    {
        if (id == myball.ID)
        {
            Messenger.RemoveListener<int>(EventName.rabbitBallSend, DestroySelf);
            Messenger.RemoveListener<int>(EventName.rabbitBtnColor, ChangeCholor);
            Destroy(gameObject); //销毁几何体自身
        }
    }

}
