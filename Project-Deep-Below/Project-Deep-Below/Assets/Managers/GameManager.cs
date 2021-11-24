//Game Manager script
//Creater: King
//Date: 11/22/21

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Variables
    private GameObject player;
    private PlayerStats playerStats;
    private GameObject[] enemys;
    private GameObject playerSpawn;
    private static GameManager instance;

    [SerializeField] private GameState gameState;

    public enum GameState
    {
        MainMenu,
        Tutorial,
        HUB,
        LevelOne
    }

    public static GameManager Instance
    {
        get => instance;
        private set
        {

        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        UpdateGameState(gameState);
    }


    public void UpdateGameState(GameState _gameState)
    {
        gameState = _gameState;

        switch(_gameState)
        {
            case GameState.MainMenu:

                break;

            case GameState.Tutorial:

                break;

            case GameState.HUB:
                SetPlayer();

                break;

            case GameState.LevelOne:

                break;
        }
    }

    private void SetPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerStats = player.GetComponent<PlayerStats>();
    }

    //Rescan the enemys walkable area
    private void Rescan()
    {
        AstarPath.active.Scan();
    }

    public PlayerStats PlayerStats
    {
        get => playerStats;
    }
    public GameObject Player
    {
        get => player;
    }
}
