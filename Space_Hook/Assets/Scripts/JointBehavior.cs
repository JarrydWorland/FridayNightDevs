using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointBehavior : MonoBehaviour {

    public GameObject pullTowards;
    public Vector2 direction;
    public float pull;

	// Use this for initialization
	void Start () {
        direction = new Vector2(pullTowards.transform.position.x - transform.position.x, pullTowards.transform.position.y - transform.position.y).normalized;
    }
	
	// Update is called once per frame
	void Update () {
        direction = new Vector2(pullTowards.transform.position.x - transform.position.x, pullTowards.transform.position.y - transform.position.y).normalized;
        if((transform.position - pullTowards.transform.position).magnitude > GetComponent<CircleCollider2D>().radius)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
        GetComponent<Rigidbody2D>().AddForce(direction * pull);
    }
}
