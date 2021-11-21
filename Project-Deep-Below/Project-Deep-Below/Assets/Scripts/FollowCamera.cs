//Camera follow script
//Creater: King
//Date: 11/21/21

using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    //Variables
    [SerializeField] private GameObject target;
    [SerializeField] private float smoothedSpeed = 10f;
    [SerializeField] private Vector3 offset;

    void LateUpdate()
    {
        //Keep smoothing speed contant regardless of framerate
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, (target.transform.position + offset), (smoothedSpeed * Time.deltaTime));

        transform.position = smoothedPosition;
    }

    #region Getters
    //Get what the camera is currently targeting
    public GameObject GetTarget
    {
        get
        {
            return target;
        }
    }

    //Not sure if ill need this but might as well go ahead and add it incase
    //Get the cameras offset
    public Vector3 GetOffset
    {
        get
        {
            return offset;
        }
    }

    #endregion

    #region Setters
    //Set the target of the camera
    public GameObject SetTarget
    {
        set
        {
            target = value;
        }
    }

    //Should make it easier to 'film' cut scenes
    //Set the offset of the camera
    public Vector3 SetOffset
    {
        set
        {
            offset = value;
        }
    }

    #endregion
}
