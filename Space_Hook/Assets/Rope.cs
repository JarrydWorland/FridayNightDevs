using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public GameObject RopeSegmentPrefab;
    public float ReelinSpeed =1;
    public bool RopeAttachedToPlayer = false;

    private Stack<GameObject> RopeSegments;
    private CircleCollider2D PlayerCircle;
    private GameObject FrontRopeSegment;
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
        if (RopeSegments.Count <= 0)
        {
            FrontRopeSegment = Instantiate(RopeSegmentPrefab,transform);
            RopeSegments.Push(FrontRopeSegment);
            HingeJoint2D hinge = FrontRopeSegment.AddComponent<HingeJoint2D>();
            hinge.connectedBody = hook.GetComponent<Rigidbody2D>();
            hinge.enableCollision = true;
        }
        if ((RopeSegments.Peek().transform.position - PlayerCircle.transform.position).magnitude > RopeSegments.Peek().GetComponent<CircleCollider2D>().radius)
        {
            GameObject NewSegment = Instantiate(RopeSegmentPrefab, transform);
            HingeJoint2D hinge = NewSegment.AddComponent<HingeJoint2D>();
            hinge.connectedBody = RopeSegments.Peek().GetComponent<Rigidbody2D>();
            hinge.enableCollision = true;

            RopeSegments.Push(NewSegment);
            //PlayerCircle.GetComponent<HingeJoint2D>().connectedBody = RopeSegments.Peek().GetComponent<Rigidbody2D>();
        }
        // Checks if end Rope Segment is pass the egde of the player
        // if so adds a extra segment.

  
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
            RopeAttachedToPlayer = true;
        }
    }

    public void ChangeHook(GameObject newHook)
    {
        FrontRopeSegment.GetComponent<HingeJoint2D>().connectedBody = newHook.GetComponent<Rigidbody2D>();
    }
}
