using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManger : Singleton<LevelManger>
{
    public PlayerController player;
    public GameObject ForceField;
    public Vector3 StartPoint;
    public List<GameObject> Asteroids;

    void Awake()
    {
        StartPoint = player.transform.position;
    }

    public void ResetLevel()
    {
        Debug.Log("Level Reset");
        player.Reset();
        player.transform.position = StartPoint;
        foreach(GameObject g in Asteroids)
        {
            foreach (Transform t in g.transform)
            {
                t.gameObject.SetActive(true);
                t.transform.position = t.gameObject.GetComponent<AsteroidBehavior>().startPosition;
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.GetComponent<PlayerController>())
        {
            ResetLevel();
        }
        if (collision.GetComponent<AsteroidBehavior>())
        {
            collision.gameObject.SetActive(false);
        }
    }
}
