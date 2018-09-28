﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForcefieldPull : MonoBehaviour {

    
    public GameObject player;
    

    private Vector2 betweenFP;
    private Vector2 pullDirection;
    private PlayerController playerC;
    private float pullForce;

    public bool clockwise = true;
    public bool checkRot = true;
    //variables to play with
    public float startCurve = 6, endCurve = 3;
    public float howClose;
    public float degreeChange;
    public float distBW;
    public float angle;
    // Use this for initialization
    void Start()
    {
        playerC = player.GetComponent<PlayerController>();
    }
	
    void CheckRotation()
    {
        if (player.transform.position.x <= transform.position.x)//left - mid
        {
            if (player.transform.position.y >= transform.position.y) //top-mid
            {
                if (angle >= 0 && angle <= 180)
                {
                    clockwise = false;
                }
                else clockwise = true;
            }
            else//bot
            {
                if (angle >= 0 && angle <= 180)
                {
                    clockwise = false;
                }
                else clockwise = true;
            }
        }
        else//right
        {
            if (player.transform.position.y >= transform.position.y) //top-mid
            {
                if (angle >= 0 && angle <= 180)
                {
                    clockwise = false;
                }
                else clockwise = true;
            }
            else//bot
            {
                if (angle >= 0 && angle <= 180)
                {
                    clockwise = false;
                }
                else clockwise = true;
            }
        }
    }
	// Update is called once per frame
	void Update () {

        pullForce = player.GetComponent<ConstantSpeed>().Speed * 5;
        betweenFP = ((Vector2)transform.position - (Vector2)player.transform.position);
        pullDirection = betweenFP.normalized;
        distBW = Mathf.Abs(betweenFP.magnitude);

        Vector2 currentD = playerC.rb2d.velocity.normalized;
        angle = Vector2.SignedAngle(currentD, pullDirection);

        if (checkRot)
        {
            CheckRotation();
            checkRot = false;
        }
       

    Pull();
	}

    void Pull()
    {
        
        if (distBW > startCurve)
        {
            playerC.rb2d.velocity = Vector2.zero;
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
            if(!clockwise)
            { degreeChange *= -1; }
            Vector2 newPullDirection = playerC.Rotate(pullDirection, degreeChange);
            playerC.rb2d.velocity = Vector2.zero;
            playerC.rb2d.AddForce(pullForce * newPullDirection);
        }
    }
}
