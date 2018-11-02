using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    public Rigidbody2D rb2d;
    public float speed;
    public List<GameObject> collectables;
    //Below is public for testing in Unity, otherwise would be private
    public GameObject attatchedTo;
    public bool attatched = false;
    public float playerVelocity;

    public enum Directions { Up, Down, Left, Right, UpLeft, UpRight, DownLeft, DownRight };
    public Directions startDirection;

    private GameObject forcefield;
    private SoundManager sMan;
    private Animator playerAnimator;

    private bool playerMoving = false;

    void Start()
    {
        forcefield = LevelManger.Instance.ForceField;
        sMan = SoundManager.Instance;
        playerAnimator = GetComponentInChildren<Animator>();
    }

    public void StartPlayerMovement()
    {
        playerMoving = true;

        Vector3 startD = new Vector3(0, 0, 0);
        switch (startDirection)
        {
            case Directions.Up:
                startD = new Vector3(0, 1, 0);
                break;
            case Directions.Down:
                startD = new Vector3(0, -1, 0);
                break;
            case Directions.Left:
                startD = new Vector3(-1, 0, 0);
                break;
            case Directions.Right:
                startD = new Vector3(1, 0, 0);
                break;
            case Directions.UpLeft:
                startD = new Vector3(-1, 1, 0);
                break;
            case Directions.UpRight:
                startD = new Vector3(1, 1, 0);
                break;
            case Directions.DownLeft:
                startD = new Vector3(-1, -1, 0);
                break;
            case Directions.DownRight:
                startD = new Vector3(1, -1, 0);
                break;
        }
        GetComponent<Rigidbody2D>().AddForce(startD * 5f);
    }

    public void Detatch()
    {
        float currentspeed = rb2d.velocity.magnitude;
        forcefield.SetActive(false);
        forcefield.GetComponent<ForcefieldPull>().checkRot = true;
        attatchedTo.GetComponent<AsteroidBehavior>().imAttatched = false;
        attatchedTo = null;
        attatched = false;
        sMan.PlaySound(sMan.SpeedUp);
        sMan.StopSound(sMan.SecondsoundToPlay);
    }
    void Attatched()
    {
        if (Input.GetButtonDown("Fire1")) //dettaches
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (!attatchedTo.GetComponent<Collider2D>().bounds.Contains(mousePos))
            {
                Detatch();
            }
        }
    }

    void FreeFall()
    {
        this.GetComponent<ConstantSpeed>().Speed -= 0.03f;
        forcefield.GetComponent<ForcefieldPull>().howClose = 0;

    }
    void Update()
    {
        if (attatched)
        {
            Attatched();
        }
        else
        {
            FreeFall();
        }
        playerAnimator.SetBool("Attached",attatched);
    }
    private void FixedUpdate()
    {
        if (!attatched)
        {
            playerVelocity = rb2d.velocity.magnitude;
        }
        sMan.ChangePitch((this.GetComponent<ConstantSpeed>().Speed / 10) - 0.5f);
    }

    public void Reset()
    {
        // When the player die reset things the player needs.
        
        GetComponent<ConstantSpeed>().Speed = GetComponent<ConstantSpeed>().StartSpeed;
        if (attatchedTo != null)
            Detatch();
        attatchedTo = null;
        rb2d.velocity = Vector2.zero;
    }
    
    public Vector2 Rotate(Vector2 v, float degrees)
    {
        float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
        float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

        float tx = v.x;
        float ty = v.y;
        v.x = (cos * tx) - (sin * ty);
        v.y = (sin * tx) + (cos * ty);
        return v;
    }

    private void OnMouseDown()
    {
        if (!playerMoving)
        {
            StartPlayerMovement();
        }
    }

}
