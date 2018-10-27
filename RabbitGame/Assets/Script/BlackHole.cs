using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackHole : MonoBehaviour {
  
    private int hasDestroyBallCount = 0;//已被销毁的小球数量
    public GameObject FailureInterface;
    public GameObject CommomRabitParent;
    public GameObject PropRabitParent;
    public GameObject Board;
    // Use this for initialization
    void Start () {
        Messenger.AddListener(EventName.updateBlackHole, UpdageBlackHole);

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)//被小球碰到时调用
    {
        Destroy(collision.transform.gameObject);
        hasDestroyBallCount++;
        if (Chapter.ballCount== hasDestroyBallCount)
        {
            Chapter.ballCount = 0;
            hasDestroyBallCount = 0;
            if (CommomRabitParent.transform.childCount== 0 && PropRabitParent.transform.childCount == 0)
            {
                FailureInterface.SetActive(true);
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(false);
                Task.isInBlackHole = false;
                Board.GetComponentInChildren<Text>().text = Chapter.chapters[Chapter.currentChapter].BoardBlood.ToString();
                Board.SetActive(true);
            }


        }
    }
    private void UpdageBlackHole()
    {
        Debug.LogError("666666666666666666");
        Chapter.ballCount = 0;
        hasDestroyBallCount = 0;
        Task.isInBlackHole = false;
        gameObject.SetActive(false);
        Board.SetActive(true);
    }
}
