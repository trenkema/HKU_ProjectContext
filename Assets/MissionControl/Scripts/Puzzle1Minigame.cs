using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Puzzle1Minigame : MonoBehaviour
{
    public string title;
    public string description;
    public string start;
    public GameObject missingPipe;
    public GameObject fixedPipe;
    public GameObject particles;
    public PuzzleKeyPad puzzleKeyPad;
    public PuzzleKeyPad puzzlePadToActivate;
    private string oldStandardText;

    public AudioSource audioSource;
    public AudioClip buttonClick;

    public TextMeshPro titleText;
    public TextMeshPro descriptionText;
    public TextMeshPro startText;
    private bool selectingMinigame = false;
    private bool enteringCode = false;

    [Header("RayCast")]
    public float maxDistance;
    RaycastHit hit;
    Ray ray;
    public LayerMask layers;

    private void Start()
    {
        enteringCode = false;
        selectingMinigame = false;
        oldStandardText = puzzlePadToActivate.standardText;
        puzzlePadToActivate.isClicked = true;
        puzzlePadToActivate.standardText = "PIPE MISSING";
        puzzlePadToActivate.answerInput.text = puzzlePadToActivate.standardText;
        puzzleKeyPad.onCorrect += OnCompletion;
        puzzleKeyPad.enabled = false;
        fixedPipe.SetActive(false);
        missingPipe.SetActive(true);
        titleText.text = title;
        descriptionText.text = description;
        startText.text = start;
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
            if (hit.transform.GetComponent<PuzzleKeyPadKeys>() != null)
            {
                if (audioSource != null)
                {
                    audioSource.clip = buttonClick;
                    audioSource.Play();
                }

                PuzzleKeyPadKeys keyPad = hit.transform.GetComponent<PuzzleKeyPadKeys>();
                char key = keyPad.key;

                if (keyPad.enterKey && !enteringCode)
                {
                    ShowMinigames();
                }

                if (keyPad.removeKey && selectingMinigame && !enteringCode)
                {
                    EnterCodeState();
                }
            }
        }
    }

    public void ShowMinigames()
    {
        titleText.text = "SELECT MINIGAME";
        descriptionText.text = "PLAY ONE GAME AMONG US";
        startText.text = "PRESS REMOVE TO START";
        selectingMinigame = true;
    }

    public void EnterCodeState()
    {
        enteringCode = true;
        titleText.text = "ENTER CODE";
        descriptionText.enabled = false;
        startText.text = "PRESS ENTER TO VALIDATE";
        puzzleKeyPad.enabled = true;
    }

    public void OnCompletion()
    {
        puzzlePadToActivate.isClicked = false;
        puzzlePadToActivate.standardText = oldStandardText;
        puzzlePadToActivate.answerInput.text = oldStandardText;
        startText.text = "";
        titleText.text = "";
        fixedPipe.SetActive(true);
        missingPipe.SetActive(false);
        if (particles != null)
        {
            GameObject particleExplosion = Instantiate(particles, fixedPipe.transform.position, Quaternion.identity);
            Destroy(particleExplosion, 5f);
        }
    }
}
