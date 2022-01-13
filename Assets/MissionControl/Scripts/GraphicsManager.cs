using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class GraphicsManager : MonoBehaviour
{
    public GameObject lowButtonActive, mediumButtonActive, highButtonActive;

    public PostProcessVolume postProcessVolume;
    ScreenSpaceReflections SSR;

    private void Start()
    {
        postProcessVolume.profile.TryGetSettings(out SSR);
        GetSSRQuality();
    }

    public void SetSSRQuality(string quality)
    {
        PlayerPrefs.SetString("SSR", quality.ToLower());
        GetSSRQuality();
    }

    public void GetSSRQuality()
    {
        switch (PlayerPrefs.GetString("SSR", "high"))
        {
            case "low":
                lowButtonActive.SetActive(true);
                mediumButtonActive.SetActive(false);
                highButtonActive.SetActive(false);
                SSR.preset.value = ScreenSpaceReflectionPreset.Low;
                break;
            case "medium":
                lowButtonActive.SetActive(false);
                mediumButtonActive.SetActive(true);
                highButtonActive.SetActive(false);
                SSR.preset.value = ScreenSpaceReflectionPreset.High;
                break;
            case "high":
                lowButtonActive.SetActive(false);
                mediumButtonActive.SetActive(false);
                highButtonActive.SetActive(true);
                SSR.preset.value = ScreenSpaceReflectionPreset.Overkill;
                break;
        }
    }
}
