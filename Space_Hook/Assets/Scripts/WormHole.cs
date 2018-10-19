using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormHole : MonoBehaviour
{
    private WormLink link;

    void Start()
    {
        link = GetComponentInParent<WormLink>();
    }

    public void OnTriggerEnter2D(Collider2D coll)
    {
        link.Warp(coll.gameObject, this);
    }
}
