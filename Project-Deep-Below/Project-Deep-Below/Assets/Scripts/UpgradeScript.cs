//Script to upgrade the player stats 
//Creater: King
//Date: 12/1/21
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeScript : MonoBehaviour
{
    [SerializeField] private int baseDamage;
    [SerializeField] private int heavyDamage;
    [SerializeField] private bool penetrate;

    /// <summary>
    /// Set the player damage and if the weapon has penetrate damage
    /// </summary>
    /// <param name="_player">The current instance of the player(GameManager.Instance.Player)</param>
  public void UpgradePlayer(GameObject _player)
    {
        _player.GetComponent<PlayerStats>().BaseDamage = baseDamage;
        _player.GetComponent<PlayerStats>().HeavyDamage = heavyDamage;
        _player.GetComponent<PlayerStats>().Penetrate = penetrate;
    }
}
