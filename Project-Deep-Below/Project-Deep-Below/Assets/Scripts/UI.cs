//UI logic script
//Creater: King
//Date: 11/24/21

using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] GameObject HUD;
    [SerializeField] GameObject Bag;
    [SerializeField] GameObject Pause;
    [SerializeField] GameObject Death;
    [Space][Space]
    [SerializeField] Image health;
    [SerializeField] GameObject ammo;
    [SerializeField] Text ammoCount;
    [Space]
    [Space]
    [SerializeField] private UIState gameState;

    public enum UIState
    {
        InGame,
        Bag,
        Pause,
        Dead
    }
    private void Start()
    {
        gameState = UIState.InGame;
    }


    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetButtonDown("Pause") && gameState == UIState.InGame)
        {
            PauseGame();
        }
        else if(Input.GetButtonDown("Pause") && gameState == UIState.Pause)
        {
            Resume();
        }
        if (PlayerStats.Instance.Health <= 0)
        {
            gameState = UIState.Dead;
        }

        GetActiveState();
    }
    private void GetActiveState()
    {
        switch(gameState)
        {
            case UIState.InGame:
                HUDStuff();
                break;
            case UIState.Bag:
                break;
            case UIState.Pause:
                break;
            case UIState.Dead:
                break;
        }
    }
    private void HUDStuff()
    {
        health.fillAmount = (float)GameManager.Instance.PlayerStats.Health / (float)GameManager.Instance.PlayerStats.MaxHealth;
        AmmoCounter();
    }
    private void PauseGame()
    {
        Pause.SetActive(true);
        gameState = UIState.Pause;
        Time.timeScale = 0f;
    }
    //Displays the ammo count if a ranged weapon is currently equiped
    private void AmmoCounter()
    {
        if (GameManager.Instance.Player.GetComponentInChildren<RangeCombat>())
        {
            ammo.SetActive(true);
            ammoCount.text =
                GameManager.Instance.Player.GetComponentInChildren<RangeCombat>().Clip.ToString()
                + " / " +
                GameManager.Instance.Player.GetComponentInChildren<RangeCombat>().AmmoCount.ToString();
        }
        else
        {
            ammo.SetActive(false);
        }
    }

    #region Public Functions
    //Quits the application
    public void Quit()
    {
        Application.Quit();
    }
    //Resume application
    public void Resume()
    {
        Pause.SetActive(false);
        HUD.SetActive(true);
        gameState = UIState.InGame;
        Time.timeScale = 1f;
    }

    #endregion

    #region Properties

    public UIState GetMenuState
    {
        get => gameState;
    }

    #endregion
}
