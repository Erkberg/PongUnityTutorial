using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAudio : MonoBehaviour
{
    public AudioSource asSounds;

    public AudioClip wallSound;
    public AudioClip paddleSound;

    public void PlayWallSound()
    {
        asSounds.PlayOneShot(wallSound);
    }

    public void PlayPaddleSound()
    {
        asSounds.PlayOneShot(paddleSound);
    }
}
