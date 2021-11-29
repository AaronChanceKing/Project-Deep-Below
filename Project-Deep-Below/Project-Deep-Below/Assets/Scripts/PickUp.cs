//Pick Up logic
//Creater: King
//Date: 11/22/21

using UnityEngine;

public class PickUp : MonoBehaviour
{
    private GameObject parent;
    [SerializeField] private Vector3 location;
    [SerializeField] private Vector3 rotation;
    [SerializeField] private bool melee;

    public void OnTriggerEnter(Collider _other)
    {
        if(_other.tag == "Player")
        {
            parent = GameManager.Instance.PlayerStats.PickUpTarget;
            AddItem(_other.gameObject);
        }
    }

    private void AddItem(GameObject _player)
    {
        Destroy(this.GetComponentInChildren<ParticleSystem>());
        Destroy(this.GetComponentInChildren<SpriteRenderer>());

        //If Player is already holding a weapon
        if(_player.GetComponentInChildren<PickUp>())
        {
            AddToInventory();
        }
        //Player has no weapon currently equiped
        else
        {
            this.GetComponent<UpgradeScript>().UpgradePlayer(_player);

            //Add Logic for picking up
            transform.parent = parent.transform;
            transform.localPosition = location;
            transform.localRotation = Quaternion.Euler(rotation);

            if(melee)
            {
                this.GetComponent<MeleeCombat>().enabled = true;
            }
            else
            {
                this.GetComponent<RangeCombat>().enabled = true;
            }
        }

    }

    //TODO
    private void AddToInventory()
    {

    }
}
