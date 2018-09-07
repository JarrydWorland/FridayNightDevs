using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLevel : MonoBehaviour {

    public GameObject levelObject;

    public float spawnRange;

    public int amtToSpawn;

	void Start () {

        for (int i = 0; i < amtToSpawn; i++)
        {
           var newAsteroid = Instantiate(levelObject, new Vector3(Random.Range(-spawnRange, spawnRange), Random.Range(-spawnRange, spawnRange), 0), Quaternion.identity);
        }		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
