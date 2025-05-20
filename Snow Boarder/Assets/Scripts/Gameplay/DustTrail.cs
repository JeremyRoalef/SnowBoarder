using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dusttrail : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The particle system that plays the trail effects")]
    ParticleSystem trailVFX;

    const string GROUND_TAG_STRING = "Ground";
    const string NULL_TRAIL_VFX_STRING = "Warning: No trail VFX found!";

    private void Start()
    {
        InitializeReferences();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        //Conditions preventing collision enter
        if (!other.gameObject.CompareTag(GROUND_TAG_STRING)) return;
        if (ObjectReference.IsNull(trailVFX, NULL_TRAIL_VFX_STRING)) return;

        //Collision logic
        trailVFX.Play();
    }
    void OnCollisionExit2D(Collision2D other)
    {
        //Conditions preventing collision exit
        if (!other.gameObject.CompareTag(GROUND_TAG_STRING)) return;
        if (ObjectReference.IsNull(trailVFX, NULL_TRAIL_VFX_STRING)) return;

        //Collision logic
        trailVFX.Stop();
    }

    private void InitializeReferences()
    {
        //Check serialized fields
        ObjectReference.IsNull(trailVFX, NULL_TRAIL_VFX_STRING);
    }

}
