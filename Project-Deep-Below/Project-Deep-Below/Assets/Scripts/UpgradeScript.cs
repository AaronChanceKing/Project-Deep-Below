using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeScript : MonoBehaviour
{
    [SerializeField] private int baseDamage;
    [SerializeField] private int heavyDamage;

  public void UpgradePlayer(GameObject _player)
    {
        _player.GetComponent<PlayerStats>().BaseDamage = baseDamage;
        _player.GetComponent<PlayerStats>().HeavyDamage = heavyDamage;
    }
}
