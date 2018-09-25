using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookShoot : MonoBehaviour {

    public GameObject hookAim;
    public GameObject attachment;
    public GameObject playerChar;
    private PlayerController playerCont;
    public float angle;
    public Rigidbody2D hookrb;
    public GameObject ropeLink;
    public float ropeD;
    public float hookDistance = 0;
    public Vector2 initialPos;
    public Vector2 nextPos;



    // Use this for initialization
    void Start () {
        playerCont = playerChar.GetComponent<PlayerController>();
        ropeD = ropeLink.GetComponent<CircleCollider2D>().radius * 2;
    }
	
	// Update is called once per frame
	void Update () {

        playerCont.state = "shoot";
        nextPos = transform.position;

        hookDistance = Vector2.Distance(initialPos, nextPos);


        /*  if(hookrb.constraints == RigidbodyConstraints2D.FreezeAll)
          {
              transform.RotateAround(attachment.transform.position, transform.forward, angle);
          }*/

    }
    
    public void ResetPosition()
    {
        transform.position = hookAim.transform.position;
        transform.rotation = hookAim.transform.rotation;
        initialPos = transform.position;
        nextPos = transform.position;
        hookDistance = 0;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag.Equals("attatch") == true)
        {
            float v = playerCont.rb2d.velocity.magnitude;
            attachment = col.gameObject;
            AsteroidBehavior asteroid = col.gameObject.GetComponent<AsteroidBehavior>();
            asteroid.imAttatched = true;
            asteroid.forcfield.SetActive(true);
            asteroid.hookAttatch.SetActive(true);
            asteroid.forcfield.transform.position = asteroid.transform.position;
            float distanceOfReel = asteroid.maxDistance;
            Vector2 reelDirection = new Vector2(transform.position.x - attachment.transform.position.x, transform.position.y - attachment.transform.position.y).normalized;
            Vector2 forceDirection = playerCont.Rotate(reelDirection, 90f);



            if (playerCont.rb2d.velocity.x >= playerCont.rb2d.velocity.y)
            {
                if ((playerCont.rb2d.velocity.x > 0 && playerCont.transform.position.y <= asteroid.forcfield.transform.position.y) 
                    || (playerCont.rb2d.velocity.x < 0 && playerCont.transform.position.y > asteroid.forcfield.transform.position.y))//if previous x movement is + and position is <= forcf y position, then counterclockwise
                {
                    playerCont.rb2d.velocity = Vector3.zero;
                    playerCont.rb2d.AddForce(forceDirection * 5 * v);
                }
                else//clockwise
                {
                    playerCont.rb2d.velocity = Vector3.zero;
                    playerCont.rb2d.AddForce(forceDirection * -5 * v);
                }
            }
            else
            {
                if ((playerCont.rb2d.velocity.y > 0 && playerCont.transform.position.x <= asteroid.forcfield.transform.position.x) || (playerCont.rb2d.velocity.y < 0 && playerCont.transform.position.x > asteroid.forcfield.transform.position.x))//if previous x movement is + and position is <= forcf y position, then counterclockwise
                {
                    playerCont.rb2d.velocity = Vector3.zero;
                    playerCont.rb2d.AddForce(forceDirection * -5 * v);
                }
                else//counterclockwise
                {
                    playerCont.rb2d.velocity = Vector3.zero;
                    playerCont.rb2d.AddForce(forceDirection * 5 * v);
                }
            }

            Vector2 landedHere = new Vector2(transform.position.x - asteroid.forcfield.transform.position.x, transform.position.y - asteroid.forcfield.transform.position.y).normalized;
            Vector2 currentlyHere = new Vector2(asteroid.hookAttatch.transform.position.x - asteroid.forcfield.transform.position.x, asteroid.hookAttatch.transform.position.y - asteroid.forcfield.transform.position.y).normalized;
            angle = Vector2.SignedAngle(currentlyHere, landedHere);

            asteroid.forcfield.transform.Rotate(transform.forward, angle);
             this.gameObject.SetActive(false);

            //hookrb.constraints = RigidbodyConstraints2D.FreezeAll; //stops all movement
        }
    }
}
