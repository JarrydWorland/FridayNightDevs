using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    
    public Rigidbody2D rb2d;
    public float distanceOfHook = 0.9f;
    private int rotationSpeed = 100;
    private float movementSpeed = 1f;
    public bool manualMovement = false;
    public GameObject hook;
    private Vector3 changeInPlayerPos;
    private Vector3 initialPlayerPos;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {

        initialPlayerPos = transform.position;
        
        if (manualMovement)
        {
            MovePlayer();
            changeInPlayerPos = transform.position;
        }
       
    }

    void MovePlayer()
    {
        if (Input.GetKey("w"))
        {
            rb2d.AddForce(transform.up * movementSpeed);
        }
        if (Input.GetKey("s"))
        {
            rb2d.AddForce(transform.up * -movementSpeed);
        }
        if (Input.GetKey("a"))
        {
            rb2d.AddForce(transform.right * -movementSpeed);
        }
        if (Input.GetKey("d"))
        {
            rb2d.AddForce(transform.right * movementSpeed);
        }
    }
}
