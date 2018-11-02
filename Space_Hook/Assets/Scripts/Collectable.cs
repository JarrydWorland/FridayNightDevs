using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {

    public GameObject attatchedTo;
    private SoundManager sMan;
    private PlayerController player;

    public float distFromObj;
    public float setDistance;
    public Vector2 between;
    public bool clockwise;
    public float speed;
	// Use this for initialization
	void Start () {
        sMan = SoundManager.Instance;
        player = PlayerController.Instance;
        attatchedTo.GetComponent<AsteroidBehavior>().collectable = this.gameObject;
    }

    // Update is called once per frame
    void Update () {
        //Moving around their attatched objects
        transform.RotateAround(attatchedTo.transform.position, transform.forward, 100f * Time.deltaTime);
        if (attatchedTo.GetComponent<Rigidbody2D>().velocity.magnitude != 0)
        {
            transform.position += (Vector3)attatchedTo.GetComponent<Rigidbody2D>().velocity * Time.deltaTime;
        }
        between = ((Vector2)transform.position - (Vector2)attatchedTo.transform.position);
        distFromObj = between.magnitude;

        if(distFromObj < setDistance)
        {
            transform.position += (Vector3)between * Time.deltaTime;
        }
        else if(distFromObj > setDistance)
        {
            transform.position -= (Vector3)between * Time.deltaTime;
        }

    }

    private void  OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            sMan.PlaySound(sMan.bounceCol);
            if (attatchedTo.GetComponent<AsteroidBehavior>())
            {
                attatchedTo.GetComponent<AsteroidBehavior>().collectable = null;
                attatchedTo = player.gameObject;
                player.GetComponent<PlayerController>().collectables.Add(this.gameObject);
            }
        }
    }
}
