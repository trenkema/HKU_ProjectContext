using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM : MonoBehaviour
{
    public GameObject player;
    public GameObject pauseMenu;
    private PlayerLook playerLook;
    private PlayerMovement playerMovement;
    public GameObject crossHair;
    private bool inPauseMenu = false;
    public bool canOpenPauseMenu = true;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        playerLook = player.GetComponent<PlayerLook>();
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !pauseMenu.activeSelf && !inPauseMenu && canOpenPauseMenu)
        {
            OpenPauseMenu();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && pauseMenu.activeSelf && inPauseMenu)
        {
            ClosePauseMenu();
        }
    }

    public void OpenPauseMenu()
    {
        inPauseMenu = true;
        pauseMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        playerLook.enabled = false;
        playerMovement.enabled = false;
        crossHair.SetActive(false);
    }

    public void ClosePauseMenu()
    {
        inPauseMenu = false;
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        playerLook.enabled = true;
        playerMovement.enabled = true;
        crossHair.SetActive(true);
    }

    public void InPauseMenu()
    {
        inPauseMenu = true;
    }

    public void NotInPauseMenu()
    {
        inPauseMenu = false;
    }
}
