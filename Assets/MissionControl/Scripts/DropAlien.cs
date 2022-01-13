using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DropAlien : MonoBehaviour
{
    [Header("Raycasts")]
    RaycastHit hit;
    Ray ray;
    public LayerMask layers;
    public float maxDistance;

    public GameObject dropButton;

    public AudioSource alienSource;
    public AudioSource buttonSource;
    public AudioClip endClipAlien;

    public AudioSource bgMusicSource;
    public AudioClip intenseBGMusic;

    public Animator hatchAnimator;

    public float delay = 1f;

    private bool introPlayed = false;

    public GameObject crossHair;

    public GameObject endText;
    public GameObject player;
    public GameObject endCamera;

    public GameObject earthLight;

    public string mainMenuScene;
    public bool canReturnToMenu = false;

    public NextLevelTrigger nextLevelTrigger;

    private void Start()
    {
        endText.SetActive(false);
    }

    private void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            MouseClick();
        }

        if (canReturnToMenu)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                nextLevelTrigger.StartGameFadeOut();
            }
        }
    }

    private void MouseClick()
    {
        if (Physics.Raycast(ray, out hit, maxDistance, layers, QueryTriggerInteraction.Ignore))
        {
            if (hit.transform.gameObject == dropButton)
            {
                buttonSource.Play();
                StartCoroutine(StartEndScene(delay));
            }
        }
    }

    public IEnumerator StartEndScene(float _delay)
    {
        crossHair.SetActive(false);
        dropButton.SetActive(false);
        hatchAnimator.SetBool("isOpen", true);
        alienSource.clip = endClipAlien;
        alienSource.Play();
        yield return new WaitForSeconds(_delay);
        GameManager.Instance.fsm.canOpenPauseMenu = false;
        canReturnToMenu = true;
        earthLight.GetComponent<RotateObject>().rotateSpeed = 6f;
        bgMusicSource.volume = 0.3f;
        player.SetActive(false);
        endText.SetActive(true);
        endCamera.SetActive(true);
        endCamera.GetComponent<Animator>().SetBool("isEnd", true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!introPlayed)
        {
            bgMusicSource.clip = intenseBGMusic;
            bgMusicSource.Play();
            introPlayed = true;
        }
    }
}
