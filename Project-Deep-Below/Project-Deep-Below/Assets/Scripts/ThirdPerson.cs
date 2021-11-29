//The second camera for 3rd person perspective
//Creater: King
//Date: 11/26/21
using UnityEngine;

public class ThirdPerson : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothedPosition = .02f;


    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.position, (smoothedPosition * Time.deltaTime));
        //transform.position = target.position;
        Quaternion rotation = (target.parent.gameObject.transform.rotation);
        transform.rotation = rotation;
    }
}
