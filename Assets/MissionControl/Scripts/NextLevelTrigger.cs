using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelTrigger : MonoBehaviour
{
    public string level;
    AsyncOperation asyncOperation;
    public bool fadeOut;
    public Animator fadeAnimator;

    private void OnTriggerEnter(Collider other)
    {
            if (other.CompareTag("Player") && !fadeOut)
            {
                asyncOperation = SceneManager.LoadSceneAsync(level);
            }
            else if (other.CompareTag("Player") && fadeOut)
            {
                Debug.Log("TEST");
                fadeAnimator.SetBool("Fade", true);
            }
    }

    public void FadeOut()
    {
        asyncOperation = SceneManager.LoadSceneAsync(level);
    }

    public void StartGameFadeOut()
    {
        fadeAnimator.SetBool("Fade", true);
    }
}
