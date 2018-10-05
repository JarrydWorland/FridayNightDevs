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
    void OnMouseDown()
    {
        if(playerC.attatchedTo != null)
        {
            forcfield.GetComponent<ForcefieldPull>().checkRot = true;
            playerC.attatchedTo.GetComponent<AsteroidBehavior>().imAttatched = false;

            if (playerC.attatchedTo != attatchedTo)
            {
                playerC.attatchedTo = attatchedTo;
                imAttatched = true;
                forcfield.SetActive(true);
                forcfield.transform.position = transform.position;
            }
            else
            {
                forcfield.SetActive(false);
                forcfield.GetComponent<ForcefieldPull>().checkRot = true;
                playerC.attatchedTo.GetComponent<AsteroidBehavior>().imAttatched = false;
                playerC.attatchedTo = null;
                playerC.attatched = false;
            }
        }
        else
        {
            playerC.attatchedTo = attatchedTo;
            imAttatched = true;
            forcfield.SetActive(true);
            forcfield.transform.position = transform.position;
        }

       
    }
    // Update is called once per frame
    void Update ()
    {
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
            if (!playerC.attatched)
            {
                playerC.attatched = true;
                playerC.attatchedTo = attatchedTo;
            }
        }
    }
}
