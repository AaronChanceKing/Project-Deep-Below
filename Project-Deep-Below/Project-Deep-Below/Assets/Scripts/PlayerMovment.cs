//Player Movment script
//Creater: King
//Date: 11/21/21

using System.Collections;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    //Variables
    //To Add Later
    private Animator animator;
    //private AudioSource audioSorce;
    [SerializeField] private Camera cam;

    //For movment
    private CharacterController controller;
    private PlayerStats stats;
    private float speed;

    float rollBuffer = 0f;
    bool rolling = false;

    // Start is called before the first frame update
    void Start()
    {
        //Get access to character controller
        controller = this.GetComponent<CharacterController>();
        //Get access to player stats
        stats = this.GetComponent<PlayerStats>();

        //TODO
        animator = GetComponentInChildren<Animator>();
        //audioSorce = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //Get the inputs of the player
        Vector3 move = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        Vector3 mousePosition = Input.mousePosition;

        //Initiate roll
        if (Input.GetButtonDown("Roll") && Time.time >= rollBuffer)
        {
            rollBuffer = Time.time + stats.RollRate;
            rolling = true;
            StartCoroutine(PlayerRoll());
        }
        
        //Is sprint button pushed down
        speed = Input.GetButton("Sprint") && stats.Stamina > 0 ? stats.SprintSpeed : stats.BaseSpeed;

        //Move thoes inputs to outside methods
        if(cam.isActiveAndEnabled)
        {
            PlayerDirectionThird(mousePosition);
            PlayerMoveThird(move);
        }
        else
        {
            PlayerDirection(mousePosition);
            PlayerMove(move);
        }
        DrainStamina(move);
        if (rolling)
        {
            Roll(move);
        }
    }
    private void Roll(Vector3 move)
    {
        if (cam.isActiveAndEnabled)
        {
            Vector3 moveX = cam.transform.right * move.x;
            Vector3 moveZ = transform.forward * move.z;

            move = (moveX + moveZ);
        }
        controller.Move(move * stats.Roll * Time.deltaTime);
    }
    //Method used for moving the character controller
    private void PlayerMove(Vector3 _Speed)
    {
        if (_Speed.magnitude >= .1f)
        {
            controller.Move(_Speed * Time.deltaTime * speed);
        }

        animator.SetFloat("Speed", (Mathf.Abs(_Speed.x) + Mathf.Abs(_Speed.z)));
        
    }
    //Method for 3rd person movment
    private void PlayerMoveThird(Vector3 _Speed)
    {
        Vector3 moveX = cam.transform.right * _Speed.x;
        Vector3 moveZ = transform.forward * _Speed.z;

        _Speed = (moveX + moveZ);

        if (_Speed.magnitude >= .1f)
        {
            controller.Move(_Speed * Time.deltaTime * speed);
        }

        animator.SetFloat("Speed", (Mathf.Abs(_Speed.x) + Mathf.Abs(_Speed.z)));
    }

    //Method to rotate the player in direction of the mouse
    private void PlayerDirection(Vector3 _MousePosition)
    {
        Vector3 playerPosition = Camera.main.WorldToScreenPoint(transform.position);
        _MousePosition.z = Camera.main.transform.position.z;
        _MousePosition.x = _MousePosition.x - playerPosition.x;
        _MousePosition.y = _MousePosition.y - playerPosition.y;

        float angle = Mathf.Atan2(_MousePosition.x, _MousePosition.y) * Mathf.Rad2Deg;


        transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0));
    }

    private void PlayerDirectionThird(Vector3 _MousePosition)
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, _MousePosition.x, 0));
    }

    IEnumerator PlayerRoll()
    {
        yield return new WaitForSeconds(stats.RollDistance);

        rolling = false;
    }

    private void DrainStamina(Vector3 _Speed)
    {
        if(_Speed.magnitude >= .1f)
        {
            if (speed > stats.BaseSpeed && stats.Stamina > 0)
            {
                stats.Stamina -= (stats.StaminaDrain * 2);
            }
        }
        //Need to remain still to gain stamina back
        else if(_Speed.magnitude == 0)
        {
            if (stats.Stamina < stats.MaxStamina)
            {
                stats.Stamina += stats.StaminaDrain;
            }
        }

    }

}
