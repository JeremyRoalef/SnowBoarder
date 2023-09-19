using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dusttrail : MonoBehaviour
{

    [SerializeField] ParticleSystem trailEffect;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Crash") //collisions need the .gameObject part. triggers only need other.tag
        {
            trailEffect.Play();
        }
    }
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Crash")
        {
            trailEffect.Stop();
        }
    }
}
