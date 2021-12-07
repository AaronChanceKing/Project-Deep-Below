//Game Manager script
//Creater: King
//Date: 11/22/21

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Variables
    public GameObject player;
    public PlayerStats playerStats;

    //May not need this
    private GameObject[] enemys;
    private GameObject playerSpawn;
    private static GameManager instance;

    /// <summary>
    /// An enum for the current game state
    /// </summary>
    [SerializeField] private GameState gameState;

    public enum GameState
    {
        MainMenu,
        Tutorial,
        HUB,
        InGame
    }

    public static GameManager Instance { get => instance; }

    //Method that is called before start
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

    /// <summary>
    /// This will update the game state allowing for diffrent logic to be called depending on what part of the game is being played
    /// </summary>
    /// <param name="_gameState">What state is the game in/Going into</param>
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
                //This is what the GM is set to in the testing scene
                SetPlayer();

                break;

            case GameState.InGame:

                break;
        }
    }
    //Find the current instance of the player
    private void SetPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerStats = PlayerStats.Instance;
    }

    //Rescan the enemys walkable area upon load
    private void Rescan()
    {
        AstarPath.active.Scan();
    }

    public GameObject Player
    {
        get => player;
    }

    public PlayerStats PlayerStats
    {
        get => playerStats;
    }
}
