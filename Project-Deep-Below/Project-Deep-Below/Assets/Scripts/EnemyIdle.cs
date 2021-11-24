//Enemy Idle Script
//Creater: King
//Date: 11/23/21

using UnityEngine;
using Pathfinding;

public class EnemyIdle : MonoBehaviour
{
    [SerializeField] private AIDestinationSetter pathfinding;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            pathfinding.target = GameManager.Instance.Player.transform;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            pathfinding.target = null;
        }
    }
}
