using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlinkingLightsButton : MonoBehaviour
{
    public Material notPressedMaterial;
    public Material pressedMaterial;
    public SpriteRenderer colorBlindShape;
    public BlinkingLightsButtonData buttonData;
    public Material buttonMaterialToChange;
    public BlinkingLightsMechanic blinkingLightsMechanic;
    public bool isClicked;
    public Material lampMaterial;
    public Material lampLightMaterial;
    public float emissionIntensityAfterPressed = 0.75f;

    private void Start()
    {
        for (int i = 0; i < blinkingLightsMechanic.blinkingLightDataCorrectPlayer.playerColors.Length; i++)
        {
            if (buttonData.buttonID == i)
            {
                Color color = blinkingLightsMechanic.blinkingLightDataCorrectPlayer.playerColors[i];
                lampMaterial.color = new Color(color.r, color.g, color.b, 50f / 255f);
                lampLightMaterial.SetColor("_EmissionColor", color);
                notPressedMaterial.color = blinkingLightsMechanic.blinkingLightDataCorrectPlayer.playerColors[i];
                colorBlindShape.sprite = blinkingLightsMechanic.blinkingLightDataCorrectPlayer.colorBlindShapes[i];
                colorBlindShape.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = blinkingLightsMechanic.blinkingLightDataCorrectPlayer.colorBlindShapes[i];
                buttonMaterialToChange.SetColor("_EmissionColor", color * 0.75f);
            }
        }

        blinkingLightsMechanic.onReset += StartReset;
    }

    public IEnumerator OnClicked(float buttonCooldown)
    {
        isClicked = true;

        if (blinkingLightsMechanic.visualPatternPressed.text.Length < blinkingLightsMechanic.maxVisualPatternLength)
        {
            blinkingLightsMechanic.pressedColorCode.Add(buttonData.buttonID);
            char colorCharPressed = blinkingLightsMechanic.blinkingLightDataCorrectPlayer.colorFirstChar[buttonData.buttonID];

            blinkingLightsMechanic.visualPatternPressed.text += (colorCharPressed + "-");
        }

        Color color = buttonMaterialToChange.GetColor("_EmissionColor");
        buttonMaterialToChange.SetColor("_EmissionColor", color * emissionIntensityAfterPressed);
        yield return new WaitForSeconds(buttonCooldown);
        buttonMaterialToChange.SetColor("_EmissionColor", color);
        isClicked = false;
    }

    public void StartReset()
    {
        StartCoroutine(ResetButton());
    }

    public IEnumerator ResetButton()
    {
        isClicked = true;
        yield return new WaitForSeconds(blinkingLightsMechanic.buttonCooldown);
        isClicked = false;
    }
}
