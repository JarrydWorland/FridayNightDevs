using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidBehavior : MonoBehaviour
{

    public float speed = 30f;
    public bool clockwise = true;
    public bool rotating = true;
    public GameObject attatchedTo;
    public GameObject forcfield;
    public GameObject hookAttatch;
    public GameObject player;
    public float maxDistance;
    public bool imAttatched;

    private PlayerController playerC;

    public float minScale, maxScale;

    // Use this for initialization
    void Start()
    {

        //Sets scale of object 
        SetTransform();

        transform.parent = GameObject.FindGameObjectWithTag("Asteroids").transform;
        forcfield = GameObject.FindGameObjectWithTag("Forcefield");
        hookAttatch = GameObject.FindGameObjectWithTag("HookAttach");
        player = GameObject.FindGameObjectWithTag("Player");

        // forcfield.SetActive(false);
        imAttatched = false;
        playerC = player.GetComponent<PlayerController>();
        maxDistance = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (imAttatched == false)
        {
            maxDistance = 0;
        }
        if (rotating)
        {
            if (!clockwise)
            {
                transform.Rotate(Vector3.forward * Time.deltaTime * speed);
            }
            else
            {
                transform.Rotate(Vector3.forward * Time.deltaTime * -speed);

            }
        }

        if (imAttatched)
        {
            if (playerC.state != "attatch")
            {
                playerC.state = "attatch";
                playerC.attatchedTo = attatchedTo;
                if (maxDistance == 0)
                {
                    maxDistance = (playerC.transform.position - forcfield.transform.position).magnitude;
                }

            }
            if (maxDistance < 2.3)
            {
                maxDistance = 2.3f;
            }

            forcfield.GetComponent<DistanceJoint2D>().distance = maxDistance;

            Vector2 hookDirection = new Vector2(playerC.transform.position.x - forcfield.transform.position.x, playerC.transform.position.y - forcfield.transform.position.y).normalized;
            Vector2 currentHookDirection = new Vector2(hookAttatch.transform.position.x - forcfield.transform.position.x, hookAttatch.transform.position.y - forcfield.transform.position.y);
            float angle = Vector2.SignedAngle(currentHookDirection, hookDirection);

            hookAttatch.transform.RotateAround(forcfield.transform.position, transform.forward, angle);
        }

    }

    public void SetTransform()
    {
        float objectScale = Random.Range(minScale, maxScale);
        transform.localScale = new Vector3(objectScale, objectScale, 0);

        transform.localRotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360)));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "attatch")
        {
            float spawnRnge = GetComponentInParent<SpawnLevel>().spawnRange;
            transform.position = new Vector3(Random.Range(-spawnRnge, spawnRnge), Random.Range(-spawnRnge, spawnRnge), 0);
        }
    }
}
