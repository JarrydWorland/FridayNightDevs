using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {

    public GameObject attatchedTo;
    private SoundManager sMan;
    private PlayerController player;
    

    public bool clockwise;
    public float speed;
	// Use this for initialization
	void Start () {
        sMan = SoundManager.Instance;
        player = PlayerController.Instance;
    }

    // Update is called once per frame
    void Update () {

        //howClose = 1 - ((distBW - endCurve) / (startCurve - endCurve)); //percentage in decimal of how far between the start and end point you are
        int degreeChange = 90;
        if (!clockwise)
        { degreeChange *= -1; }
        Vector2 pullDirection = ((Vector2)attatchedTo.transform.position - (Vector2)transform.position);
        Vector2 newPullDirection = player.Rotate(pullDirection, degreeChange);
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().AddForce(speed * newPullDirection);

        if (attatchedTo.GetComponent<Rigidbody2D>().velocity.magnitude != 0)
        {
            transform.position += (Vector3)attatchedTo.GetComponent<Rigidbody2D>().velocity * Time.deltaTime;
        }
    }

    private void  OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            sMan.PlaySound(sMan.bounceCol);

            Destroy(this.gameObject);
        }
    }
}
