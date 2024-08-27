using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Getting the rigidbody of the player
    private Rigidbody rb;

    //Variables to hold the speed of the player and speed of the camera
    public float walkSpeed = 0.0f;

    //Used to control player crouch height
    public Transform player;

    public bool isGamePaused;

    //Float used as ratio for turning
    public float turnSpeed = 4.0f;

    //Max axis for x and y input
    public float minTurnAngle = -90.0f;
    public float maxTurnAngle = 90.0f;
    private float rotX;

    // Start is called before the first frame update
    void Start()
    {
        isGamePaused = false;

        //Getting the rigidbody
        rb = this.gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        ///Not included in Update as movement relies on timescale so is redundant being in there due to pausing methods
        //Every fixed frame, this function is called
        Movement();
    }

    void Look()
    {
        // get the mouse inputs
        float y = Input.GetAxis("Mouse X") * turnSpeed;
        rotX += Input.GetAxis("Mouse Y") * turnSpeed;
        // clamp the vertical rotation
        rotX = Mathf.Clamp(rotX, minTurnAngle, maxTurnAngle);
        // rotate the camera
        transform.eulerAngles = new Vector3(-rotX, transform.eulerAngles.y + y, 0);
    }

    void Movement()
    {
        //IF player holds left shift
        if (Input.GetKey(KeyCode.LeftShift))
        {
            //Increase speed
            walkSpeed = 10f;
        }
        else
        {
            //Default speed
            walkSpeed = 5f;
        }

        Vector2 axis = new Vector2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal")) * walkSpeed;
        Vector3 forward = new Vector3(-Camera.main.transform.right.z, 0.0f, Camera.main.transform.right.x);
        Vector3 direction = forward * axis.x + Camera.main.transform.right * axis.y + Vector3.up * rb.velocity.y;
        rb.velocity = direction;
    }
}
