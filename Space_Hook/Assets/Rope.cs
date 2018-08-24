using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public GameObject RopeSegmentPrefab;

    private Stack<GameObject> RopeSegments;
    private GameObject Player;
	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void Unreal(GameObject hook)
    {
        if (RopeSegments.Count <= 0)
        {
            
            GameObject firstSegment = Instantiate(RopeSegmentPrefab,transform);
            RopeSegments.Push(firstSegment);
            HingeJoint2D hinge = firstSegment.AddComponent<HingeJoint2D>();
            hinge.connectedBody = hook.GetComponent<Rigidbody2D>();
        }
        if ((RopeSegments.Peek().transform.position - Player.transform.position).magnitude >
            RopeSegments.Peek().GetComponent<CircleCollider2D>().radius)
        {
            GameObject NewSegment = Instantiate(RopeSegmentPrefab, transform);
            HingeJoint2D hinge = NewSegment.AddComponent<HingeJoint2D>();
            hinge.connectedBody = RopeSegments.Peek().GetComponent<Rigidbody2D>();
            RopeSegments.Push(NewSegment);
        }
        // Checks if end Rope Segment is pass the egde of the player
        // if so adds a extra segment.

  
    }

    void Realin()
    {
        if ((RopeSegments.Peek().transform.position - Player.transform.position).magnitude + RopeSegments.Peek().GetComponent<CircleCollider2D>().radius 
            < RopeSegments.Peek().GetComponent<CircleCollider2D>().radius)
        {
            Destroy(RopeSegments.Pop());
        }

    }


}
