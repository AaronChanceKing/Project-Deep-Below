//Camera follow script
//Creater: King
//Date: 11/21/21

using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    //Variables
    /// <summary>The GameObject that the camera will follow in game</summary>
    [SerializeField] private GameObject target;
    /// <summary>The total amount of smoothing, higher numbers will make the camera less smooth</summary>
    [SerializeField] private float smoothedSpeed = 10f;
    [SerializeField] private Vector3 offset;

    void LateUpdate()
    {
        //Keep smoothing speed contant regardless of framerate
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, (target.transform.position + offset), (smoothedSpeed * Time.deltaTime));

        transform.position = smoothedPosition;
    }
}
