using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class propBtn : MonoBehaviour {


    public int ID;
    public Image img;
    public int rank; //道具等级
    public Aim.SendType type;
    // Use this for initialization
    private void Awake()
    {
        Messenger.AddListener<int>(EventName.rabbitBallSend, DestroySelf);
        Messenger.AddListener<int>(EventName.rabbitBtnColor, ChangeCholor);
    }
    void Start()
    {
        UIEventTriggerListener.Get(gameObject).OnClick = ChangeAimBallType;

    }
    void ChangeAimBallType(GameObject go)  //按下小球时触发
    {
        Aim.sendType = type;
        Aim.destroyID = ID;
        Chapter.isCanSendBall = true;
        img.color = new Color32(223, 243, 97, 255);
        Messenger.Broadcast<int>(EventName.rabbitBtnColor, ID);
    }

    void ChangeCholor(int id)
    {
        if (id != ID)
        {
            img.color = Color.white;
        }
    }

    private void DestroySelf(int id) //销毁自己
    {
        if (id == ID)
        {
            Messenger.RemoveListener<int>(EventName.rabbitBallSend, DestroySelf);
            Messenger.RemoveListener<int>(EventName.rabbitBtnColor, ChangeCholor);
            Destroy(gameObject); //销毁几何体自身
        }
    }
}
