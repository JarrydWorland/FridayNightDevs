using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidBehavior : MonoBehaviour {

    public float speed = 30f;
    public bool clockwise = true;
    public bool rotating = true;
    public GameObject attatchedTo;
    public GameObject forcfield;
    public GameObject player;
    public bool imAttatched;
    
    private PlayerController playerC;
    public Collider2D myCol;

	// Use this for initialization
	void Start () {

        forcfield.SetActive(false);
        imAttatched = false;
        playerC = player.GetComponent<PlayerController>();
        myCol = attatchedTo.GetComponent<Collider2D>();
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.gameObject.tag == "Player")
        {
            
            if(playerC.attatchedTo!= null)
            { playerC.Detatch(); }
            

            if (attatchedTo.tag == "Bouncy")
            {
                //Bounce();
                player.GetComponent<ConstantSpeed>().Speed += 2;
            }
            if (attatchedTo.tag == "Damage")
            {
                //Damage();
                player.GetComponent<ConstantSpeed>().Speed -= 4;
                Destroy(attatchedTo);
            }
        }
    }

    void OnMouseDown()
    {
        if(playerC.attatchedTo != null) //your already attatched
        {
            forcfield.GetComponent<ForcefieldPull>().checkRot = true;
            playerC.attatchedTo.GetComponent<AsteroidBehavior>().imAttatched = false;

            if (playerC.attatchedTo != attatchedTo) //if what this object is not what youre currently attatched to
            {
                player.GetComponent<ConstantSpeed>().Speed += 3f;
                playerC.attatchedTo = attatchedTo;
                imAttatched = true;
                forcfield.SetActive(true);
                forcfield.transform.position = transform.position;
            }
            else //if youre attacthed to this object and then click it - detatches
            {
                playerC.Detatch();
            }
        }
        else //if in free fall - attatch
        {
            playerC.attatchedTo = attatchedTo;
            imAttatched = true;
            forcfield.SetActive(true);
            forcfield.transform.position = transform.position;
        }

       
    }
    // Update is called once per frame
    void Update () {
        if (rotating)
        {
            if (!clockwise)
            {
                transform.Rotate(Vector3.forward * Time.deltaTime * speed);
            }
            else
            {
                transform.Rotate(Vector3.forward * Time.deltaTime * -speed);

            }
        }

        if(imAttatched)
        {
            player.GetComponent<ConstantSpeed>().Speed -= 0.02f;
            if (!playerC.attatched)
            {
                playerC.attatched = true;
                playerC.attatchedTo = attatchedTo;
            }
        }
    }
}
