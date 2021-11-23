//Ranged combat script
//Creater: King
//Date: 11/22/21

using UnityEngine;
using System;

public class RangeCombat : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] float speed;
    [SerializeField] int clip;
    private int clipMax;
    [SerializeField] float attackRate = 1.0f;
    [SerializeField] float heavyAttackRate = 4.0f;
    [SerializeField] float reloadTime = 4.0f;
    float attackBuffer = 0f;
    float heavyAttackBuffer = 0f;

    private void Start()
    {
        clipMax = clip;
    }
    private void Update()
    {
        if(Time.time >= attackBuffer && Input.GetButtonDown("Fire1"))
        {
            Attack(1);
            attackBuffer = Time.time + attackRate;
        }
        else if(Time.time >= heavyAttackBuffer && Input.GetButtonDown("Fire2"))
        {
            Attack(2);
            heavyAttackBuffer = Time.time + heavyAttackRate;
        }

        if(Input.GetButtonDown("Reload"))
        {
            Reload();
        }
    }

    private void Attack(int _AttackChoice)
    {
        //Set speed of the bullet
        bullet.GetComponent<RangeAttack>().Speed = speed;

        //Set damage of the bullet
        if (_AttackChoice == 1)
        {
            bullet.GetComponent<RangeAttack>().SetDamage = (float)Math.Round((UnityEngine.Random.Range(1.00f, 2.00f) * GameManager.Instance.PlayerStats.BaseDamage), 1);
        }
        else if(_AttackChoice == 2 && clip >= 2)
        {
            bullet.GetComponent<RangeAttack>().SetDamage = (float)Math.Round((UnityEngine.Random.Range(1.00f, 2.00f) * GameManager.Instance.PlayerStats.HeavyDamage), 1);
            clip--;
        }
        //If you dont have enough bullets for heavy ranged damage will be cut down
        else
        {
            bullet.GetComponent<RangeAttack>().SetDamage = (float)Math.Round((UnityEngine.Random.Range(0.00f, 1.00f) * GameManager.Instance.PlayerStats.BaseDamage), 1);
        }

        Instantiate(bullet, this.transform.position, this.transform.rotation);
        clip--;
        //If clip becomes 0 auto reload
        if (clip == 0)
        {
            Reload();
        }
    }

    private void Reload()
    {
        attackBuffer = Time.time + reloadTime;
        heavyAttackBuffer = Time.time + reloadTime;

        clip = clipMax;
    }
}
