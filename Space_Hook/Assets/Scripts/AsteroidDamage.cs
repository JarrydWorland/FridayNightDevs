using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidDamage : AsteroidBehavior {

    void OnTriggerEnter2D(Collider2D collider)
    {
        // resets level if collides with player
        if (collider.GetComponent<PlayerController>() != null)
        {
            GameObject check = GameObject.Find("Manager");
            check.GetComponent<LevelManger>().ResetLevel();
        }
    }
}
