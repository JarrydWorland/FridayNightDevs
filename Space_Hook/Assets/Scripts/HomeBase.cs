using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeBase : MonoBehaviour {

    private PlayerController playerC;

	// Use this for initialization
	void Start () {
        playerC = LevelManger.Instance.player;
    }

    // Update is called once per frame
    void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(playerC.collectables.Count != 0)
        {
            foreach(GameObject g in playerC.collectables)
            {
                g.GetComponent<Collectable>().attatchedTo = this.gameObject;
                g.GetComponent<Collectable>().speed = 0.03f;
                playerC.collectables.Remove(g);
            }
        }
    }
}
