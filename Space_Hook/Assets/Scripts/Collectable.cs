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
    public bool hitCollectable;
    public float speed;
    public Vector2 direction;
	// Use this for initialization
	void Start () {
        sMan = SoundManager.Instance;
        hitCollectable = false;
        player = PlayerController.Instance;
        attatchedTo.GetComponent<AsteroidBehavior>().collectable = this.gameObject;
        direction = new Vector2(Random.value, Random.value).normalized;
    }

    // Update is called once per frame
    void Update () {
        //Moving around their attatched objects
        if (attatchedTo != null)
        {
            if (hitCollectable == false)
            {
                transform.RotateAround(attatchedTo.transform.position, transform.forward, 100f * Time.deltaTime);
            }
            if (attatchedTo.GetComponent<Rigidbody2D>().velocity.magnitude != 0)
            {
                transform.position += (Vector3)attatchedTo.GetComponent<Rigidbody2D>().velocity * Time.deltaTime;
            }
            between = ((Vector2)transform.position - (Vector2)attatchedTo.transform.position);
            distFromObj = between.magnitude;

            if (distFromObj < setDistance)
            {
                transform.position += (Vector3)between * Time.deltaTime;
            }
            else if (distFromObj > setDistance)
            {
                transform.position -= (Vector3)between * Time.deltaTime;
            } }
        else
        {
            transform.position += (Vector3)direction * 0.1f;
        }

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (collision.GetComponent<Collectable>())
        {
            hitCollectable = true;
            if (attatchedTo.GetComponent<PlayerController>())
            {
                int index = attatchedTo.GetComponent<PlayerController>().collectables.FindIndex(x => x == this.gameObject);
                transform.RotateAround(attatchedTo.transform.position, transform.forward,  index);
            }
            else if (attatchedTo.GetComponent<HomeBase>())
            {
                int index = attatchedTo.GetComponent<HomeBase>().collected.FindIndex(x => x == this.gameObject);
                transform.RotateAround(attatchedTo.transform.position, transform.forward,  index);
            }
        }
        else
        {
            hitCollectable = false;
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
        if(collision.GetComponent<WormHole>())
        {
            if (attatchedTo.GetComponent<AsteroidBehavior>())
            {
                attatchedTo.GetComponent<AsteroidBehavior>().collectable = null;
            }
            if (attatchedTo.GetComponent<PlayerController>())
            {
                
                attatchedTo.GetComponent<PlayerController>().collectables.Remove(this.gameObject);
                attatchedTo = null;
            }
        }
    }
}
