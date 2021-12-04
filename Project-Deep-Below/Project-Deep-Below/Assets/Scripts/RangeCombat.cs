//Ranged combat script
//Creater: King
//Date: 11/22/21

using UnityEngine;
using System;

public class RangeCombat : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject muzzel;
    [SerializeField] float speed;
    [SerializeField] int ammoCount;
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
        if (clip > 0)
        {
            if (Time.time >= attackBuffer && Input.GetButton("Fire1"))
            {
                Attack(1);
                attackBuffer = Time.time + attackRate;
            }
            if (Time.time >= heavyAttackBuffer && Input.GetButtonDown("Fire2"))
            {
                Attack(2);
                heavyAttackBuffer = Time.time + heavyAttackRate;
            }
        }
        //Auto reload
        else if (ammoCount > 0 && clip == 0)
        {
            Reload();
            attackBuffer = Time.time + reloadTime;
            heavyAttackBuffer = Time.time + reloadTime;
        }

        //Button reload
        if(Input.GetButtonDown("Reload") && clip < clipMax && ammoCount > 0)
        {
            Reload();
            attackBuffer = Time.time + reloadTime;
            heavyAttackBuffer = Time.time + reloadTime;
        }
    }

    private void Attack(int _AttackChoice)
    {
        //Set speed of the bullet
        bullet.GetComponent<RangeAttack>().Speed = speed;
        GameManager.Instance.PlayerStats.Animation.SetTrigger("Shoot");

        //Set damage of the bullet
        if (_AttackChoice == 1)
        {
            bullet.GetComponent<RangeAttack>().SetDamage = (int)((float)Math.Round((UnityEngine.Random.Range(1.0f, 2.0f) * GameManager.Instance.PlayerStats.BaseDamage), 1) * 10);
        }
        else if(_AttackChoice == 2 && clip >= 2)
        {
            bullet.GetComponent<RangeAttack>().SetDamage = (int)((float)Math.Round((UnityEngine.Random.Range(1.5f, 2.0f) * GameManager.Instance.PlayerStats.HeavyDamage), 1) * 10);
            clip--;
        }
        //If you dont have enough bullets for heavy ranged damage will be cut down
        else
        {
            bullet.GetComponent<RangeAttack>().SetDamage = (int)((float)Math.Round((UnityEngine.Random.Range(0.0f, 1.5f) * GameManager.Instance.PlayerStats.BaseDamage), 1) * 10);
        }

        Instantiate(bullet, muzzel.transform.position, this.transform.rotation);
        clip--;
    }

    private void Reload()
    {
        clip = ammoCount >= clipMax ? clipMax : ammoCount;
        ammoCount -= clip;
        GameManager.Instance.PlayerStats.Animation.ResetTrigger("Shoot");
        GameManager.Instance.PlayerStats.Animation.SetTrigger("Reload");
    }

    public int Clip
    {
        get => clip;
    }
    public int ClipMax
    {
        get => ammoCount;
    }
}
