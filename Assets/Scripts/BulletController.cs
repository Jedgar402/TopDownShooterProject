using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float delayToDestroy = 2f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, delayToDestroy);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}