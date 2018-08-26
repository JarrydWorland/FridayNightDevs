using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public GameObject RopeSegmentPrefab;
    public float ReelinSpeed =1;
    public bool RopeAttachedToPlayer = false;
    public float jointMass;
    public float jointSize;
    private Stack<GameObject> RopeSegments;
    private CircleCollider2D PlayerCircle;
    private GameObject FrontRopeSegment;
    private GameObject currentAttatchment;
	// Use this for initialization
	void Start ()
	{
	    PlayerCircle = GetComponentInParent<CircleCollider2D>();
	    RopeSegments = new Stack<GameObject>();

    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

     public void Unreal(GameObject hook)
    {
        currentAttatchment = hook;
        if (RopeSegments.Count <= 0)
        {
            FrontRopeSegment = Instantiate(RopeSegmentPrefab,transform);
            FrontRopeSegment.transform.localScale = new Vector3(jointSize, jointSize, 1);
            RopeSegments.Push(FrontRopeSegment);
            HingeJoint2D hinge = FrontRopeSegment.AddComponent<HingeJoint2D>();
            hinge.connectedBody = hook.GetComponent<Rigidbody2D>();
            hinge.enableCollision = true;
            FrontRopeSegment.GetComponent<Rigidbody2D>().mass = jointMass;
        }
        if ((RopeSegments.Peek().transform.position - PlayerCircle.transform.position).magnitude > RopeSegments.Peek().GetComponent<CircleCollider2D>().radius * jointSize)
        {
            GameObject NewSegment = Instantiate(RopeSegmentPrefab, transform);

            NewSegment.transform.localScale = new Vector3(jointSize, jointSize, 1);
            HingeJoint2D hinge = NewSegment.AddComponent<HingeJoint2D>();
            hinge.connectedBody = RopeSegments.Peek().GetComponent<Rigidbody2D>();
            hinge.enableCollision = true;
            NewSegment.GetComponent<Rigidbody2D>().mass = jointMass;
            RopeSegments.Push(NewSegment);
            
            //PlayerCircle.GetComponent<HingeJoint2D>().connectedBody = RopeSegments.Peek().GetComponent<Rigidbody2D>();
        }
        // Checks if end Rope Segment is pass the egde of the player
        // if so adds a extra segment.

        
  
    }

    public void SwitchAttach(GameObject attatchment)
    {
        currentAttatchment = attatchment;
        FrontRopeSegment.GetComponent<HingeJoint2D>().connectedBody = attatchment.GetComponent<Rigidbody2D>();
    }

    public void Reelin()
    {
        if ((RopeSegments.Peek().transform.position - PlayerCircle.transform.position).magnitude + RopeSegments.Peek().GetComponent<CircleCollider2D>().radius 
            < PlayerCircle.radius)
        {
            Destroy(RopeSegments.Pop());
            PlayerCircle.GetComponent<HingeJoint2D>().connectedBody = RopeSegments.Peek().GetComponent<Rigidbody2D>();
        }
        Vector2 force = PlayerCircle.transform.position - RopeSegments.Peek().transform.position;
        RopeSegments.Peek().GetComponent<Rigidbody2D>().AddForce(force.normalized*ReelinSpeed);


    }

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
            PlayerCircle.GetComponent<HingeJoint2D>().connectedBody = RopeSegments.Peek().GetComponent<Rigidbody2D>();
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
