using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour {
    public GameObject ballParent;//小球父物体
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)//被小球碰到时调用
    {
        Destroy(collision.transform.gameObject);
        Aim.allBall = ballParent.GetComponentsInChildren<Rigidbody2D>();//初始化(获取当前所有小球)
    }
}
