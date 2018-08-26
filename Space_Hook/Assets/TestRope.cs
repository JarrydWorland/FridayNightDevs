using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRope : MonoBehaviour
{

    internal Rigidbody2D RBody;
	// Use this for initialization
	void Start ()
	{
	    RBody = GetComponent<Rigidbody2D>();

        int childcount = transform.childCount;

	    for (int i = 0; i < childcount; i++)
	    {
	        Transform t = transform.GetChild(i);
	        HingeJoint2D hinge =  t.gameObject.AddComponent<HingeJoint2D>();
	        Rigidbody2D rb = t.GetComponent<Rigidbody2D>();
	        rb.mass = 0.5f;
	        hinge.connectedBody = 
                i == 0 ? RBody : transform.GetChild(i - 1).GetComponent<Rigidbody2D>();
	        hinge.enableCollision = true;
	    }

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
