using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidBehavior : MonoBehaviour {

    public float speed = 30f;
    public bool clockwise = true;
    public bool rotating = true;
    public GameObject attatchedTo;
    public GameObject forcfield;
    public GameObject hookAttatch;
    public GameObject player;
    public float maxDistance;
    public bool imAttatched;
    
    private PlayerController playerC;
    

	// Use this for initialization
	void Start () {
        forcfield.SetActive(false);
        imAttatched = false;
        playerC = player.GetComponent<PlayerController>();
        maxDistance = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if(imAttatched == false)
        {
            maxDistance = 0;
        }
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
            if(playerC.state != "attatch")
            {
                playerC.state = "attatch";
                playerC.attatchedTo = attatchedTo;
                if(maxDistance == 0)
                {
                    maxDistance = (playerC.transform.position - forcfield.transform.position).magnitude;
                }
                
            }
            if (maxDistance < 2.3)
            {
                maxDistance = 2.3f;
            }

            forcfield.GetComponent<DistanceJoint2D>().distance = maxDistance;

            Vector2 hookDirection = new Vector2(playerC.transform.position.x - forcfield.transform.position.x, playerC.transform.position.y - forcfield.transform.position.y).normalized;
            Vector2 currentHookDirection = new Vector2(hookAttatch.transform.position.x - forcfield.transform.position.x, hookAttatch.transform.position.y - forcfield.transform.position.y);
            float angle = Vector2.SignedAngle(currentHookDirection, hookDirection);

            hookAttatch.transform.RotateAround(forcfield.transform.position, transform.forward, angle);
        }
    }
}
