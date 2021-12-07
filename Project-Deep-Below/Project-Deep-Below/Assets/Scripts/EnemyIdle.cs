//Enemy Idle Script
//Creater: King
//Date: 11/23/21

using UnityEngine;
using Pathfinding;

public class EnemyIdle : MonoBehaviour
{
    /// <summary>
    /// ***DO NOT MODIFY***
    /// </summary>
    [SerializeField] private AIDestinationSetter pathfinding;


    //when the player enters trigger the enemy will begin the chase
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            pathfinding.target = GameManager.Instance.Player.transform;
        }
    }
    //when the player exits the trigger the enemy will remain stationary
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            pathfinding.target = null;
            this.GetComponentInParent<EnemyStats>().Animation.SetFloat("Speed", 0);
        }
    }
}
