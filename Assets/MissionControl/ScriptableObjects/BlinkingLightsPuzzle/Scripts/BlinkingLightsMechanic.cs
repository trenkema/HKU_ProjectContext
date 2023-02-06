using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BlinkingLightsMechanic : MonoBehaviour
{
    public System.Action onReset;

    [Header("References")]
    public GameObject informationBoard;
    public Explosion explosionGameObject;
    public Material notRunningMaterial;
    public Material runningMaterial;
    public BlinkingLightsData blinkingLightData;
    public BlinkingLightsData blinkingLightDataCorrectPlayer;
    public GameObject resetPart;
    public GameObject resetButtonToDisable;
    public GameObject enterPart;
    public GameObject enterButtonToDisable;
    public GameObject startPatternButton;
    public GameObject destructionButton1;
    public GameObject destructionButton2;
    public GameObject destructionButtonCapCollider1;
    public GameObject destructionButtonCapCollider2;
    public GameObject[] destructionButtonProtection1;
    public GameObject[] destructionButtonProtection2;
    public TextMeshPro startPatternText;
    public TextMeshPro resetText;
    public TextMeshPro enterText;
    public TextMeshPro visualPatternPressed;
    public int maxVisualPatternLength;
    public GameObject doorToOpen;
    public AudioSource audioSource;
    public AudioClip doorOpen;
    public AudioClip doorClose;

    public AudioSource buttonAudioSource;
    public AudioClip buttonClick;

    [Header("Settings")]
    public float inBetweenLampSwitchTime;
    public float waitTimeBetweenPatterns;
    public float maxDistance;
    private bool patternRunning;
    public float buttonCooldown;
    private MeshRenderer meshRenderer;
    public List<DifferentPlayerModes> differentModes = new List<DifferentPlayerModes>(); // NEW
    //public List<SetDataPerPlayer> setDataPerPlayer = new List<SetDataPerPlayer>();
    private bool isClicked = false;
    private bool isDone = false;
    private bool isDestructed = false;
    private int amountOfCorrectPresses = 0;
    private int playerID;
    public bool destructionButtonUncapped1 = false;
    public bool destructionButtonUncapped2 = false;

    public Material[] lightMaterials;
    public SpriteRenderer[] colorBlindShapes;

    public List<int> pressedColorCode = new List<int>();

    [Header("Buttons")]
    RaycastHit hit;
    Ray ray;
    public LayerMask layers;

    private void Awake()
    {
        playerID = (int)GameManager.thisPlayer;

        for (int i = 0; i < differentModes.Count; i++)
        {
            if (i == GameManager.playerMode)
            {
                for (int ii = 0; ii < differentModes[i].setDataPerPlayer.Count; ii++)
                {
                    if (playerID == differentModes[i].setDataPerPlayer[ii].playerID)
                    {
                        blinkingLightData = differentModes[i].setDataPerPlayer[ii].blinkingLightData;
                        blinkingLightDataCorrectPlayer = differentModes[i].setDataPerPlayer[ii].blinkingLightDataCorrectPlayer;
                    }
                }
            }
        }

        //for (int i = 0; i < setDataPerPlayer.Count; i++)
        //{
        //    if (playerID == setDataPerPlayer[i].playerID)
        //    {
        //        blinkingLightData = setDataPerPlayer[i].blinkingLightData;
        //        blinkingLightDataCorrectPlayer = setDataPerPlayer[i].blinkingLightDataCorrectPlayer;
        //    }
        //}
    }

    private void Start()
    {
        maxVisualPatternLength = (blinkingLightDataCorrectPlayer.patternList.Count * 2) + 1;
        destructionButton1.SetActive(false);
        destructionButton2.SetActive(false);
        visualPatternPressed.text = "-";
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = notRunningMaterial;

        foreach (Material lightMaterial in lightMaterials)
        {
            lightMaterial.DisableKeyword("_EMISSION");
        }

        foreach (SpriteRenderer colorBlindShape in colorBlindShapes)
        {
            colorBlindShape.gameObject.SetActive(false);
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
            if (hit.transform.GetComponent<BlinkingLightsButton>() != null)
            {
                BlinkingLightsButton button = hit.transform.GetComponent<BlinkingLightsButton>();
                if (!button.isClicked)
                {
                    buttonAudioSource.clip = buttonClick;
                    buttonAudioSource.Play();
                    button.StartCoroutine(button.OnClicked(buttonCooldown));
                }
            }

            if (hit.transform.gameObject == startPatternButton && !patternRunning)
            {
                buttonAudioSource.clip = buttonClick;
                buttonAudioSource.Play();
                StartCoroutine(StartPattern());
            }

            if (hit.transform.gameObject == resetPart && !isClicked && !isDone)
            {
                buttonAudioSource.clip = buttonClick;
                buttonAudioSource.Play();
                StartCoroutine(ResetButton());
            }

            if (hit.transform.gameObject == enterPart && !isClicked && !isDone)
            {
                buttonAudioSource.clip = buttonClick;
                buttonAudioSource.Play();
                StartCoroutine(EnterButton());
            }

            if (hit.transform.gameObject == destructionButton1 || hit.transform.gameObject == destructionButton2 && !isClicked && isDone && !isDestructed && destructionButtonUncapped1 && destructionButtonUncapped2)
            {
                buttonAudioSource.clip = buttonClick;
                buttonAudioSource.Play();
                destructionButton1.GetComponent<TriggerObject>().triggerTextPositive.SetActive(false);
                destructionButton2.GetComponent<TriggerObject>().triggerTextPositive.SetActive(false);
                destructionButton1.SetActive(false);
                destructionButton2.SetActive(false);
                StartCoroutine(DestructionButton(buttonCooldown));
            }

            if (hit.transform.gameObject == destructionButtonCapCollider1 && !isClicked && isDone && !isDestructed && !destructionButtonUncapped1)
            {
                destructionButtonUncapped1 = true;
                buttonAudioSource.clip = buttonClick;
                buttonAudioSource.Play();
                destructionButtonCapCollider1.GetComponent<TriggerObject>().triggerTextPositive.SetActive(false);

                foreach (GameObject protection in destructionButtonProtection1)
                {
                    protection.SetActive(false);
                }

                destructionButton1.GetComponent<TriggerObject>().enabled = true;
            }

            if (hit.transform.gameObject == destructionButtonCapCollider2 && !isClicked && isDone && !isDestructed && !destructionButtonUncapped2)
            {
                destructionButtonUncapped2 = true;
                buttonAudioSource.clip = buttonClick;
                buttonAudioSource.Play();
                destructionButtonCapCollider2.GetComponent<TriggerObject>().triggerTextPositive.SetActive(false);

                foreach (GameObject protection in destructionButtonProtection2)
                {
                    protection.SetActive(false);
                }

                destructionButton2.GetComponent<TriggerObject>().enabled = true;
            }
        }
    }

    public IEnumerator ResetButton()
    {
        pressedColorCode.Clear();
        visualPatternPressed.text = "-";
        amountOfCorrectPresses = 0;
        isClicked = true;
        resetText.text = "CLEARED";
        onReset?.Invoke();
        yield return new WaitForSeconds(buttonCooldown);
        resetText.text = "RESET";
        isClicked = false;
    }

    public IEnumerator EnterButton()
    {
        isClicked = true;
        for (int i = 0; i < blinkingLightDataCorrectPlayer.patternList.Count; i++)
        {
            if (pressedColorCode.Count > blinkingLightDataCorrectPlayer.patternList.Count || pressedColorCode.Count < blinkingLightDataCorrectPlayer.patternList.Count)
            {
                amountOfCorrectPresses = 0;
                break;
            }

            if (pressedColorCode[i] == blinkingLightDataCorrectPlayer.patternList[i].colorCodeSingleINT)
            {
                amountOfCorrectPresses++;
            }
        }

        if (amountOfCorrectPresses == blinkingLightDataCorrectPlayer.patternList.Count)
        {
            isDone = true;
            enterText.text = "CORRECT";
            yield return new WaitForSeconds(buttonCooldown);
            enterButtonToDisable.SetActive(false);
            resetButtonToDisable.SetActive(false);
            destructionButton1.SetActive(true);
            destructionButton2.SetActive(true);
        }
        else
        {
            onReset?.Invoke();
            StartCoroutine(ResetButton());
            enterText.text = "WRONG";
        }

        yield return new WaitForSeconds(buttonCooldown);
        enterText.text = "ENTER";
        isClicked = false;
    }

    public IEnumerator DestructionButton(float cooldown)
    {
        informationBoard.SetActive(false);
        startPatternButton.transform.GetChild(0).gameObject.SetActive(false);
        startPatternButton.GetComponent<MeshRenderer>().enabled = false;
        startPatternButton.GetComponent<Collider>().enabled = false;
        isDestructed = true;
        isClicked = true;
        doorToOpen.GetComponent<Animator>().SetBool("isOpen", true);
        audioSource.clip = doorOpen;
        explosionGameObject.Explode();
        audioSource.Play();
        yield return new WaitForSeconds(cooldown);
        isClicked = false;
    }

    /// <summary>
    /// Enables and disables the right shape and continent of the earth
    /// to lit up in the specified sequence and color.
    /// </summary>
    /// <param name="waitForNewPattern"></param>
    /// <returns></returns>
    public IEnumerator StartPattern()
    {
        meshRenderer.material = runningMaterial;

        for (int i = 0; i < blinkingLightData.patternList.Count; i++)
        {
            int colorCodeINT = blinkingLightData.patternList[i].colorCodeSingleINT;
            Color emissionColor = blinkingLightData.playerColors[colorCodeINT];
            patternRunning = true;
            int randomLight = Random.Range(0, lightMaterials.Length);
            yield return new WaitForSeconds(inBetweenLampSwitchTime / 2);

            colorBlindShapes[randomLight].sprite = blinkingLightData.colorBlindShapes[colorCodeINT];
            colorBlindShapes[randomLight].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = 
                blinkingLightData.colorBlindShapes[colorCodeINT];
            colorBlindShapes[randomLight].gameObject.SetActive(true);
            lightMaterials[randomLight].SetColor("_EmissionColor", emissionColor);
            lightMaterials[randomLight].EnableKeyword("_EMISSION");
            yield return new WaitForSeconds(inBetweenLampSwitchTime);

            lightMaterials[randomLight].DisableKeyword("_EMISSION");
            colorBlindShapes[randomLight].gameObject.SetActive(false);
        }

        patternRunning = false;
        meshRenderer.material = notRunningMaterial;
    }
}

[System.Serializable]
public class SetDataPerPlayer
{
    public BlinkingLightsData blinkingLightData;
    public BlinkingLightsData blinkingLightDataCorrectPlayer;
    public int playerID;
}

[System.Serializable]
public class DifferentPlayerModes
{
    public List<SetDataPerPlayer> setDataPerPlayer = new List<SetDataPerPlayer>();
}
