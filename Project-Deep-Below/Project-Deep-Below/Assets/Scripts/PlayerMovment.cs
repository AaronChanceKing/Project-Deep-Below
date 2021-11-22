//Player Movment script
//Creater: King
//Date: 11/21/21

using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    //Variables
    //To Add Later
    //private Animator animator;
    //private AudioSource audioSorce;

    //For movment
    private CharacterController controller;
    private PlayerStats stats;
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        //Get access to character controller
        controller = this.GetComponent<CharacterController>();
        //Get access to player stats
        stats = this.GetComponent<PlayerStats>();

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

        //Is sprint button pushed down
        speed = Input.GetButton("Sprint") && stats.Stamina > 0 ? stats.SprintSpeed : stats.BaseSpeed;

        //Move thoes inputs to outside methods
        PlayerMove(move);
        PlayerDirection(mousePosition);
        DrainStamina(move);
    }

    //Method used for moving the character controller
    private void PlayerMove(Vector2 _Speed)
    {
        //Takes in absolute value of speed to always return a positive number
        if (Mathf.Abs(_Speed.x) >= 0 && Mathf.Abs(_Speed.y) >= 0)
        {
            controller.Move(_Speed * Time.deltaTime * (speed * stats.MoveLimiter));
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

    private void DrainStamina(Vector3 _Speed)
    {
        if(Mathf.Abs(_Speed.x) > 0 || Mathf.Abs(_Speed.y) > 0)
        {
            if (speed > stats.BaseSpeed && stats.Stamina > 0)
            {
                stats.Stamina -= (stats.StaminaDrain * 2);
            }
        }
        //Need to remain still to gain stamina back
        else if(_Speed.x == 0 && _Speed.y == 0)
        {
            if (stats.Stamina < stats.MaxStamina)
            {
                stats.Stamina += stats.StaminaDrain;
            }
        }

    }

}
