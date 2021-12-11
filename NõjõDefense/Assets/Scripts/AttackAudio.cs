using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAudio : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip attackAudio;
    private AudioClip barulhoAudio;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        barulhoAudio = audioSource.clip;
    }

    public void PlayAttackAudio ()
    {
        audioSource.clip = attackAudio;
        audioSource.volume = 0.1f;
        audioSource.PlayOneShot(attackAudio);
        audioSource.clip = barulhoAudio;
    }
}
