using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillOnTouch : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<PlayerController>() != null)
        {

            GameObject check = GameObject.Find("Manager");
            check.GetComponent<LevelManger>().ResetLevel();
        }
    }

}
