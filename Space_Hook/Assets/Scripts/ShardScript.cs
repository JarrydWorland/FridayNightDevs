using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShardScript : MonoBehaviour {


    public GameObject SoundManager;
    private SoundManager sMan;

    // Use this for initialization
    void Start () {
        sMan = SoundManager.GetComponent<SoundManager>();
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "Player")
        {
            sMan.PlaySound(sMan.bounceCol);
            Destroy(this.gameObject);
        }
    }

            // Update is called once per frame
            void Update () {
		
	}
}
