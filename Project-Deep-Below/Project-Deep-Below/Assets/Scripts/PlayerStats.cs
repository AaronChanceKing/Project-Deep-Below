//Player stats script
//Creater: King
//Date: 11/21/21

using UnityEngine;
using System;

public class PlayerStats : MonoBehaviour
{
    private static PlayerStats instance;
    [SerializeField] private Animator animator;
    [SerializeField] private RuntimeAnimatorController unarmedController;
    [Space] [Space]
    [SerializeField] private float stamina = 5;
    [SerializeField] private float maxStamina = 5;
    [SerializeField] private float staminaDrain = .01f;
    [Space] [Space]
    [SerializeField] private int health;
    [SerializeField] private int maxHealth;
    [Space] [Space]
    [SerializeField] private float baseSpeed = 10f;
    [SerializeField] private float sprintSpeed = 16f;
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
    [SerializeField] private float levelUpStamina;
    [SerializeField] private int levelUpHealth;
    [Space]
    [SerializeField] private GameObject pickUpTarget;

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

    //Damage the player
    //Takes in INT
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
    //Heal the player
    //Takes in INT
    public void HealPlayer(int _Health)
    {
        health = (health + _Health) <= maxHealth ? health = +_Health : maxHealth;
    }
    //Add experience to the player
    //Takes in FLOAT
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
        playerEXPMultipliyer = (float)Math.Round(playerEXPMultipliyer * 1.2f, 2);

        maxHealth += levelUpHealth;
        maxStamina += levelUpStamina;
    }
    //TODO
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
    //Retruns float for base speed
    public float BaseSpeed
    {
        get => baseSpeed;
    }
    //Returns float for sprint speed
    public float SprintSpeed
    {
        get => sprintSpeed;
    }
    //Returns the stamina of player
    public float Stamina
    {
        get => stamina;
        set => stamina = value;
    }
    public float MaxStamina
    {
        get => maxStamina;
    }
    //Returns the stamina drain of the player
    public float StaminaDrain
    {
        get => staminaDrain;
    }
    //Returns the base damage of player
    //Allows setting of player damage
    public int BaseDamage
    {
        get => baseDamage;
        set => baseDamage = value;
    }
    //Returns the heavy damage of player
    //Allows setting of player heavy damage
    public int HeavyDamage
    {
        get => heavyDamage;
        set => heavyDamage = value;
    }
    //Returns player health
    //Allows setting of player health
    public int Health
    {
        get => health;
        set => health = value;
    }
    //Returns player max health
    //Allows setting of player max health
    public int MaxHealth
    {
        get => maxHealth;
    }
    //Set if player damage will penetrate enemys
    public bool Penetrate
    {
        get => penetrateDamage;
        set => penetrateDamage = value;
    }
    //Returns the roll Distance of player
    public float Roll
    {
        get => rollSpeed;
    }
    //Retruns the roll time of player
    public float RollDistance
    {
        get => rollDistance;
    }
    //Returns the rolling rate
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
    //Returns unarmed animator
    public RuntimeAnimatorController Unarmed
    {
        get => unarmedController;
    }
    //Returns the location to parent weapons to
    public GameObject PickUpTarget
    {
        get => pickUpTarget;
    }

    #endregion

}
