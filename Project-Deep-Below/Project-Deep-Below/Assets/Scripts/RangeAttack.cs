using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttack : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float speed;

    private void Start()
    {
        Invoke("CleanUp", 5f);
    }
    private void Update()
    {
        this.transform.position = new Vector3(this.transform.position.x, .5f, this.transform.position.z);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
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
        _enemy.GetComponent<EnemyStats>().Damage(damage);

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

    public int SetDamage
    {
        set => damage = value;
    }
    public float Speed
    {
        set => speed = value;
    }

}
