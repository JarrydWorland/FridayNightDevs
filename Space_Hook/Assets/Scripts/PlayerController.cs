using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Vector3 hookDirection;
    private int rotationSpeed = 100;
    private float movementSpeed = 0.1f;
    public bool manualMovement = false;

    public GameObject hook;
    public GameObject body;

	// Use this for initialization
	void Start () {
        hook.transform.position = body.transform.position + new Vector3(0,0.9f,0);
    }
	
	// Update is called once per frame
	void Update () {

        if(Input.GetKey("left"))
        {
            hook.transform.RotateAround(body.transform.position, Vector3.forward, rotationSpeed * Time.deltaTime);
        }
        if(Input.GetKey("right"))
        {
            hook.transform.RotateAround(body.transform.position, Vector3.back, rotationSpeed * Time.deltaTime);
        }

        if (manualMovement)
        {
            if (Input.GetKey("w"))
            {
                transform.position += Vector3.up * movementSpeed;
            }
            if (Input.GetKey("s"))
            {
                transform.position += Vector3.down * movementSpeed;
            }
            if (Input.GetKey("a"))
            {
                transform.position += Vector3.left * movementSpeed;
            }
            if (Input.GetKey("d"))
            {
                transform.position += Vector3.right * movementSpeed;
            }
        }

    }
}
