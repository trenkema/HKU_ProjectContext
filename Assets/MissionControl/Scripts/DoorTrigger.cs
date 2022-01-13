using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public Animator doorToOpen;
    public AudioSource audioSource;
    public AudioClip doorOpen;
    public AudioClip doorClose;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doorToOpen.SetBool("isOpen", true);
            audioSource.clip = doorOpen;
            audioSource.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doorToOpen.SetBool("isOpen", false);
            audioSource.clip = doorClose;
            audioSource.Play();
        }
    }
}
