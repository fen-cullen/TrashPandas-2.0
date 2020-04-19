using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaccoonEffectPlayer : MonoBehaviour
{
    // The arrays that contain all the sound effects for the raccoon.
    // When a sound is played, pick a source from the array at random.
    public AudioSource[] jumpSounds;
    public AudioSource[] landingSounds;
    public AudioSource[] walkingSounds;
    public AudioSource[] eatSounds;

    //tells if the walking sounds are currently being played
    private bool playWalking = false;

    public void PlayJumpSound()
    {
        print("playing jump sound");
        AudioSource sound = jumpSounds[Random.Range(0, jumpSounds.Length)];
        sound.PlayOneShot(sound.clip);
    }

    public void PlayLandingSound()
    {
        AudioSource sound = landingSounds[Random.Range(0, landingSounds.Length)];
        sound.PlayOneShot(sound.clip);
    }

    public void StartWalkingSound()
    {
        print("starting walk sound");
        playWalking = true;
        StartCoroutine("ContinueWalkingSound");

    }

    IEnumerator ContinueWalkingSound()
    {
        if (playWalking)
        {
            AudioSource sound = walkingSounds[Random.Range(0, walkingSounds.Length)];
            sound.PlayOneShot(sound.clip);
            yield return new WaitForSeconds(0.3f);
            StartCoroutine("ContinueWalkingSound");
        }
    }

    public void StopWalkingSound()
    {
        print("stopping walk sound");
        playWalking = false;
    }

    public void PlayEatSound()
    {
        print("play eat sound");
        AudioSource sound = eatSounds[Random.Range(0, eatSounds.Length)];
        sound.PlayOneShot(sound.clip);
    }
}
