using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    
    public Rigidbody2D rb2d;
    public float distanceOfHook = 0.9f;
    private int rotationSpeed = 100;
    private float movementSpeed = 1f;
    public bool manualMovement = false;
    public GameObject hookAim;
    public GameObject hookShot;
    public GameObject hookAttatch;
    private Vector3 changeInPlayerPos;
    private Vector3 initialPlayerPos;
    public string state = "";
    public float thrust;

    private Vector2 direction;
    private Rope rope;


    // Use this for initialization
    void Start ()
    {
        rope = GetComponentInChildren<Rope>();
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButtonUp("Fire1"))
        {
            if (state == "attatch")
            {
                hookAim.SetActive(true);
                hookShot.SetActive(false);
                hookAttatch.SetActive(false);
            }
            else if (state == "aim")
            {
                hookAim.SetActive(false);
                hookShot.GetComponent<HookShoot>().ResetPosition();
                hookShot.SetActive(true);
                direction = new Vector2(hookShot.transform.position.x - transform.position.x, hookShot.transform.position.y - transform.position.y).normalized;
                hookShot.GetComponent<HookShoot>().hookrb.AddForce(direction * thrust);
                hookAttatch.SetActive(false);
            }
            else if (state == "shoot")
            {
                hookAim.SetActive(true);
                hookShot.SetActive(false);
                hookAttatch.SetActive(false);
            }

        }

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
