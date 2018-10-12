using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManger : MonoBehaviour
{
    public PlayerController player;
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
        foreach(GameObject ast in Asteroids)
        {
            foreach(Transform t in ast.transform)
            {
                t.gameObject.SetActive(true);

            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.GetComponent<PlayerController>())
        {
            ResetLevel();
        }
    }
}
