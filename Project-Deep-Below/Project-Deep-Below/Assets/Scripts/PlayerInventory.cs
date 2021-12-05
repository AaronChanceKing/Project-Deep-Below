//Script for player inventory
//Creater: king
//Date: 12/3/21
using UnityEngine;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private List<GameObject> inventory = new List<GameObject>();
    [SerializeField] int inventoryMax = 2;
    [SerializeField] GameObject currentWeapon = null;
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
    //Drop equiped weapon
    private void DropWeapon()
    {
        RemoveFromInventory(currentWeapon);
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
                    if(weapon.tag == "Ranged")
                    {
                        RangeCombat set = weapon.GetComponent<RangeCombat>();
                        AmmoManager.Instance.AddGun(weapon.name, set.Clip, set.AmmoCount, set.ClipMax);
                    }
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

                if (weapon.tag == "Ranged")
                {
                    AmmoManager.Instance.RemoveGun(weapon.name);
                }
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
                    SetWeapon();
                }
                break;
            case 1:
                if(inventory.Count > (currentInventory + _InventoryNumber))
                {
                    currentInventory++;
                    if(currentWeapon.tag == "Ranged")
                    {
                        SetAmmo();
                    }
                    SetWeapon();
                }
                break;
            case -1:
                if(currentInventory > 0)
                {
                    currentInventory--;
                    if (currentWeapon.tag == "Ranged")
                    {
                        SetAmmo();
                    }
                    SetWeapon();
                }
                break;
            default:
                PlayerStats.Instance.Animation.runtimeAnimatorController = PlayerStats.Instance.Unarmed; 
                break;
        }
    }
    private void SetWeapon()
    {
        Destroy(currentWeapon);
        GameObject item = Instantiate(inventory[currentInventory]);
        currentWeapon = item;
        item.name = inventory[currentInventory].name;
        item.transform.localScale = new Vector3(1, 1, 1);

        if(item.tag == "Ranged")
        {
            int[] ammo = AmmoManager.Instance.FindGun(item.name);
            RangeCombat set = item.GetComponent<RangeCombat>();

            set.Clip = ammo[0];
            set.AmmoCount = ammo[1];
            set.ClipMax = ammo[2];
        }
    }
    private void SetAmmo()
    {
        RangeCombat set = currentWeapon.GetComponent<RangeCombat>();

        AmmoManager.Instance.AddGun(currentWeapon.name, set.Clip, set.AmmoCount, set.ClipMax);
    }
}
