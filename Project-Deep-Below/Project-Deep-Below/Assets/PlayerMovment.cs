//Player Movment script
//Creater: King
//Date: 11/21/21

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    //Variables
    //To Add Later
    private Animator animator;
    private AudioSource audioSorce;

    //For movment
    private CharacterController controller;
    [SerializeField] private float moveLimiter;
    [SerializeField] private float speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        //Get access to character controller
        controller = GetComponent<CharacterController>();

        //TODO
        //animator = GetComponent<Animator>();
        //audioSorce = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //Get the inputs of the player
        Vector2 move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Vector3 mousePosition = Input.mousePosition;

        //Move thoes inputs to outside methods
        PlayerMove(move);
        PlayerDirection(mousePosition);
    }

    //Method used for moving the character controller
    private void PlayerMove(Vector2 _Speed)
    {
        if (Mathf.Abs(_Speed.x) >= 0 && Mathf.Abs(_Speed.y) >= 0)
        {
            controller.Move(_Speed * Time.deltaTime * (speed * moveLimiter));
        }
        else
        {
            controller.Move(_Speed * Time.deltaTime * speed);
        }
    }

    //Method to rotate the player in direction of the mouse
    private void PlayerDirection(Vector3 _MousePosition)
    {
        Vector3 playerPosition = Camera.main.WorldToScreenPoint(transform.position);
        _MousePosition.z = Camera.main.transform.position.z;
        _MousePosition.x = _MousePosition.x - playerPosition.x;
        _MousePosition.y = _MousePosition.y - playerPosition.y;

        float angle = Mathf.Atan2(_MousePosition.y, _MousePosition.x) * Mathf.Rad2Deg;


        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
