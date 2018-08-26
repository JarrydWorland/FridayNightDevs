using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidBehavior : MonoBehaviour {

    public float speed = 30f;
    public bool clockwise = true;
    public bool rotating = true;
    public GameObject hookAttatch;
    public GameObject player;
    
    private PlayerController playerC;
    

	// Use this for initialization
	void Start () {
        hookAttatch.SetActive(false);
        playerC = player.GetComponent<PlayerController>();

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

        if(hookAttatch.activeInHierarchy)
        {
            if(playerC.state != "attatch")
            {
                playerC.state = "attatch";

                playerC.hookAttatch = hookAttatch;
            }
            
            
        }
    }
}
