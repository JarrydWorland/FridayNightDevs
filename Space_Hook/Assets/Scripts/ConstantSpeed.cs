using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantSpeed : MonoBehaviour {

    public float Speed;

    private Rigidbody2D rigidbody2D;
    // Use this for initialization
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 normalizedVel = rigidbody2D.velocity.normalized;
        rigidbody2D.velocity = new Vector2(normalizedVel.x * Speed, normalizedVel.y * Speed);

    }
}
