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
            AddToInventory();
            Destroy(this.gameObject);
        }
    }

    //Add item to player inventory and equip it if no weapon is active
    private void AddToInventory()
    {
        GameManager.Instance.Player.GetComponent<PlayerInventory>().AddToInventory(this.gameObject.name);
    }
}
