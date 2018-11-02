using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantSpeed : MonoBehaviour {

    public float Speed;
    private float startSpeed;
    public float min;
    public float max;
    private Rigidbody2D rigidbody2D;
    // Use this for initialization
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        startSpeed = Speed;
    }

    public float StartSpeed
    {
        get { return startSpeed; }
    }
    // Update is called once per frame
    void Update()
    {
        if(Speed <min)
        {
            Speed = min;
        }
        if (rigidbody2D.gameObject.GetComponent<PlayerController>().attatched)
        {
            if (Speed > max - (max / 3))
            {
                Speed = max - (max / 3);
            }
        }
        if (Speed > max)
        {
            Speed = max;
        }
      
        Vector3 normalizedVel = rigidbody2D.velocity.normalized;
        rigidbody2D.velocity = new Vector2(normalizedVel.x * Speed, normalizedVel.y * Speed);

    }
}
