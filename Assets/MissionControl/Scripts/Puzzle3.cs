using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle3 : MonoBehaviour
{
    public List<DifferentPlayerModes3> differentModes = new List<DifferentPlayerModes3>(); // NEW
    //public List<PlayerDataPuzzle3> playerData = new List<PlayerDataPuzzle3>();
    private int playerID;

    private void Start()
    {
        playerID = (int)GameManager.thisPlayer;

        for (int i = 0; i < differentModes.Count; i++)
        {
            // Disable All
            for (int ii = 0; ii < differentModes[i].setDataPerPlayer.Count; ii++)
            {
                for (int iii = 0; iii < differentModes[i].setDataPerPlayer[ii].corridors.Length; iii++)
                {
                    differentModes[i].setDataPerPlayer[ii].corridors[iii].SetActive(false);
                }
            }
        }

        // Enable Correct One
        for (int i = 0; i < differentModes.Count; i++)
        {
            if (i == GameManager.playerMode)
            {
                for (int ii = 0; ii < differentModes[i].setDataPerPlayer.Count; ii++)
                {
                    if (playerID == differentModes[i].setDataPerPlayer[ii].playerID)
                    {
                        for (int iii = 0; iii < differentModes[i].setDataPerPlayer[ii].corridors.Length; iii++)
                        {
                            differentModes[i].setDataPerPlayer[ii].corridors[iii].SetActive(true);
                        }
                    }
                }
            }
        }

        // Enable Correct One
        //for (int i = 0; i < playerData.Count; i++)
        //{
        //    if (playerID == playerData[i].playerID)
        //    {
        //        for (int ii = 0; ii < playerData[i].corridors.Length; ii++)
        //        {
        //            playerData[i].corridors[ii].SetActive(true);
        //        }
        //    }
        //}
    }
}

[System.Serializable]
public class DifferentPlayerModes3
{
    public List<PlayerDataPuzzle3> setDataPerPlayer = new List<PlayerDataPuzzle3>();
}

[System.Serializable]
public class PlayerDataPuzzle3
{
    public GameObject[] corridors;
    public int playerID;
}