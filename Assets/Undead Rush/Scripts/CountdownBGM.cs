using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountdownBGM : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip countdownClip;

    void Start()
    {
        audioSource.PlayOneShot(countdownClip);
    }
}