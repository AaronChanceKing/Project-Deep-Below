//Player Movment script
//Creater: King
//Date: 11/21/21

using System.Collections;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    #region Variables
    //To Add Later
    private Animator animator;
    //private AudioSource audioSource;

    //For movement
    private CharacterController controller;
    private PlayerStats stats;

    float rollBuffer = 0f;
    bool rolling = false;
    Vector3 roll;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //Get access to character controller
        controller = this.GetComponent<CharacterController>();
        //Get access to player stats
        stats = PlayerStats.Instance;
        //Get access to player animator
        animator = GetComponentInChildren<Animator>();
        //TODO
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
            roll = move;
            StartCoroutine(PlayerRoll());
        }
        

        PlayerDirection(mousePosition);
        PlayerMove(move);
        
        if (rolling)
        {
            Roll();
        }
    }

    //Method that activly moves player once roll is pressed
    private void Roll()
    {
        controller.Move(roll * stats.Roll * Time.deltaTime);
    }

    //Method used for moving the character controller
    private void PlayerMove(Vector3 _Speed)
    {
        if (_Speed.magnitude >= .1f)
        {
            controller.Move(_Speed * Time.deltaTime * stats.BaseSpeed);
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

    //Method that sets the animator for rolling as well as turns off roll after x time
    IEnumerator PlayerRoll()
    {
        animator.SetBool("Roll", rolling);
        yield return new WaitForSeconds(stats.RollDistance);
        rolling = false;
        animator.SetBool("Roll", rolling);
    }

}
