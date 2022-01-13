using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EpicMusicTrigger : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip epicStart;
    public AudioClip epicPart;

    private void Start()
    {
        audioSource.clip = epicStart;
        audioSource.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(StartEpic());
        }
    }

    public IEnumerator StartEpic()
    {
        audioSource.clip = epicPart;
        audioSource.Play();
        audioSource.volume = audioSource.volume / 2;
        yield return new WaitForSeconds(0);
        Destroy(gameObject);
    }
}
