//Melee combat script
//Creater: King
//Date: 11/22/21
using System;
using UnityEngine;

public class MeleeCombat : MonoBehaviour
{
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private Collider[] enemiesHit;

    [SerializeField] private float attackRange = 1.0f;
    [SerializeField] float attackRate = 2.0f;
    float attackBuffer = 0f;
    [SerializeField] float heavyAttackRate = 4f;
    float heavyAttackBuffer = 0f;

    private float damage = 0;

    private void Update()
    {
        enemiesHit = Physics.OverlapSphere(transform.position, attackRange, enemyLayer);
        //Delay for base attack
        if(Time.time >= attackBuffer && Input.GetButtonDown("Fire1"))
        {
            Attack(1);
            attackBuffer = Time.time + attackRate;
        }
        //Delay for heavy attack
        else if(Time.time >= heavyAttackBuffer && Input.GetButtonDown("Fire2"))
        {
            Attack(2);
            heavyAttackBuffer = Time.time + heavyAttackRate;
        }
    }

    private void Attack(int _AttackChoice)
    {
        foreach(Collider enemy in enemiesHit)
        {
            if(_AttackChoice == 1)
            {
                damage = (float)Math.Round((UnityEngine.Random.Range(1.00f, 2.00f) * GameManager.Instance.PlayerStats.BaseDamage), 1);
            }
            else if(_AttackChoice == 2)
            {
                damage = (float)Math.Round((UnityEngine.Random.Range(1.00f, 2.00f) * GameManager.Instance.PlayerStats.HeavyDamage), 1);
            }
            //enemy.GetComponent<EnemyStats>().Damage(damage);
            Debug.Log(enemy.name + " hit for " + damage);
        }
    }
}
