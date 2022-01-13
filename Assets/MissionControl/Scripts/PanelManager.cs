﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PanelManager : MonoBehaviour
{
    // Open Panel Function
    public void OpenPanel(GameObject Panel)
    {
        Panel.SetActive(true);
    }

    // Close Panel Function
    public void ClosePanel(GameObject Panel)
    {
        Panel.SetActive(false);
    }
}
