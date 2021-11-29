//UI logic script
//Creater: King
//Date: 11/24/21

using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] Image health;
    [SerializeField] GameObject ammo;
    [SerializeField] Text ammoCount;

    // Update is called once per frame
    void Update()
    {
        health.fillAmount = (float)GameManager.Instance.PlayerStats.Health / (float)GameManager.Instance.PlayerStats.MaxHealth;

        if(GameManager.Instance.Player.GetComponentInChildren<RangeCombat>())
        {
            ammo.SetActive(true);
            ammoCount.text =
                GameManager.Instance.Player.GetComponentInChildren<RangeCombat>().Clip.ToString() 
                + " / " +
                GameManager.Instance.Player.GetComponentInChildren<RangeCombat>().ClipMax.ToString();
        }
        else
        {
            ammo.SetActive(false);
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
