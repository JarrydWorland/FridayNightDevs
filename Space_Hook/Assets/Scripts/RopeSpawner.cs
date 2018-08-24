using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeSpawner : MonoBehaviour
{

    public float numberOfSegments;
    [Tooltip("This only going to work for the square prototype")]
    public float sizeOfSegment;

    public float angleOfJointsBend;

    public GameObject Rope;
    public GameObject TESTRope;

    private SpringJoint2D HookJoint;
    private GameObject ropePart1, ropePart2;

    void Awake ()
    {
        HookJoint = TESTRope.AddComponent<SpringJoint2D>();
    }

    void Start()
    {
        ropePart1 = Instantiate(Rope, this.transform);
        HookJoint.connectedBody = ropePart1.GetComponent<Rigidbody2D>();
    }

    void Update ()
    {
        ropePart2 = Instantiate(Rope, this.transform);
        ropePart1.GetComponent<SpringJoint2D>().connectedBody = ropePart2.GetComponent<Rigidbody2D>();
        ropePart1 = ropePart2;
        ropePart2 = null;
    }
}
