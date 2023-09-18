using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] float fltTorqueAmount = 1.0f;
    Rigidbody2D rb2d;


    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
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
}
