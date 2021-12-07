//Enemy attack script
//Creater: King
//Date: 11/23/21

using UnityEngine;
using System;
using Pathfinding;

public class EnemyAttack : MonoBehaviour
{
    private EnemyStats enemyStats;
    /// <summary>
    /// **DO NOT MODIFY***
    /// </summary>
    [SerializeField] private AIDestinationSetter pathfinding;
    /// <summary>
    /// ***DO NOT MODIFY***
    /// </summary>
    [SerializeField] private AIPath state;

    // Start is called before the first frame update
    void Start()
    {
        enemyStats = this.GetComponentInParent<EnemyStats>();
    }

    // Update is called once per frame
    void Update()
    {
        //checks to make sure the enemy has reached its target distance from player and is currently pursuing
        if (state.reachedDestination && pathfinding.target != null)
        {
            if(PlayerStats.Instance.Health <= 0)
            {
                //TODO
                //Something cool when the player dies
            }
            else
            {
                Attack((int)UnityEngine.Random.Range(.5f, 1.6f), enemyStats.Ranged);
            }
            enemyStats.Animation.SetFloat("Speed", 0);
        }
        //else if the enemy is currently pursing the player and has not reached its stoping distance
        else if(!state.reachedDestination && pathfinding.target != null)
        {
            enemyStats.Animation.SetFloat("Speed", 1);
        }
    }

    //Function for the enemy to attack the player
    //Will check if the enemy is a melee or ranged attacker
    private void Attack(int _AttackChoice, bool Ranged)
    {
        //Light attack choice
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
        //Heavy attack choice(Not very common)
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
    //Need to add an enemy that uses ranged attacks before implementing
    private void RangedAttack(int _AttackChoice)
    {

    }
    //If the enemy is melee based choose this attack method
    //Damage is being handled inside the animation to allow for acurate delay
    private void MeleeAttack(int _AttackChoice)
    {
        if(_AttackChoice == 1)
        {
            enemyStats.Animation.SetInteger("AttackDamage", 1);
        }
        else if(_AttackChoice == 2)
        {
            enemyStats.Animation.SetInteger("AttackDamage", 2);
        }
        enemyStats.Animation.SetTrigger("Attack");
    }

    /// <summary>
    /// This is the method that will deal damage to the player, is currently being called by the animation clip
    /// </summary>
    /// <param name="_Damage"> The amount of damage to deal to the player</param>
    public void Damage(int _Damage)
    {
        GameManager.Instance.PlayerStats.DamagePlayer(_Damage);
    }
}
