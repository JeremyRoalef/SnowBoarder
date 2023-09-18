using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class FinishLine : MonoBehaviour
{
    [SerializeField] float fltReloadDelay = 1f;

    void OnTriggerEnter2D(Collider2D other)
    {
       if (other.tag == "Player")
        {
            Invoke("ReloadScene", fltReloadDelay);
        }

    }

    void ReloadScene()
    {
        SceneManager.LoadScene(0); //loads scene with index 0 in build settings.
    }
}
