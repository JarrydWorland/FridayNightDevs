using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

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
        soundToPlay.pitch = f;
        SecondsoundToPlay.pitch = f;
    }
    public void PlaySound(AudioClip sound)
    {
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
	// Update is called once per frame
	void Update () {
		
	}
}
