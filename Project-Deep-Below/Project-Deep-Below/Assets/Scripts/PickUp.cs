//Logic for picking up weapons
//Creater: King
//Date: 11/22/21

using UnityEngine;

public class PickUp : MonoBehaviour
{
    public void OnTriggerEnter(Collider _other)
    {
        if(_other.tag == "Player")
        {
            Add();
            //Not really adding this instance of the object as it is a dummy item
            Destroy(this.gameObject);
        }
    }

    //Add item to player inventory and equip it if no weapon is active
    private void Add()
    {
        GameManager.Instance.Player.GetComponent<PlayerInventory>().AddToInventory(this.gameObject.name);
    }
}
