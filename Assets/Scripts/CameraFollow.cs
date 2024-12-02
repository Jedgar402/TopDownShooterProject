using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //The player's transform to follow
    public Transform target;
    //Spped to control the smoothness of the camera follow
    public float smoothSpeed;
    //Offset from the player's position
    public Vector3 offset;
    private void Start()
    {
        if (target == null)
        {
            target = transform.Find("Player");
        }
    }

    void LateUpdate()
    {
        if (target == null)
            return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        //Make the camera look at the player
        transform.LookAt(target);
    }

}
