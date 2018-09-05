using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticRope : MonoBehaviour {

    public GameObject Joint;
    public PhysicsMaterial2D slippery;
    public Vector2 spawnPoint;
    //public float ReelinSpeed = 1;
    public float jointPull;
    public bool RopeAttachedToPlayer = false;
    public float jointMass;
    public float jointSizeX;
    public float jointSizeY;

    private Stack<GameObject> RopeSegments;
    public GameObject player;
    private CircleCollider2D PlayerCircle;
    private GameObject FrontRopeSegment;
    private GameObject currentAttatchment;
    // Use this for initialization
    void Start()
    {
        
        PlayerCircle = player.GetComponent<CircleCollider2D>();
        RopeSegments = new Stack<GameObject>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Unreal(GameObject hook)
    {
        currentAttatchment = hook;
        if (RopeSegments.Count <= 0)
        {
            FrontRopeSegment = Instantiate(Joint, transform);
            FrontRopeSegment.transform.localScale = new Vector3(jointSizeX, jointSizeY, 1);
            RopeSegments.Push(FrontRopeSegment);
          
            FrontRopeSegment.GetComponent<Rigidbody2D>().mass = jointMass;
            FrontRopeSegment.GetComponent<JointBehavior>().pullTowards = hook;
            FrontRopeSegment.GetComponent<JointBehavior>().pull = jointPull;
        }
        if ((RopeSegments.Peek().transform.position - PlayerCircle.transform.position).magnitude > RopeSegments.Peek().GetComponent<CircleCollider2D>().radius * jointSizeX)
        {
            GameObject NewSegment = Instantiate(Joint, transform);

            NewSegment.transform.localScale = new Vector3(jointSizeX, jointSizeY, 1);
           
            NewSegment.GetComponent<Rigidbody2D>().mass = jointMass;

            NewSegment.GetComponent<Rigidbody2D>().mass = jointMass;
            NewSegment.GetComponent<JointBehavior>().pullTowards = RopeSegments.Peek();
            NewSegment.GetComponent<JointBehavior>().pull = jointPull;

            RopeSegments.Push(NewSegment);
            //PlayerCircle.GetComponent<HingeJoint2D>().connectedBody = RopeSegments.Peek().GetComponent<Rigidbody2D>();
        }
        // Checks if end Rope Segment is pass the egde of the player
        // if so adds a extra segment.



    }

    public void SwitchAttach(GameObject attatchment)
    {
        currentAttatchment = attatchment;
        FrontRopeSegment.GetComponent<JointBehavior>().pullTowards = attatchment;
    }

   /* public void Reelin()
    {
        if ((RopeSegments.Peek().transform.position - PlayerCircle.transform.position).magnitude + RopeSegments.Peek().GetComponent<CircleCollider2D>().radius
            < PlayerCircle.radius)
        {
            Destroy(RopeSegments.Pop());
            PlayerCircle.GetComponent<HingeJoint2D>().connectedBody = RopeSegments.Peek().GetComponent<Rigidbody2D>();
        }
        Vector2 force = PlayerCircle.transform.position - RopeSegments.Peek().transform.position;
        RopeSegments.Peek().GetComponent<Rigidbody2D>().AddForce(force.normalized * ReelinSpeed);


    }*/

    public void DestoryRope()
    {
        foreach (GameObject ropeSegment in RopeSegments)
        {
            Destroy(ropeSegment);
        }
        RopeSegments.Clear();
        RopeAttachedToPlayer = false;
    }

    public void AttachPlayer()
    {
        if (!RopeAttachedToPlayer)
        {
            HingeJoint2D playerHinge = PlayerCircle.GetComponent<HingeJoint2D>();
            playerHinge.connectedBody = RopeSegments.Peek().GetComponent<Rigidbody2D>();

            //DistanceJoint2D fixJoint = currentAttatchment.AddComponent<DistanceJoint2D>();
            //fixJoint.connectedBody = PlayerCircle.GetComponent<Rigidbody2D>();
            //fixJoint.maxDistanceOnly = true;
            RopeAttachedToPlayer = true;
        }
    }

    public void ChangeHook(GameObject newHook)
    {
        FrontRopeSegment.GetComponent<HingeJoint2D>().connectedBody = newHook.GetComponent<Rigidbody2D>();
    }
}
