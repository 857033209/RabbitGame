using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bullet : MonoBehaviour {


    public Transform startPosition;
    public Transform endPostion;
    public Text boardText;
    public float speed = 0.00001f;

    // Use this for initialization
    void Start () {
        endPostion = GameObject.Find("EndPositiong/endpositon").transform;
        boardText = GameObject.Find("Board/image/Text").GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate((endPostion.position - transform.position).normalized* speed * Time.deltaTime,Space.World);
     //   transform.position = Vector3.MoveTowards(startPosition.position, endPostion.position, speed * Time.deltaTime);
        if((transform.position.y-endPostion.position.y)<=10)
        {
            int num = System.Convert.ToInt32(boardText.text) - 1;
            boardText.text = num.ToString();
            if (num < 1)
            {
                Messenger.Broadcast(EventName.boardDestroy);
            }
            Destroy(gameObject);
        }
	}
}
