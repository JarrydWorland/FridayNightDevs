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
    public float reel;
    public float scroll = 0f;

    private Vector2 direction;
    private Rope rope;
    private Vector2 shootDirection;

    // Use this for initialization
    void Start ()
    {
        rope = GetComponentInChildren<Rope>();
    }
	
	// Update is called once per frame
	void Update () {
        if (manualMovement)
        {
            MovePlayer();
            changeInPlayerPos = transform.position;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            if (state == "attatch")//unhooks
            {
                hookAim.SetActive(true);
                hookShot.SetActive(false);
                hookAttatch.SetActive(false);
                rope.DestoryRope();

            }
            else if (state == "aim") //shoots
            {
                hookAim.SetActive(false);
                hookShot.GetComponent<HookShoot>().ResetPosition();
                hookShot.SetActive(true);
                state = "shoot";
                shootDirection = new Vector2(hookShot.transform.position.x - transform.position.x, hookShot.transform.position.y - transform.position.y).normalized;
                hookShot.GetComponent<HookShoot>().hookrb.AddForce(shootDirection * thrust);
                hookAttatch.SetActive(false);
            }
            else if (state == "shoot")//resets shot
            {
                hookAim.SetActive(true);
                hookShot.SetActive(false);
                hookAttatch.SetActive(false);
                rope.DestoryRope();

            }

        }


        if (state == "attatch")
        {
            Vector2 pullDirection = new Vector2(hookAttatch.transform.position.x - transform.position.x, hookAttatch.transform.position.y - transform.position.y).normalized;
                        
            scroll = Input.GetAxis("Mouse ScrollWheel");
            transform.position += (Vector3)pullDirection * scroll * reel;

            //rb2d.AddForce(pullDirection * reel);
            if (Input.GetButtonDown("Fire2"))
            {
                rope.Reelin();
            }
            rope.SwitchAttach(hookAttatch);
            rope.AttachPlayer();
            rope.ChangeHook(hookAttatch);

           // transform.RotateAround(/*transformaround*/, transform.forward, angle);
            /* rb2d.AddForce(pullDirection * reel);*/ // this is magnetic pull in direction of hook.
        }
        else if (state == "shoot")
	    {
	            rope.Unreal(hookShot);
	    }
        else if (state == "aim")
	    {

	    }

	    initialPlayerPos = transform.position;

        

       
       
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
