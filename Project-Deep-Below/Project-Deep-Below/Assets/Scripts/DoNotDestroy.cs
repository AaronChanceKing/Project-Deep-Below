//Script to not destroy object on load
//Creater: King
//Date: 11/24/21

using UnityEngine;

public class DoNotDestroy : MonoBehaviour
{
    private GameObject instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this.gameObject;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
    }
}
