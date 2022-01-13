using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum Players { one=1, two, three, four }
    public static int playerMode;
    public static Players thisPlayer;
    //public static PlayerMode thisPlayerMode;
    public static GameManager Instance { get; private set; }
    public GameObject player;
    public FSM fsm;

    private void Start()
    {
        if (player == null)
        {
            if (GameObject.FindGameObjectWithTag("Player") != null)
            {
                player = GameObject.FindGameObjectWithTag("Player");
            }
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void SetPlayer(int id)
    {
        thisPlayer = (Players)id;
    }

    public void SetPlayerMode(int id)
    {
        playerMode = id;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}