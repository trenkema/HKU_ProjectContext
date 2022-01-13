using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayIntroDelay : MonoBehaviour
{
    public AudioSource introAudioSource;
    public float startDelay = 1.5f;
    public DialogueSystem dialogueSystem;

    private void Start()
    {
        StartCoroutine(PlayIntro());
    }

    public IEnumerator PlayIntro()
    {
        yield return new WaitForSeconds(startDelay);
        introAudioSource.Play();
        dialogueSystem.StartIntro();
    }
}
