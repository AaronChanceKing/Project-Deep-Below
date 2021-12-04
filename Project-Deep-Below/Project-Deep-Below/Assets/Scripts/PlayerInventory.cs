//Script for player inventory
//Creater: king
//Date: 12/3/21
using UnityEngine;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private List<GameObject> inventory = new List<GameObject>();
    [SerializeField] int inventoryMax = 2;
    [SerializeField] GameObject currentWeapon;
    private Object[] preFabs;
    private int currentInventory = 0;

    // Start is called before the first frame update
    void Start()
    {
        preFabs = Resources.LoadAll("PrefabWeapons", typeof(GameObject));
    }

    // Update is called once per frame
    void Update()
    {
        GetCurrentWeapon();
        float scrollWheel = Input.GetAxis("Swap");

        if (Input.GetButtonDown("Drop") && currentWeapon != null)
        {
            DropWeapon();
        }

        if(scrollWheel >= 1)
        {
            Equip(1);
        }
        else if(scrollWheel <= -1)
        {
            Equip(-1);
        }
    }

    //Get the current weapon equiped
    private void GetCurrentWeapon()
    {
        if (this.GetComponentInChildren<MeleeCombat>())
        {
            currentWeapon = this.gameObject.GetComponentInChildren<MeleeCombat>().gameObject;
        }
        else if(this.GetComponentInChildren<RangeCombat>())
        {
            currentWeapon = this.gameObject.GetComponentInChildren<RangeCombat>().gameObject;
        }
        else
        {
            currentWeapon = null;
        }
    }
    //Drop equiped weapon
    private void DropWeapon()
    {
        RemoveFromInventory(currentWeapon);
        Destroy(currentWeapon);
        Equip(0);
    }
    //Add item to inventory
    public void AddToInventory(string _WeaponName)
    {
        if(inventory.Count < inventoryMax)
        {
            foreach(GameObject weapon in preFabs)
            {
                if(weapon.name == _WeaponName)
                {
                    inventory.Add(weapon);
                }
            }
        }
        if(currentWeapon == null)
        {
            Equip(0);
        }
    }
    //Remove item from inventory
    private void RemoveFromInventory(GameObject _weapon)
    {
        foreach(GameObject weapon in preFabs)
        {
            if(weapon.name == _weapon.name)
            {
                inventory.Remove(weapon);
                currentInventory = 0;
            }
        }
    }
    //Equip weapon
    private void Equip(int _InventoryNumber)
    {
        switch(_InventoryNumber)
        {
            case 0:
                if (inventory.Count > _InventoryNumber)
                {
                    GameObject item = Instantiate(inventory[currentInventory]);
                    item.name = inventory[currentInventory].name;
                    item.transform.localScale = new Vector3(1, 1, 1);
                }
                break;
            case 1:
                if(inventory.Count > (currentInventory + _InventoryNumber))
                {
                    Destroy(currentWeapon);
                    currentInventory++;
                    GameObject item = Instantiate(inventory[currentInventory]);
                    item.name = inventory[currentInventory].name;
                    item.transform.localScale = new Vector3(1, 1, 1);
                }
                break;
            case -1:
                if(currentInventory > 0)
                {
                    Destroy(currentWeapon);
                    currentInventory--;
                    GameObject item = Instantiate(inventory[currentInventory]);
                    item.name = inventory[currentInventory].name;
                    item.transform.localScale = new Vector3(1, 1, 1);
                }
                break;
        }
    }
}
