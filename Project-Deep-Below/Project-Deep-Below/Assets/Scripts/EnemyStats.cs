//Enemy stats script
//Creater: King
//Date: 11/23/21
using System;
using UnityEngine;
using Pathfinding;
using System.Collections;

public class EnemyStats : MonoBehaviour
{
    #region Variables
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
    #endregion

    private void Start()
    {
        StartCoroutine(LateStart());
    }

    //Allows for method to be called after start and only once
    IEnumerator LateStart()
    {
        yield return new WaitForSeconds(.1f);
        //This is currently what is keeping the player collider from causeing the enemy to spin off into the distance
        Physics.IgnoreCollision(this.GetComponent<CapsuleCollider>(), GameManager.Instance.Player.GetComponent<CharacterController>(), true);
    }
    /// <summary>
    /// Method that will damage the enemy that the script is attacked to
    /// </summary>
    /// <param name="_Damage">The amount of damage to deal to the target</param>
    public void Damage(int _Damage)
    {
        health = health > 0 ? health -= _Damage : 0;
        animator.SetTrigger("Damage");

        if(health <= 0)
        {
            Death();
        }
    }

    //Helper mehtod to get the amount of exp to reward the player
    private float GetEXP()
    {
        if(level > 1)
        {
            expMultipliyer = (float)Math.Round(expMultipliyer * 1.2f, 2) * level;
        }

        return baseExp * expMultipliyer;
    }

    //Disable components on the enemy upon death
    //Small wait timer for effect before compleately destroying
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

    //Just a destroy method to allow Invoke method to be used
    private void Destroy()
    {
        Destroy(this.gameObject);
    }

    #region Properties
    /// <summary>
    ///Sets the enemy base damage INT
    ///<para>Returns the current damage of the enemy INT</para>
    /// </summary>
    public int EnemyBaseDamage
    {
        get => baseDamage;

        set => baseDamage = value;
    }
    /// <summary>
    ///Sets the enemy heavy damage INT
    ///<para>Returns the current heavy damage of the enemy INT</para>
    /// </summary>
    public int EnemyHeavyDamage
    {
        get => heavyDamage;

        set => heavyDamage = value;
    }
    /// <summary>
    ///Set to if the enemy is ranged or melee
    ///<para>Returns the current attack type BOOL</para>
    /// </summary>
    public bool Ranged
    {
        get => ranged;

        set => ranged = value;
    }
    /// <summary>
    /// Set the range of attack melee only
    /// <para>Returns the range of attack</para>
    /// </summary>
    public float AttackRange
    {
        get => attackRange;

        set => attackRange = value;
    }
    /// <summary>
    /// Set the base attack timing
    /// <para>Returns the current timming of the base attack</para>
    /// </summary>
    public float BaseAttackBuffer
    {
        get => attackBuffer;

        set => attackBuffer = value;
    }
    /// <summary>
    /// Set the base attack rate
    /// <para>Returns the current rate of the base attack</para>
    /// </summary>
    public float BaseAttackRate
    {
        get => attackRate;

        set => attackRate = value;
    }
    /// <summary>
    /// Set the heavy attack timing
    /// <para>Returns the current timming of the heavy attack</para>
    /// </summary>
    public float HeavyAttackBuffer
    {
        get => heavyAttackBuffer;

        set => heavyAttackBuffer = value;
    }
    /// <summary>
    /// Set the base attack rate
    /// <para>Returns the current rate of the base attack</para>
    /// </summary>
    public float HeavyAttackRate
    {
        get => heavyAttackRate;

        set => heavyAttackRate = value;
    }
    /// <summary>
    /// Returns the animator attached to this game object
    /// </summary>
    public Animator Animation
    {
        get => animator;
    }
    #endregion
}
