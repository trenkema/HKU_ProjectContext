using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

//idc wat je vindt van hoe dit gecode is
public class MinigameTrigger : MonoBehaviour
{
    public GameObject[] toEnable;

    public GameObject text;
    private bool inRange = false;

    private void Start()
    {
        text.SetActive(false);
    }

    private void Update()
    {
        if (inRange)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                for (int i = 0; i < toEnable.Length; i++)
                {
                    if (toEnable[i].GetComponent<SetForPlayer>() && toEnable[i].GetComponent<SetForPlayer>().differentModes[GameManager.playerMode].visibleFor != GameManager.thisPlayer)
                    {
                        toEnable[i].SetActive(false);
                    }
                    else
                    {
                        toEnable[i].SetActive(true);
                    }
                }
                gameObject.SetActive(false);
                text.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider coll)
    {
        text.SetActive(true);
        inRange = true;
    }

    private void OnTriggerExit(Collider coll)
    {
        text.SetActive(false);
        inRange = false;
    }
}
