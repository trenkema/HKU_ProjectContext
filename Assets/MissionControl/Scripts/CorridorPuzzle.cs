using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorridorPuzzle : MonoBehaviour
{
    public List<PlayerModes> playerModes = new List<PlayerModes>();
    private int playerID;

    private void Start()
    {
        playerID = (int)GameManager.thisPlayer;

        DisableAllCorridors();

        EnableCorridors();
    }

    /// <summary>
    /// Disables all corridors from all player modes.
    /// </summary>
    private void DisableAllCorridors()
    {
        for (int i = 0; i < playerModes.Count; i++)
        {
            for (int ii = 0; ii < playerModes[i].setDataPerPlayer.Count; ii++)
            {
                for (int iii = 0; iii < playerModes[i].setDataPerPlayer[ii].corridors.Length; iii++)
                {
                    playerModes[i].setDataPerPlayer[ii].corridors[iii].SetActive(false);
                }
            }
        }
    }

    /// <summary>
    /// Enable the corridors corresponding to the player mode.
    /// </summary>
    private void EnableCorridors()
    {
        for (int i = 0; i < playerModes.Count; i++)
        {
            if (i == GameManager.playerMode)
            {
                for (int ii = 0; ii < playerModes[i].setDataPerPlayer.Count; ii++)
                {
                    if (playerID == playerModes[i].setDataPerPlayer[ii].playerID)
                    {
                        for (int iii = 0; iii < playerModes[i].setDataPerPlayer[ii].corridors.Length; iii++)
                        {
                            playerModes[i].setDataPerPlayer[ii].corridors[iii].SetActive(true);
                        }
                    }
                }
            }
        }
    }
}

[System.Serializable]
public class PlayerModes
{
    public List<PlayerDataPuzzle3> setDataPerPlayer = new List<PlayerDataPuzzle3>();
}

[System.Serializable]
public class PlayerDataPuzzle3
{
    public GameObject[] corridors;
    public int playerID;
}