using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PuzzleKeyPadPuzzle4 : MonoBehaviour
{
    public System.Action onCorrect;

    private int playerID;

    public List<PlayerDataPuzzle4Z> playerData = new List<PlayerDataPuzzle4Z>();

    public GameObject[] allPuzzleSignsToDisable;
    //public List<DifferentPlayerModes4> playerModes = new List<DifferentPlayerModes4>();

    [Header("References")]
    public string standardText;
    public TextMeshPro answerInput;
    public TextMeshPro titleText;
    public GameObject doorToOpen;
    public AudioSource audioSource;
    public AudioClip doorOpen;
    public AudioClip doorClose;
    public AudioSource buttonAudioSource;
    public AudioClip buttonClick;
    public string afterCorrectText = "OPENED";

    [Header("Settings")]
    public string keyPadAnswer;
    private bool isCorrect = false;
    public bool isClicked = false;
    public float cooldown = 1f;
    public int maxLength = 8;

    [Header("RayCast")]
    public float maxDistance;
    RaycastHit hit;
    Ray ray;
    public LayerMask layers;

    private void Start()
    {
        playerID = (int)GameManager.thisPlayer;
        answerInput.text = standardText;

        foreach (GameObject sign in allPuzzleSignsToDisable)
        {
            sign.SetActive(false);
        }

        for (int i = 0; i < playerData.Count; i++)
        {
            if (i == GameManager.playerMode)
            {
                //titleText.text = playerData[i].titleText;
                keyPadAnswer = playerData[i].puzzleWord;

                for (int ii = 0; ii < playerData[i].setDataPerPlayer.Count; ii++)
                {
                    if (playerID == playerData[i].setDataPerPlayer[ii].playerID)
                    {
                        for (int iii = 0; iii < playerData[i].setDataPerPlayer[ii].perPlayerSigns.Length; iii++)
                        {
                            playerData[i].setDataPerPlayer[ii].perPlayerSigns[iii].SetActive(true);
                        }
                    }
                }
            }
        }
    }

    private void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            MouseClick();
        }
    }

    private void MouseClick()
    {
        if (Physics.Raycast(ray, out hit, maxDistance, layers, QueryTriggerInteraction.Ignore))
        {
            if (hit.transform.GetComponent<PuzzleKeyPadKeys>() != null && hit.transform.GetComponent<PuzzleKeyPadKeys>().puzzleKeyPad == gameObject)
            {
                if (buttonAudioSource != null)
                {
                    buttonAudioSource.clip = buttonClick;
                    buttonAudioSource.Play();
                }

                PuzzleKeyPadKeys keyPad = hit.transform.GetComponent<PuzzleKeyPadKeys>();
                char key = keyPad.key;

                if (!keyPad.enterKey && !keyPad.removeKey && !isClicked && !isCorrect)
                {
                    if (answerInput.text == standardText)
                    {
                        answerInput.text = "";
                    }

                    if (answerInput.text.Length < maxLength)
                    {
                        answerInput.text += key;
                    }

                }

                if (keyPad.enterKey && !isClicked && !isCorrect && answerInput.text.Length > 0)
                {
                    StartCoroutine(ValidateAnswer());
                }

                if (keyPad.removeKey && !isClicked && !isCorrect && answerInput.text.Length > 0 && answerInput.text != standardText)
                {
                    answerInput.text = answerInput.text.Substring(0, answerInput.text.Length - 1);

                    if (answerInput.text.Length == 0)
                    {
                        answerInput.text = standardText;
                    }
                }
            }
        }
    }

    public IEnumerator InCorrectAnswer()
    {
        isClicked = true;
        answerInput.text = "INCORRECT";
        yield return new WaitForSeconds(cooldown);
        isClicked = false;
        answerInput.text = standardText;
    }

    public IEnumerator ValidateAnswer()
    {
        if (isCorrect || isClicked)
        {
            yield break;
        }

        if (answerInput.text == keyPadAnswer)
        {
            onCorrect?.Invoke();
            isCorrect = true;
            isClicked = true;
            answerInput.text = "CORRECT";
            yield return new WaitForSeconds(cooldown);
            isClicked = false;
            answerInput.text = afterCorrectText;
            if (doorToOpen != null)
            {
                doorToOpen.GetComponent<Animator>().SetBool("isOpen", true);
                audioSource.clip = doorOpen;
                audioSource.Play();
            }
        }
        else
        {
            StartCoroutine(InCorrectAnswer());
        }
    }
}

[System.Serializable]
public class DifferentPlayerModes4
{
    public GameObject[] perPlayerSigns;
    public int playerID;
}

[System.Serializable]
public class PlayerDataPuzzle4Z
{
    public string titleText;
    public string puzzleWord;

    public List<DifferentPlayerModes4> setDataPerPlayer = new List<DifferentPlayerModes4>();
}