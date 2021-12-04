//Enemy stats script
//Creater: King
//Date: 11/23/21
using System;
using UnityEngine;
using Pathfinding;
using System.Collections;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] private float health;
    [Space]
    [Space]
    [SerializeField] private int baseDamage;
    [SerializeField] private int heavyDamage;
    [SerializeField] private bool ranged = false;
    [Space]
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private float attackRate = 2.0f;
    private float attackBuffer = 0;
    [SerializeField] private float heavyAttackRate = 4f;
    private float heavyAttackBuffer;
    [Space]
    [Space]
    [SerializeField] private int level = 1;
    [SerializeField] private float baseExp;
    [SerializeField] private float expMultipliyer = 1;
    [SerializeField] private Animator animator;

    private void Start()
    {
        StartCoroutine(LateStart());
    }

    IEnumerator LateStart()
    {
        yield return new WaitForSeconds(.1f);
        Physics.IgnoreCollision(this.GetComponent<CapsuleCollider>(), GameManager.Instance.Player.GetComponent<CharacterController>(), true);
    }
    public void Damage(int _Damage)
    {
        health = health > 0 ? health -= _Damage : 0;
        animator.SetTrigger("Damage");

        if(health <= 0)
        {
            Death();
        }
    }

    private float GetEXP()
    {
        if(level > 1)
        {
            expMultipliyer = (float)Math.Round(expMultipliyer * 1.2f, 2) * level;
        }

        return baseExp * expMultipliyer;
    }

    private void Death()
    {
        GameManager.Instance.PlayerStats.AddExp(GetEXP());
        animator.SetTrigger("Death");
        this.GetComponent<AIPath>().enabled = false;
        this.GetComponent<CapsuleCollider>().enabled = false;
        this.GetComponentInChildren<EnemyAttack>().enabled = false;
        this.GetComponentInChildren<EnemyIdle>().enabled = false;
        this.GetComponent<Rigidbody>().isKinematic = true;

        Invoke("Destroy", 10f);
    }

    private void Destroy()
    {
        Destroy(this.gameObject);
    }

    //Sets the enemy base damage INT
    //Returns the current damage of the enemy INT
    public int EnemyBaseDamage
    {
        get => baseDamage;

        set => baseDamage = value;
    }
    //Sets the enemy heavy damage INT
    //Returns the current heavy damage of the enemy INT
    public int EnemyHeavyDamage
    {
        get => heavyDamage;

        set => heavyDamage = value;
    }
    //Sets if the enemy is ranged
    //Retruns if the enemy is ranged
    public bool Ranged
    {
        get => ranged;

        set => ranged = value;
    }

    public float AttackRange
    {
        get => attackRange;

        set => attackRange = value;
    }
    public float BaseAttackBuffer
    {
        get => attackBuffer;

        set => attackBuffer = value;
    }
    public float BaseAttackRate
    {
        get => attackRate;

        set => attackRate = value;
    }
    public float HeavyAttackBuffer
    {
        get => heavyAttackBuffer;

        set => heavyAttackBuffer = value;
    }
    public float HeavyAttackRate
    {
        get => heavyAttackRate;

        set => heavyAttackRate = value;
    }
    public Animator Animation
    {
        get => animator;
    }

}
