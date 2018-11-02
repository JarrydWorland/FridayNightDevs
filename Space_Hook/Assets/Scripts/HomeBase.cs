using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeBase : MonoBehaviour {

    private PlayerController playerC;
    public List<GameObject> collected;

	// Use this for initialization
	void Start () {
        playerC = LevelManger.Instance.player;
    }

    // Update is called once per frame
    void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (playerC.collectables.Count != 0)
            {
                foreach (GameObject g in playerC.collectables)
                {
                    g.GetComponent<Collectable>().attatchedTo = this.gameObject;
                    collected.Add(g);
                    g.GetComponent<Collectable>().speed = 0.03f;
                }
                playerC.collectables.Clear();
            }
        }
    }

    private void OnMouseDown()
    {
        float pullForce = playerC.GetComponent<ConstantSpeed>().Speed * 5;
        Vector2 betweenFP = ((Vector2)transform.position - (Vector2)playerC.transform.position);
        Vector2 pullDirection = betweenFP.normalized;

        playerC.rb2d.velocity = Vector2.zero;
        playerC.rb2d.AddForce(pullForce * pullDirection);
    }
}
