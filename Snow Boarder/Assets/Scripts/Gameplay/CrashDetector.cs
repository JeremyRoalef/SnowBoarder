using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrashDetector : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    [Tooltip("The particle system responsible for player dying")]
    ParticleSystem crashVFX;

    [SerializeField]
    [Tooltip("The audio clip that sounds like the player crashing")]
    AudioClip crashSFX;

    [SerializeField]
    [Tooltip("The audio source attached to this obejct")]
    AudioSource audioSource;

    [Header("Settings")]
    [SerializeField]
    [Tooltip("The delay before loading a scene")]
    float reloadDelay = 1f;

    Player player;

    public bool HasFinishedLevel { get; set; }
    public bool HasCrashed { get; set; }

    const string NULL_CRASH_VFX_STRING = "Warning: No crash VFX given!";
    const string NULL_CRASH_SFX_STRING = "Warning: No crash SFX given!";
    const string NULL_PLAYER_MOVEMENT_STRING = "Warning: No PlayerMovement script in scene!";
    const string NULL_AUDIO_SOURCE_STRING = "Warning: No audio source component found!";
    const string GROUND_TAG_STRING = "Ground";

    /*
     * 
     * ---------------UNITY EVENTS---------------
     * 
     */

    private void Start()
    {
        InitializeReferences();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //Conditions preventing trigger enter
        if (HasCrashed) return;
        if (HasFinishedLevel) return;
        if (!other.gameObject.CompareTag(GROUND_TAG_STRING)) return;
        if (ObjectReference.IsNull(player, NULL_PLAYER_MOVEMENT_STRING)) return;
        if (ObjectReference.IsNull(crashVFX, NULL_CRASH_VFX_STRING)) return;
        if (ObjectReference.IsNull(audioSource, NULL_AUDIO_SOURCE_STRING)) return;


        //play FX
        crashVFX.Play();
        audioSource.PlayOneShot(crashSFX);

        //Trigger enter logic
        HasCrashed = true;
        player.SetLifeState(false);
        ReloadScene(reloadDelay);
    }

    /*
     * 
     * ---------------PRIVATE METHODS---------------
     * 
     */

    /// <summary>
    /// Method to reload the current scene
    /// </summary>
    /// <param name="delay">Delay before reloading scene</param>
    void ReloadScene(float delay) => 
        LevelLoader.Instance.LoadLevel(SceneManager.GetActiveScene().buildIndex, delay);

    /// <summary>
    /// Method to initialize the crash detector script
    /// </summary>
    void InitializeReferences()
    {
        HasCrashed = false;
        HasFinishedLevel = false;

        //Check serialized fields
        ObjectReference.IsNull(crashVFX, NULL_CRASH_VFX_STRING);
        ObjectReference.IsNull(crashSFX, NULL_CRASH_SFX_STRING);
        ObjectReference.IsNull(audioSource, NULL_AUDIO_SOURCE_STRING);

        //Get object references
        player = FindObjectOfType<Player>();
        ObjectReference.IsNull(player, NULL_PLAYER_MOVEMENT_STRING);
    }
}
