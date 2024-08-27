using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    ////MOVEMENT VARIABLES
    ////Getting the rigidbody of the player
    //private Rigidbody rb;
    ////Variables to hold the speed of the player and speed of the camera
    //public float walkSpeed = 0.0f;
    ////Used to control player crouch height
    //public Transform player;

    ////POINT AND SHOOT VARIABLES
    //public GameObject cursor;
    //public GameObject bulletPrefab;
    //public Transform shootPoint;
    //public float bulletSpeed;
    //private Vector2 cursorPosition;

    //public bool isGamePaused;
    //// Start is called before the first frame update
    //void Start()
    //{
    //    Cursor.visible = false;

    //    isGamePaused = false;

    //    //Getting the rigidbody
    //    rb = this.gameObject.GetComponent<Rigidbody>();
    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}

    //private void FixedUpdate()
    //{
    //    ///Not included in Update as movement relies on timescale so is redundant being in there due to pausing methods
    //    //Every fixed frame, this function is called
    //    Movement();
    //}

    //void Movement()
    //{
    //    //IF player holds left shift
    //    if (Input.GetKey(KeyCode.LeftShift))
    //    {
    //        //Increase speed
    //        walkSpeed = 10f;
    //    }
    //    else
    //    {
    //        //Default speed
    //        walkSpeed = 5f;
    //    }

    //    Vector2 axis = new Vector2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal")) * walkSpeed;
    //    Vector3 forward = new Vector3(-Camera.main.transform.right.z, 0.0f, Camera.main.transform.right.x);
    //    Vector3 direction = forward * axis.x + Camera.main.transform.right * axis.y + Vector3.up * rb.velocity.y;
    //    rb.velocity = direction;
    //}

    public GameObject cursor;        // Reference to the custom cursor object
    public GameObject bulletPrefab;  // Reference to the bullet prefab
    public Transform shootPoint;     // Reference to the point where the bullet spawns
    public float moveSpeed = 5f;     // Speed of the player
    public float bulletSpeed = 20f;  // Speed of the bullet
    public LayerMask groundLayer;    // Layer mask for ground detection

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        //Cursor.visible = false; // Hide the default cursor
    }

    void Update()
    {
        // Handle player movement
        HandleMovement();

        // Handle player rotation
        HandleRotation();

        // Handle shooting
        HandleShooting();

        // Update cursor position
        UpdateCursorPosition();
    }

    void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveX, 0, moveZ).normalized * moveSpeed * Time.deltaTime;
        transform.Translate(movement, Space.World);
    }

    void HandleRotation()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, groundLayer))
        {
            Vector3 targetPosition = hitInfo.point;
            Vector3 direction = (targetPosition - transform.position).normalized;
            direction.y = 0; // Keep the player upright
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }

    void HandleShooting()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = shootPoint.forward * bulletSpeed;
    }

    void UpdateCursorPosition()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, groundLayer))
        {
            cursor.transform.position = hitInfo.point;
        }
    }
}
