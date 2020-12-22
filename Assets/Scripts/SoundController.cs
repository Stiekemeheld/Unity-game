using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{

    public AudioClip mySound;
	AudioSource audioData;
    private bool alreadyPlayed;

    void Start()
    {
        alreadyPlayed = false;
        audioData = GetComponent<AudioSource>();
    }

	void OnTriggerEnter2D(Collider2D other){

        if (!alreadyPlayed)
        {
            audioData.PlayOneShot(mySound, 1);
            alreadyPlayed = true;
        }
	}

}