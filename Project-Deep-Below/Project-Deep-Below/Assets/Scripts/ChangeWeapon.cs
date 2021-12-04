//Script for changing weapons
//Creater: King
//Date: 12/2/21
using UnityEngine;

public class ChangeWeapon : MonoBehaviour
{
    [SerializeField] private Vector3 location;
    [SerializeField] private Vector3 rotation;
    // Start is called before the first frame update
    void Start()
    {
        AddItem();
    }

    private void AddItem()
    {
        this.GetComponent<UpgradeScript>().UpgradePlayer(GameManager.Instance.Player);

        //Add Logic for equiping
        transform.parent = GameManager.Instance.PlayerStats.PickUpTarget.transform;
        transform.localPosition = location;
        transform.localRotation = Quaternion.Euler(rotation);
    }

}
