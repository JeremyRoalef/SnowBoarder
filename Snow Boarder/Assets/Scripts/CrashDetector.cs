using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CrashDetector : MonoBehaviour
{
    [SerializeField] float fltReloadDelay = 1f;
    [SerializeField] ParticleSystem crashEffect;
    [SerializeField] AudioClip crashSFX;

    bool boolHasCrashed = false;

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.tag == "Crash" && !boolHasCrashed)
        {
            FindObjectOfType<PlayerController>().DisableControls(); //finds the player controller script and calls the public
                                                                    //method.
            boolHasCrashed = true;
            crashEffect.Play(); //play particle effects
            GetComponent<AudioSource>().PlayOneShot(crashSFX);
            Invoke("ReloadScene", fltReloadDelay);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Crash") //collisions need the .gameObject part. triggers only need other.tag
        {
            FindObjectOfType<PlayerController>().PlayerCanJump();
        }
    }
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Crash")
        {
            FindObjectOfType<PlayerController>().PlayerCanNotJump();
        }
    }
    
    void ReloadScene()
    {
        SceneManager.LoadScene(0); //loads scene with index 0 in build settings.
    }


}