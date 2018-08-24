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
   
    

    // Use this for initialization
    void Start () {
        playerCont = playerChar.GetComponent<PlayerController>();
    }
	
	// Update is called once per frame
	void Update () {

        playerCont.state = "shoot";

        /*  if(hookrb.constraints == RigidbodyConstraints2D.FreezeAll)
          {
              transform.RotateAround(attachment.transform.position, transform.forward, angle);
          }*/

    }

    public void ResetPosition()
    {
        transform.position = hookAim.transform.position;
        transform.rotation = hookAim.transform.rotation;
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
