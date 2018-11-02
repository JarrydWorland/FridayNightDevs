using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class WormLink : MonoBehaviour
{
    public Color OrbColour = new Color(189, 2, 255);
    public Color RimColour = new Color(135,0, 255);
    public Gradient WaveGradient;
    public float WarpCoolDown = 2;
    private WormHole hole1;
    private WormHole hole2;
    private Dictionary<GameObject,float> WrapedObjects;
    // Use this for initializ135ation
    void Awake ()
    {
        WrapedObjects = new Dictionary<GameObject, float>();
        var holes = GetComponentsInChildren<WormHole>();
        hole1 = holes[0];
        hole2 = holes[1];
        for (int i = 0; i < holes.Length; i++)
        {
             var particles = holes[i].GetComponentsInChildren<ParticleSystem>();
            particles[1].startColor = RimColour;
            var COL1 = particles[2].colorOverLifetime;
            COL1.color = WaveGradient;
            var COL2 = particles[3].colorOverLifetime;
            COL2.color = WaveGradient;
            particles[4].startColor = OrbColour;
        }
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
            if (LevelManger.Instance.player.attatchedTo != null)
            {
                LevelManger.Instance.player.Detatch();
            }
            // TODO fix Camera to zoom to then  telaport;
            CompleteCameraController.Instance.MoveCameraTo(WarpedTarget.transform.position);
        }
        else if (WarpedTarget.GetComponent<AsteroidBehavior>())
        {
            if (LevelManger.Instance.player.attatchedTo != null)
            {
                LevelManger.Instance.player.Detatch();
            }
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
