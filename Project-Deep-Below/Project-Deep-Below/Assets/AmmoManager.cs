//Class used to keep track of the ammo used by diffrent weapons
//Creater: King
//Date: 12/4/21

using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class AmmoManager : MonoBehaviour
{
    private Dictionary<string, int[]> gun;
    private static AmmoManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            gun = new Dictionary<string, int[]>();
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
    }
    //returns the active instance of the Ammo Manager
    public static AmmoManager Instance
    {
        get => instance;
    }
    //Updates/Adds gun already in Manager
    //IN    string(Gun name)
    //IN    int(Current clip)
    //IN    int(Current ammo count)
    //IN    int(Clip max)
    public void AddGun(string _GunName, int _Clip, int _AmmoCount, int _ClipMax)
    {
        int[] ammo = new int[] { _Clip, _AmmoCount, _ClipMax };
        bool exsists = false;
        //If gun exsists already then update it
        foreach(var item in gun)
        {
            if(item.Key == _GunName)
            {
                gun[_GunName] = ammo;
                exsists = true;
                break;
            }
        }
        //If the gun isnt in the current dictionary
        if(!exsists)
        {
            gun.Add(_GunName, ammo);
        }

    }
    //Removes a gun from the Manager
    //IN    string(Gun name)
    public void RemoveGun(string _GunName)
    {
        foreach(var item in gun)
        {
            if(item.Key == _GunName)
            {
                gun.Remove(_GunName);
            }
        }
    }
    //Finds gun in the dictionary and returns the int[]
    //IN    string(Gun name)
    //OUT   int[](Clip, Ammo count, Clip max)
    //Returns null if no Key is found
    public int[] FindGun(string _GunName)
    {
        int[] thisIsWhatYouWant = null;

        foreach(var item in gun)
        {
            if(item.Key == _GunName)
            {
                thisIsWhatYouWant = gun[_GunName];
            }
        }

        return thisIsWhatYouWant;
    }
}
