using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : MonoBehaviour {

    private List<GameObject> asteroids;

	// Use this for initialization
	void Start () {
		foreach(Transform child in transform)
        {
            asteroids.Add(child.gameObject);
        }
	}
	
    void Bouncy(GameObject ast)
    {

    }

    void Damage(GameObject ast)
    {

    }

	// Update is called once per frame
	void Update () {
       
        foreach(GameObject ast in asteroids)
        {
            if(ast.tag == "Bouncy")
            {
                Bouncy(ast);
            }
            if(ast.tag == "Damage")
            {
                Damage(ast);
            }
        }
		
	}
}
