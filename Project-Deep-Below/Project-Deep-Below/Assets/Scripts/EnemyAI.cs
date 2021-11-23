//Enemy AI script
//Creater: King
//Date: 11/22/21

using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private GameObject player;
    

    private static Vector3 GetRandomDirection()
    {
        return new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized;
    }
}
