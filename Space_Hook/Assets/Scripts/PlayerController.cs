using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Ray ray;
    private RaycastHit2D hit;
    public Rigidbody2D rb2d;
    public float distanceOfHook = 0.9f;
    private int rotationSpeed = 100;
    private float movementSpeed = 1f;
    public GameObject forcfield;
    public GameObject attatchedTo;
    public bool attatched = false;
    private float distanceOfReel;
    private Vector2 direction;
    private Vector2 shootDirection;

    // Use this for initialization
    void Start()
    {
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

    void Attatched()
    {
        if (Input.GetButtonDown("Fire1")) //unhooks
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (!attatchedTo.GetComponent<AsteroidBehavior>().myCol.bounds.Contains(mousePos))
            {
                forcfield.SetActive(false);
                attatchedTo.GetComponent<AsteroidBehavior>().imAttatched = false;
                attatchedTo = null;
            }
        }
    }
    // Update is called once per frame

    void FreeFall()
    {

    }
    void Update()
    {

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (attatched)
        {
            Attatched();
        }
        else
        {
            FreeFall();
        }
    }

    public void Reset()
    {
        // When the player die reset things the player needs.
        attatched = false;
        forcfield.SetActive(false);
        attatchedTo.GetComponent<AsteroidBehavior>().imAttatched = false;
        attatchedTo = null;
        rb2d.velocity = Vector2.zero;
    }
}
