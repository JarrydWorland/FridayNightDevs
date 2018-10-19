using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class WormLink : MonoBehaviour
{
    public float WarpCoolDown = 2;
    private WormHole hole1;
    private WormHole hole2;
    private Dictionary<GameObject,float> WrapedObjects;
    // Use this for initialization
    void Awake ()
    {
        WrapedObjects = new Dictionary<GameObject, float>();
        var holes = GetComponentsInChildren<WormHole>();
        hole1 = holes[0];
        hole2 = holes[1];
    }

    public void Warp(GameObject WarpedTarget, WormHole enterHole)
    {
        float temp = 0;
        if (WrapedObjects.TryGetValue(WarpedTarget, out temp))
        {
            return;
        }
        WrapedObjects.Add(WarpedTarget,Time.time);
        if (enterHole == hole1)
        {
            WarpedTarget.transform.position = hole2.transform.position;
        }
        else
        {
            WarpedTarget.transform.position = hole1.transform.position;
        }
        if (WarpedTarget.GetComponent<PlayerController>())
        {
            LevelManger.Instance.player.Detatch();
            CompleteCameraController.Instance.MoveCameraTo(WarpedTarget.transform.position);
        }
        else if (WarpedTarget.GetComponent<AsteroidBehavior>())
        {
            LevelManger.Instance.player.Detatch();
        }
    }

    void Update()
    {
        if (WrapedObjects.Count < 1)
        {
            return;
        }
        var itemsToRemove = WrapedObjects.Where(f => f.Value + WarpCoolDown < Time.time).ToArray();
        foreach (var item in itemsToRemove)
        {
            WrapedObjects.Remove(item.Key);
        }
    }
}
