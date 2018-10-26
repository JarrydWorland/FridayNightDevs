using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForcefieldPull : MonoBehaviour {

    private Vector2 betweenFP;
    private Vector2 pullDirection;
    private PlayerController playerC;
    private float pullForce;
    public GameObject visRep;
    private SpriteRenderer sR;
    private bool increase = false;
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
        sR = visRep.GetComponent<SpriteRenderer>();
        playerC = LevelManger.Instance.player;
    }
	
    void CheckRotation()
    {
        if (playerC.transform.position.x <= transform.position.x)//left - mid
        {
            if (playerC.transform.position.y >= transform.position.y) //top-mid
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
            if (playerC.transform.position.y >= transform.position.y) //top-mid
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
	void Update () {

        pullForce = playerC.GetComponent<ConstantSpeed>().Speed * 5;
        betweenFP = ((Vector2)transform.position - (Vector2)playerC.transform.position);
        pullDirection = betweenFP.normalized;
        distBW = Mathf.Abs(betweenFP.magnitude);

        Vector2 currentD = playerC.rb2d.velocity.normalized;
        angle = Vector2.SignedAngle(currentD, pullDirection);

        if (checkRot)
        {
            CheckRotation();
            checkRot = false;
        }
        

        
	}

    private void LateUpdate()
    {
        if (playerC.attatched)
        {
            transform.position = playerC.attatchedTo.transform.position;
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

        if(playerC.attatchedTo.GetComponent<Rigidbody2D>().velocity.magnitude != 0)
        {
            playerC.transform.position += (Vector3)playerC.attatchedTo.GetComponent<Rigidbody2D>().velocity * Time.deltaTime;
        }
    }

}
