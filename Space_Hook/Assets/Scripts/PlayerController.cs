using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    
    public Rigidbody2D rb2d;
    public float distanceOfHook = 0.9f;
    private int rotationSpeed = 100;
    private float movementSpeed = 1f;
    public GameObject hookAim;
    public GameObject hookShot;
    public GameObject forcfield;
    public GameObject hookAttatch;
    public GameObject attatchedTo;
    private Vector3 changeInPlayerPos;
    private Vector3 initialPlayerPos;
    public string state = "";
    public float thrust;
    public float reel;
    public float scroll = 0f;
    public float speed;
    public float xVel, yVel;
    private float distanceOfReel;
    private Vector2 direction;
    private Vector2 shootDirection;

    // Use this for initialization
    void Start ()
    {
    }

   public Vector2 Rotate( Vector2 v, float degrees)
    {
        float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
        float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

        float tx = v.x;
        float ty = v.y;
        v.x = (cos * tx) - (sin * ty);
        v.y = (sin * tx) + (cos * ty);
        return v;
    }

    void Attatched()
    {
        distanceOfReel = attatchedTo.GetComponent<AsteroidBehavior>().maxDistance;
        Vector2 reelDirection = new Vector2(transform.position.x - attatchedTo.transform.position.x, transform.position.y - attatchedTo.transform.position.y).normalized;
        Vector2 forceDirection = Rotate(reelDirection, 90f);

        if (Input.GetKey("a") || Input.GetKey("d"))
        {
            if (Input.GetKey("a"))
            {
                rb2d.AddForce(forceDirection);
            }
            else
            {
                rb2d.AddForce(-forceDirection);
            }
           

            //transform.RotateAround(attatchedTo.transform.position, transform.forward, 1);
        }
        if (Input.GetKey("w") || Input.GetKey("s"))
        {

            if (Input.GetKey("w"))
            {
                // transform.position += (Vector3)reelDirection / 50;
                if(attatchedTo.GetComponent<AsteroidBehavior>().maxDistance > 2.3f)
                attatchedTo.GetComponent<AsteroidBehavior>().maxDistance -= (reelDirection / 10).magnitude;
            }
            else
            {
                

                attatchedTo.GetComponent<AsteroidBehavior>().maxDistance += (reelDirection / 10).magnitude;
                // transform.position -= (Vector3)reelDirection  /50;
            }
        }
        if (Input.GetButtonDown("Fire1")) //unhooks
        {
            hookAim.SetActive(true);
            hookShot.SetActive(false);
            forcfield.SetActive(false);
            attatchedTo.GetComponent<AsteroidBehavior>().imAttatched = false;
            attatchedTo = null;
        }
    }
    void ShootMode()
    {
        if (Input.GetButtonDown("Fire1"))//cancels shot
        {
            hookAim.SetActive(true);
            hookShot.SetActive(false);
            hookAttatch.SetActive(false);
        }
    }
    void AimMode()
    {
        if (Input.GetButtonDown("Fire1"))//shoots
        {
            hookAim.SetActive(false);
            hookShot.GetComponent<HookShoot>().ResetPosition();
            hookShot.SetActive(true);
            state = "shoot";
            shootDirection = new Vector2(hookShot.transform.position.x - transform.position.x, hookShot.transform.position.y - transform.position.y).normalized;
            hookShot.GetComponent<HookShoot>().hookrb.AddForce(shootDirection * thrust);
            hookAttatch.SetActive(false);
        }
    }
	// Update is called once per frame
	void Update () {
        
        if (state == "attatch")
        {
            Attatched();
        }
        else if (state == "shoot")
	    {
            ShootMode();

        }
        if (state == "aim")
	    {

            AimMode();
        }
	    initialPlayerPos = transform.position;
    }

    private void FixedUpdate()
    {
        speed = rb2d.velocity.magnitude;
        xVel = rb2d.velocity.x;
        yVel = rb2d.velocity.y;
    }
}
