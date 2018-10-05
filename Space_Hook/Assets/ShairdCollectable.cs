using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShairdCollectable : MonoBehaviour
{
    public float rotateSpeed = 10;
    public float distance = 6;
    private PlayerController player;

    private bool orbitPlayer = false;

    private Vector2 startingpoint;

    private float time = 0;
    // Use this for initialization
    void Start()
    {
        startingpoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (orbitPlayer)
        {
            OrbitPlayer(rotateSpeed);
        }
    }

    private void OrbitPlayer(float degrees)
    {
        DistanceJoint2D asdas = new DistanceJoint2D();
    }


    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<PlayerController>() != null)
        {
            player = collider.GetComponent<PlayerController>();
            orbitPlayer = true;
        }
    }

    void OnReset()
    {
        orbitPlayer = false;
        transform.position = startingpoint;
    }
}
