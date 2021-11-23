using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttack : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float speed;

    private void Start()
    {
        Invoke("CleanUp", 5f);
    }
    private void Update()
    {
        transform.Translate((Vector3.right * speed * Time.deltaTime));
    }
    private void OnTriggerEnter(Collider other)
    {
        //Destroy bullet if it hits boader first
        if(other.tag == "Boarder")
        {
            Destroy(this.gameObject);
        }
        else if(other.tag == "Enemy")
        {
            Damage(other);
        }
    }

    private void Damage(Collider _enemy)
    {
        //_enemy.GetComponent<EnemyStats>().Damage(damage);

        //Penetrate damage goes through enemys till it hits a wall
        if(!GameManager.Instance.PlayerStats.Penetrate)
        {
            CleanUp();
        }

        Debug.Log(_enemy.name + " hit for " + damage);
    }

    private void CleanUp()
    {
        Destroy(this.gameObject);
    }

    public float SetDamage
    {
        set => damage = value;
    }
    public float Speed
    {
        set => speed = value;
    }

}
