//Player stats script
//Creater: King
//Date: 11/21/21

using UnityEngine;
using System;

public class PlayerStats : MonoBehaviour
{
    #region Variables
    private static PlayerStats instance;
    [SerializeField] private Animator animator;
    [SerializeField] private RuntimeAnimatorController unarmedController;
    [Space] [Space]
    [SerializeField] private int health;
    [SerializeField] private int maxHealth;
    [Space] [Space]
    [SerializeField] private float baseSpeed = 10f;
    [Space] [Space]
    [SerializeField] private float rollSpeed;
    [SerializeField] private float rollDistance;
    [SerializeField] private float rollRate = 1f;
    [Space] [Space]
    [SerializeField] private int baseDamage;
    [SerializeField] private int heavyDamage;
    [SerializeField] private bool penetrateDamage;
    [Space] [Space]
    [SerializeField] private int playerLevel = 1;
    [SerializeField] private float playerEXP = 0;
    [SerializeField] private float playerEXPMultipliyer = 100;
    [Space] [Space]
    [SerializeField] private int levelUpHealth;
    [Space]
    [SerializeField] private GameObject pickUpTarget;
    #endregion

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public static PlayerStats Instance
    {
        get => instance;
    }

    /// <summary>
    /// Method that will damage the player
    /// </summary>
    /// <param name="_Damage">The amount of damage to deal in INT</param>
    public void DamagePlayer(int _Damage)
    {
        if (health > 0)
        {
            health = health > 0 ? health -= _Damage : 0;
            animator.SetTrigger("Damage");

            if (health <= 0)
            {
                Death();
            }
        }
    }
    /// <summary>
    /// Method to easily heal the player
    /// <para>Will not allow for the player to go above max health</para>
    /// </summary>
    /// <param name="_Health">The amount of health to return to player</param>
    public void HealPlayer(int _Health)
    {
        health = (health + _Health) <= maxHealth ? health = +_Health : maxHealth;
    }
    /// <summary>
    /// Add experience points to the player
    /// <para>Will also call the level up Method if experiecne is greater than level requirment</para>
    /// </summary>
    /// <param name="_Exp">Amount of EXP to add in FLOAT</param>
    public void AddExp(float _Exp)
    {
        playerEXP += _Exp;

        if(playerEXP >= (playerLevel * playerEXPMultipliyer))
        {
            LevelUp();
        }
    }
    //Level Up the player
    private void LevelUp()
    {
        playerLevel++;
        //increase the EXP* to make the next level harder
        playerEXPMultipliyer = (float)Math.Round(playerEXPMultipliyer * 1.2f, 2);

        //TODO
        //Will change this in the future to allow for more varied experience
        //Possibly give the player a choose of what stat to upgrade
        maxHealth += levelUpHealth;
    }
    //Diable the compopnents on the player
    private void Death()
    {
        animator.SetTrigger("Death");
        this.GetComponent<PlayerMovment>().enabled = false;
        this.GetComponent<CharacterController>().enabled = false;
        this.GetComponent<PlayerInventory>().enabled = false;
        if(this.GetComponentInChildren<MeleeCombat>())
        {
            this.GetComponentInChildren<MeleeCombat>().enabled = false;
        }
        else if(this.GetComponentInChildren<RangeCombat>())
        {
            this.GetComponentInChildren<RangeCombat>().enabled = false;
        }
    }

    #region Properties
    /// <summary>
    /// Returns the base speed of player
    /// </summary>
    // Note only called base due to past experement with sprinting
    public float BaseSpeed
    {
        get => baseSpeed;
    }
    /// <summary>
    /// Returns the base damage of player
    /// <para>Allows setting of base damage</para>
    /// </summary>
    public int BaseDamage
    {
        get => baseDamage;
        set => baseDamage = value;
    }
    /// <summary>
    /// Returns the heavy damage of player
    /// <para>Allows setting of heavy damage</para>
    /// </summary>
    public int HeavyDamage
    {
        get => heavyDamage;
        set => heavyDamage = value;
    }
    /// <summary>
    /// Returns and sets the health of the player
    /// </summary>
    public int Health
    {
        get => health;
        set => health = value;
    }
    /// <summary>
    /// Returns the max health of the player
    /// </summary>
    public int MaxHealth
    {
        get => maxHealth;
    }
    /// <summary>
    /// Gets and sets weather the current weapon has penetrate damage
    /// </summary>
    public bool Penetrate
    {
        get => penetrateDamage;
        set => penetrateDamage = value;
    }
    /// <summary>
    /// returns the roll speed of player
    /// </summary>
    public float Roll
    {
        get => rollSpeed;
    }
    /// <summary>
    /// Returns the roll distance of the player
    /// </summary>
    public float RollDistance
    {
        get => rollDistance;
    }
    /// <summary>
    /// Returns the rate at which player can roll
    /// </summary>
    public float RollRate
    {
        get => rollRate;
    }
    //Returns animator 
    public Animator Animation
    {
        get => animator;
        set => animator = value;
    }
    /// <summary>
    /// Returns the unarmed animator for the player
    /// </summary>
    public RuntimeAnimatorController Unarmed
    {
        get => unarmedController;
    }
    /// <summary>
    /// Returns the location to parent weapons to(Right hand)
    /// <para>Sorry lefties</para>
    /// </summary>
    public GameObject PickUpTarget
    {
        get => pickUpTarget;
    }

    #endregion

}
