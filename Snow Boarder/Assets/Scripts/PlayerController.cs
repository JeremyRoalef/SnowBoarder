using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] float fltTorqueAmount = 1.0f;
    Rigidbody2D rb2d;
    SurfaceEffector2D surfaceEffector2D;
    [SerializeField] float fltBaseSpeed = 12.5f;
    [SerializeField] float fltBoostSpeed = 20f;


    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        surfaceEffector2D = FindObjectOfType<SurfaceEffector2D>();
    }

    void Update()
    {
        RotatePlayer();

        RespondToBoost();
    }

    void RotatePlayer()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb2d.AddTorque(fltTorqueAmount);
        }
        else if (Input.GetKey(KeyCode.RightArrow)) //else prevents player from pressing both arrows at same time
        {
            rb2d.AddTorque(-fltTorqueAmount);
        }
    }
    void RespondToBoost()
    {
        //if we push up, speed up. else, stay at normal speed
        if (Input.GetKey(KeyCode.UpArrow))
        {
            surfaceEffector2D.speed = fltBoostSpeed;
        }
        else
        {
            surfaceEffector2D.speed = fltBaseSpeed;
        }
    }
}
