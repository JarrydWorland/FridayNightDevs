using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManger : MonoBehaviour
{
    public PlayerController player;
    public Vector3 StartPoint;
    void Awake()
    {
        StartPoint = player.transform.position;
    }

    public void ResetLevel()
    {
        Debug.Log("Level Reset");
        player.Reset();
        player.transform.position = StartPoint;
    }

    public void OnTriggerExit2D(Collider2D collider2D)
    {
        if (collider2D.GetComponent<PlayerController>())
        {
            ResetLevel();
        }
    }
}
