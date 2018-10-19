using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager> {

    public AudioClip dmgCol;
    public AudioClip bounceCol;
    public AudioClip SpeedUp;
    public AudioClip Forcefield;

    public AudioSource soundToPlay;
    public AudioSource SecondsoundToPlay;


    public float startingPitch = 1;
	// Use this for initialization
	void Start () {
        //soundToPlay = GetComponent<AudioSource>();
	}
	public void ChangePitch(float f)
    {
        if (!soundToPlay && !SecondsoundToPlay)
        {
            Debug.Log("No sounds");
            return;
        }
        soundToPlay.pitch = f;
        SecondsoundToPlay.pitch = f;
    }
    public void PlaySound(AudioClip sound)
    {
        if (!sound)
        {
            Debug.Log("No sounds");
            return;
        }
        if (sound == Forcefield)
        {
            SecondsoundToPlay.clip = sound;
            SecondsoundToPlay.loop = true;
            SecondsoundToPlay.Play();
        }

        else
        {
            soundToPlay.clip = sound;
            soundToPlay.Play();
        }
    }

    public void StopSound(AudioSource soundSource)
    {
        return;
        // TODO FIX THIS;
        if (soundSource.clip != null)
        {
            soundSource.clip = null;
        }
    }

    // Update is called once per frame
	void Update () {
		
	}
}
