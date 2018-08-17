using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookShoot : MonoBehaviour {

    public GameObject hookAim;
    public GameObject attachment;
    public float angle;
    private Vector2 direction;
    public GameObject body;
    public Rigidbody2D hookrb;
    public float thrust;
    

    // Use this for initialization
    void Start () {

        transform.position = hookAim.transform.position;
        transform.rotation = hookAim.transform.rotation;
        direction = new Vector2(transform.position.x - body.transform.position.x, transform.position.y - body.transform.position.y).normalized;
        hookAim.SetActive(false);
        hookrb.AddForce(direction * thrust);
    }
	
	// Update is called once per frame
	void Update () {
        if(!hookrb)
        {
            transform.RotateAround(attachment.transform.position, transform.forward, -angle);
        }
      /*  if(hookrb.constraints == RigidbodyConstraints2D.FreezeAll)
        {
            transform.RotateAround(attachment.transform.position, transform.forward, angle);
        }*/
		
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag.Equals("attatch") == true)
        {
            attachment = col.gameObject;
            Destroy(hookrb);//destroys rigibody

            //hookrb.constraints = RigidbodyConstraints2D.FreezeAll; //stops all movement
        }
    }
}
