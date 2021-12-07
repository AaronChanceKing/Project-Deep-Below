//Class used to keep track of the ammo used by diffrent weapons (Ranged)
//Creater: King
//Date: 12/4/21

using System.Collections.Generic;
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

    ///<summary>returns the active instance of the Ammo Manager</summary>
    public static AmmoManager Instance
    {
        get => instance;
    }

    /// <summary>
    /// This will add a gun into the Manager to keep track of the ammount count based on the gun name.
    /// </summary>
    /// <param name="_GunName">The name of the pre-fab/current pickup</param>
    /// <param name="_Clip">The amount of bullets left in the clip when picking up/ swaping</param>
    /// <param name="_AmmoCount">The total ammo remaining in the gun</param>
    /// <param name="_ClipMax">The default maximum clip size</param>
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

    /// <summary>
    /// This will remove a gun based on the name
    /// </summary>
    /// <param name="_GunName">The name of the current gun/preFab</param>
    public void RemoveGun(string _GunName)
    {
        foreach(var item in gun)
        {
            if(item.Key == _GunName)
            {
                gun.Remove(_GunName);
                break;
            }
        }
    }

    /// <summary>
    /// Finds if gun is in the current manager
    /// <para>***This method will return a null int[] if the gun is not found***</para>
    /// <para>***Make sure to check for this to avoid conflicts***</para>
    /// </summary>
    /// <param name="_GunName">The name of the current gun/preFab</param>
    /// <returns></returns>
    public int[] FindGun(string _GunName)
    {
        int[] thisIsWhatYouWant = null;

        foreach(var item in gun)
        {
            if(item.Key == _GunName)
            {
                thisIsWhatYouWant = gun[_GunName];
                break;
            }
        }

        return thisIsWhatYouWant;
    }
}
