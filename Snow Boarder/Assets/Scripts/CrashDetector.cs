using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CrashDetector : MonoBehaviour
{
    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.tag == "Crash")
        {
            SceneManager.LoadScene(0); //loads scene with index 0 in build settings.
        }
    }
}