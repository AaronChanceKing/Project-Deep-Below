//Pick Up logic
//Creater: King
//Date: 11/22/21

using UnityEngine;

public class PickUp : MonoBehaviour
{
   public void OnTriggerEnter(Collider _other)
    {
        if(_other.tag == "Player")
        {
            AddItem(_other.gameObject);
        }
    }

    private void AddItem(GameObject _player)
    {
        Destroy(this.GetComponentInChildren<ParticleSystem>());

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
            transform.parent = _player.transform;
            transform.localPosition = new Vector2(1, 0);
            transform.localRotation = Quaternion.Euler(new Vector3(0, 0, -13));
        }
    }

    //TODO
    private void AddToInventory()
    {

    }
}
