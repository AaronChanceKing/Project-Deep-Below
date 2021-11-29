//Melee combat script
//Creater: King
//Date: 11/22/21
using System;
using UnityEngine;

public class MeleeCombat : MonoBehaviour
{
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private Collider[] enemiesHit;

    [SerializeField] Transform attackPoint;
    [SerializeField] private float attackRange = 1.0f;
    [SerializeField] float attackRate = 2.0f;
    float attackBuffer = 0f;
    [SerializeField] float heavyAttackRate = 4f;
    float heavyAttackBuffer = 0f;

    private int damage = 0;

    private void Update()
    {
        enemiesHit = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayer);
        //Delay for base attack
        if(Time.time >= attackBuffer && Input.GetButtonDown("Fire1"))
        {
            Attack(1);
            GameManager.Instance.PlayerStats.Animation.SetInteger("AttackDamage", 1);
            attackBuffer = Time.time + attackRate;
        }
        //Delay for heavy attack
        else if(Time.time >= heavyAttackBuffer && Input.GetButtonDown("Fire2"))
        {
            Attack(2);
            GameManager.Instance.PlayerStats.Animation.SetInteger("AttackDamage", 2);
            heavyAttackBuffer = Time.time + heavyAttackRate;
        }
    }

    private void Attack(int _AttackChoice)
    {
        foreach(Collider enemy in enemiesHit)
        {
            if(_AttackChoice == 1)
            {
                damage = (int)((float)Math.Round((UnityEngine.Random.Range(1.00f, 2.00f) * GameManager.Instance.PlayerStats.BaseDamage), 1) * 10);
            }
            else if(_AttackChoice == 2)
            {
                damage = (int)((float)Math.Round((UnityEngine.Random.Range(1.00f, 2.00f) * GameManager.Instance.PlayerStats.HeavyDamage), 1) * 10);
            }

            enemy.GetComponent<EnemyStats>().Damage(damage);
            Debug.Log(enemy.name + " hit for " + damage);
        }
        GameManager.Instance.PlayerStats.Animation.SetTrigger("Attack");
    }
}
