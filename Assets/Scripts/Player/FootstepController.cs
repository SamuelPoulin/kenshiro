using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepController : MonoBehaviour {

    public AudioClip footstep;
    public Animator animator;

    AudioSource audiosource;

    private void Awake()
    {
        audiosource = GetComponent<AudioSource>();
    }

    void Footstep()
    {
            audiosource.PlayOneShot(footstep);
    }
}
