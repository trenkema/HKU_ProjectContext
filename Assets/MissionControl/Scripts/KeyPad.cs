using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class KeyPad : MonoBehaviour
{
    private const string correctCombi = "3542136";
    public Text inputField;

    public void InputNumber(int num)
    {
        if (inputField.text == "Enter 7 digit code...")
        {
            inputField.text = "";
        }

        if (inputField.text.Length < 7)
        {
            inputField.text += num;

            Debug.Log("pressed " + num);
        }
    }

    public void ResetNumbers()
    {
        inputField.text = "Enter 7 digit code...";
    }

    public void EnterCombination()
    {
        if (inputField.text == correctCombi)
        {
            Destroy(GameObject.Find("Exit"));
        }
        else
        {
            StartCoroutine(WrongCode());
        }
    }

    private IEnumerator WrongCode()
    {
        inputField.text = "Incorrect!";

        yield return new WaitForSeconds(1);

        ResetNumbers();
    }
}
