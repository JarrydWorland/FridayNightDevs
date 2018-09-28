using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForcefieldPull : MonoBehaviour {

    
    public GameObject player;
    public float pullForce;

    private Vector2 betweenFP;
    private Vector2 pullDirection;
    private PlayerController playerC;

    //variables to play with
    public float startCurve = 6, endCurve = 3;
    public float howClose;
    public float degreeChange;
    public float distBW;
    // Use this for initialization
    void Start () {
        playerC = player.GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
        betweenFP = ((Vector2)transform.position - (Vector2)player.transform.position);
        pullDirection = betweenFP.normalized;
        distBW = Mathf.Abs(betweenFP.magnitude);
        Pull();
	}

    void Pull()
    {
        
        if (distBW > startCurve)
        {
            playerC.rb2d.AddForce(pullForce * pullDirection);
        }
        else if(distBW <= endCurve)
        {
            playerC.rb2d.velocity = Vector2.zero;
            playerC.rb2d.AddForce(pullForce * playerC.Rotate(pullDirection, 90));
        }
        else
        {
            howClose = 1-((distBW-endCurve)/(startCurve-endCurve)); //percentage in decimal of how far between the start and end point you are
            degreeChange = 90*howClose;
            Vector2 newPullDirection = playerC.Rotate(pullDirection, degreeChange);
            playerC.rb2d.velocity = Vector2.zero;
            playerC.rb2d.AddForce(pullForce * newPullDirection);
        }
    }
}
