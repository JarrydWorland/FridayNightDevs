using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{

    public RopeSpawner RopeSpawnerScript;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	    transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0,0,10);

	    if (Input.GetKeyDown(KeyCode.T))
	    {
	        RopeSpawnerScript.enabled = true;
	    }
	    else
	    {
	        RopeSpawnerScript.enabled = false;
	    }

	}
}
