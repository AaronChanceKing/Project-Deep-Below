//Player stats script
//Creater: King
//Date: 11/21/21

using UnityEngine;
using System;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private RuntimeAnimatorController shootingController;
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
    [Range (.05f, .30f)]
    [SerializeField] private float rollDistance;
    [SerializeField] private float rollRate = 1f;
    [Space] [Space]
    [SerializeField] private int baseDamage;
    [SerializeField] private int heavyDamage;
    [SerializeField] private bool penetrateDamage;
    [Space] [Space]
    private int playerLevel = 1;
    private float playerEXP = 0;
    private float playerEXPMultipliyer = 100;
    [Space] [Space]
    [SerializeField] private float levelUpStamina;
    [SerializeField] private int levelUpHealth;

    private void Update()
    {
        if(this.GetComponentInChildren<RangeCombat>())
        {
            animator.runtimeAnimatorController = shootingController;
        }
        else
        {
            animator.runtimeAnimatorController = unarmedController;
        }
    }
    //Damage the player
    //Takes in INT
    public void DamagePlayer(int _Damage)
    {
        health = health > 0 ? health -= _Damage : 0;

        if(health <= 0)
        {
            Death();
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
    }

    #endregion

}
