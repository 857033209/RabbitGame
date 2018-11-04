using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    public Vector3 endPostion;
    public int destructivePower;
    public float speed = 1000f;
    public GameObject boomImge;
    public bool isfrist = true;
    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
      //  Debug.Log(transform.position);
        Debug.Log(endPostion);
        if (isfrist)
        {
            gameObject.transform.Translate((endPostion - transform.position).normalized * speed * Time.deltaTime, Space.World);
            if ((transform.position.y - endPostion.y) <= 10)
            {
                isfrist = false;
                boomImge.SetActive(true);
                StartCoroutine(OnDestroySelf());
            }
        }
    }

    IEnumerator OnDestroySelf()
    {
        yield return new WaitForSeconds(0.8f);
        Destroy(gameObject);
    }
}
