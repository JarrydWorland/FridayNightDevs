using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public float speed;
    public GameObject forcfield;
    
    

    //Below is public for testing in Unity, otherwise would be private
    public GameObject attatchedTo;
    public bool attatched = false;
    public float playerVelocity;
    
    void Start()
    {
        rb2d.AddForce(Vector2.right * 5f);
    }
    void Attatched()
    {
        if (Input.GetButtonDown("Fire1")) //unhooks
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (!attatchedTo.GetComponent<AsteroidBehavior>().myCol.bounds.Contains(mousePos))
            {
                forcfield.SetActive(false);
                forcfield.GetComponent<ForcefieldPull>().checkRot = true;
                attatchedTo.GetComponent<AsteroidBehavior>().imAttatched = false;
                attatchedTo = null;
                attatched = false;
            }
        }
    }

    void FreeFall()
    {

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
    }
    private void FixedUpdate()
    {
        playerVelocity = rb2d.velocity.magnitude;
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
    
}
