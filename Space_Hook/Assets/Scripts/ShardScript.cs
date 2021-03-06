﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShardScript : MonoBehaviour {
    
    private SoundManager sMan;
    public GameObject particles;

    // Use this for initialization
    void Start () {
        sMan = SoundManager.Instance;
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            sMan.PlaySound(sMan.bounceCol);
            GameObject p = Instantiate(Resources.Load<GameObject>("ShardCol"), transform.position, transform.rotation);

            p.GetComponent<ParticleSystem>().Play();
            Destroy(this.gameObject);
        }
        
    }
      // Update is called once per frame
            void Update () {
		
	}
}
