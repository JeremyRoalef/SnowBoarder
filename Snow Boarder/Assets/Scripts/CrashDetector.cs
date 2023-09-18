using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CrashDetector : MonoBehaviour
{
    [SerializeField] float fltReloadDelay = 1f;
    [SerializeField] ParticleSystem crashEffect;
    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.tag == "Crash")
        {
            crashEffect.Play(); //play particle effects
            Invoke("ReloadScene", fltReloadDelay);
        }
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(0); //loads scene with index 0 in build settings.
    }
}