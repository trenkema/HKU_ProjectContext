using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle4 : MonoBehaviour
{
    public List<PlayerData> playerData = new List<PlayerData>();
    private int playerID;

    private void Start()
    {
        playerID = (int)GameManager.thisPlayer;

        for (int i = 0; i < playerData.Count; i++)
        {
            if (playerID == playerData[i].playerID)
            {
                GameManager.Instance.player.transform.position = playerData[i].spawnPoint.transform.position;
            }
        }

        // Disable All
        for (int i = 0; i < playerData.Count; i++)
        {
            playerData[i].words.SetActive(false);
        }
        
        // Enable Correct One
        for (int i = 0; i < playerData.Count; i++)
        {
            if (playerID == playerData[i].playerID)
            {
                playerData[i].words.SetActive(true);
                playerData[i].doorAnimator.SetBool("isOpen", true);
            }
        }
    }
}

[System.Serializable]
public class PlayerData
{
    public GameObject words;
    public int playerID;
    public Animator doorAnimator;
    public GameObject spawnPoint;
}
