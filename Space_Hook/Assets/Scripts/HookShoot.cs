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
    public Vector3 ropeLinkStartPos;
    public Quaternion ropeLinkStartRot;



    // Use this for initialization
    void Start () {
        playerCont = playerChar.GetComponent<PlayerController>();
        ropeD = ropeLink.GetComponent<CircleCollider2D>().radius * 2;
        ropeLinkStartPos = ropeLink.transform.position;
        ropeLinkStartRot = ropeLink.transform.rotation;
    }
	
	// Update is called once per frame
	void Update () {

        playerCont.state = "shoot";
        nextPos = transform.position;

        hookDistance = Vector2.Distance(initialPos, nextPos);

        if(hookDistance >= ropeD)
        {
            GenerateRope();
            hookDistance = 0;
        }

        /*  if(hookrb.constraints == RigidbodyConstraints2D.FreezeAll)
          {
              transform.RotateAround(attachment.transform.position, transform.forward, angle);
          }*/

    }

    public void GenerateRope()
    {
        Instantiate(ropeLink, ropeLinkStartPos, ropeLinkStartRot, ropeLink.transform);
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
            attachment = col.gameObject;
            AsteroidBehavior asteroid = col.gameObject.GetComponent<AsteroidBehavior>();
            asteroid.hookAttatch.SetActive(true);
            asteroid.hookAttatch.transform.position = transform.position;
            asteroid.hookAttatch.transform.rotation = transform.rotation;
            this.gameObject.SetActive(false);

            //hookrb.constraints = RigidbodyConstraints2D.FreezeAll; //stops all movement
        }
    }
}
