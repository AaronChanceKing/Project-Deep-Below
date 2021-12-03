//Enemy attack script
//Creater: King
//Date: 11/23/21

using UnityEngine;
using System;
using Pathfinding;

public class EnemyAttack : MonoBehaviour
{
    private EnemyStats enemyStats;
    [SerializeField] private AIDestinationSetter pathfinding;
    [SerializeField] private AIPath state;

    // Start is called before the first frame update
    void Start()
    {
        enemyStats = this.GetComponentInParent<EnemyStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (state.reachedDestination && pathfinding.target != null)
        {
            if(PlayerStats.Instance.Health <= 0)
            {

            }
            else
            {
                Attack((int)UnityEngine.Random.Range(.5f, 1.6f), enemyStats.Ranged);
            }

            enemyStats.Animation.SetFloat("Speed", 0);
        }
        else if(!state.reachedDestination && pathfinding.target != null)
        {
            enemyStats.Animation.SetFloat("Speed", 1);
        }
    }
    private void Attack(int _AttackChoice, bool Ranged)
    {
        if(_AttackChoice == 1 && Time.time >= enemyStats.BaseAttackBuffer)
        {
            enemyStats.BaseAttackBuffer = Time.time + enemyStats.BaseAttackRate;
            if (Ranged)
            {
                RangedAttack(1);
            }
            else
            {
                MeleeAttack(1);
            }
        }
        else if(_AttackChoice == 2 && Time.time >= enemyStats.HeavyAttackBuffer)
        {
            enemyStats.HeavyAttackBuffer = Time.time + enemyStats.HeavyAttackRate;
            if (Ranged)
            {
                RangedAttack(2);
            }
            else
            {
                MeleeAttack(2);
            }
        }

    }
    //TODO
    private void RangedAttack(int _AttackChoice)
    {

    }
    //If the enemy is melee based chooce this attack method
    private void MeleeAttack(int _AttackChoice)
    {
        if(_AttackChoice == 1)
        {
            //Damage(enemyStats.EnemyBaseDamage);
            enemyStats.Animation.SetInteger("AttackDamage", 1);
        }
        else if(_AttackChoice == 2)
        {
            //Damage(enemyStats.EnemyHeavyDamage);
            enemyStats.Animation.SetInteger("AttackDamage", 2);
        }
        enemyStats.Animation.SetTrigger("Attack");
    }

    public void Damage(int _Damage)
    {
        GameManager.Instance.PlayerStats.DamagePlayer(_Damage);
    }
}
