using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueSystem : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public string[] sentences;
    private int index;
    public float typingSpeed;
    public float delayNextSentence;
    public float endLastSentenceDelay;

    private void Start()
    {
    }

    public void StartIntro()
    {
        StartCoroutine(Type());
    }

    IEnumerator Type()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        yield return new WaitForSeconds(delayNextSentence);
        StartCoroutine(NextSentence());
    }

    IEnumerator NextSentence()
    {
        if (index < sentences.Length - 1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        }
        else
        {
            yield return new WaitForSeconds(endLastSentenceDelay);
            textDisplay.text = "";
            textDisplay.enabled = false;
        }
    }
}
