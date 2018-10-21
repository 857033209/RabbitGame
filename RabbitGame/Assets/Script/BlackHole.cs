using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour {
  
    private int hasDestroyBallCount = 0;//已被销毁的小球数量
    public GameObject FailureInterface;
    // Use this for initialization
    void Start () {
		
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
            Chapter.ballCount = 1;
            hasDestroyBallCount = 0;
            FailureInterface.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
