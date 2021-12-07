//Bullet logic script
//Creater: King
//Date: 11/23/21
using UnityEngine;

public class RangeAttack : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float speed;
    private float startheight;

    private void Start()
    {
        startheight = this.transform.position.y;
        Invoke("CleanUp", 5f);
    }
    private void Update()
    {
        //Strange way of offseting the fact that the weapon muzzle has a ton a sway with the animation being used
        this.transform.position = new Vector3(this.transform.position.x, startheight, this.transform.position.z);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    //May need to change this to OnCollisionEnter
    private void OnTriggerEnter(Collider other)
    {
        //Destroy bullet if it hits boader first
        //Not working at the momoent
        if(other.tag == "Boarder")
        {
            CleanUp();
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

    //Allows Invoke to destory the game object
    private void CleanUp()
    {
        Destroy(this.gameObject);
    }

    /// <summary>
    /// Set the damage of the current instance
    /// </summary>
    public int SetDamage
    {
        set => damage = value;
    }
    /// <summary>
    /// Set the speed of the current instance
    /// </summary>
    public float Speed
    {
        set => speed = value;
    }

}
