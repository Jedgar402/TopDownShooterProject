using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Custom cursor object
    public GameObject cursor;        
    //Bullet prefab
    public GameObject bulletPrefab; 
    //Point where the bullet spawns
    public Transform shootPoint;
    //Player speed
    public float moveSpeed = 5f;     
    //Bullet Speed
    public float bulletSpeed = 20f;  
    //Ground layer for cursor
    public LayerMask groundLayer;    
    //Main camera
    private Camera mainCamera;
    //Bool to determine shoot speed
    public bool shootFast = false;

    void Start()
    {
        mainCamera = Camera.main;
        //Cursor.visible = false; // Hide the default cursor
    }

    void Update()
    {
        //Handle player movement
        PlayerMovement();

        //Handle player rotation
        Rotation();

        //Handle shooting
        ShootHandler();

        //Update cursor position
        UpdateCursorPosition();
    }

    void PlayerMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveX, 0, moveZ).normalized * moveSpeed * Time.deltaTime;
        transform.Translate(movement, Space.World);
    }

    void Rotation()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, groundLayer))
        {
            Vector3 targetPosition = hitInfo.point;
            Vector3 direction = (targetPosition - transform.position).normalized;
            //Keep player upright
            direction.y = 0; 
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }

    void ShootHandler()
    {
        if(shootFast)
        { 
            while (Input.GetMouseButtonDown(0))
            {
                Shoot();
            }
        }
        else 
        {
            if (Input.GetMouseButtonDown(0))
            {
                Shoot();
            }
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
