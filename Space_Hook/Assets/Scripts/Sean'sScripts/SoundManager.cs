using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public AudioClip dmgCol;
    public AudioClip bounceCol;
    public AudioClip SpeedUp;
    public AudioSource soundToPlay;
    public float startingPitch = 1;
	// Use this for initialization
	void Start () {
        //soundToPlay = GetComponent<AudioSource>();
	}
	public void ChangePitch(float f)
    {
        soundToPlay.pitch = f;
    }
    public void PlaySound(AudioClip sound)
    {
        if(sound )
        soundToPlay.clip = sound;
        soundToPlay.Play();
    }
	// Update is called once per frame
	void Update () {
		
	}
}
