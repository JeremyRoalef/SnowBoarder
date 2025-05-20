using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class FinishLine : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    [Tooltip("The particle system for when the player finished the level")]
    ParticleSystem finishVFX;

    [SerializeField]
    [Tooltip("The audio source attached to this game object")]
    AudioSource audioSource;


    [Header("Settings")]
    [SerializeField]
    [Tooltip("The delay before loading a scene")]
    float reloadDelay = 1f;


    Player player;

    bool hasFinishedLevel = false;

    const string NULL_PLAYER_STRING = "Warning: No player found in scene!";
    const string NULL_FINISH_VFX_STRING = "Warning: No finish VFX given!";
    const string NULL_AUDIO_SOURCE_STRING = "Warning: No audio source given!";

    /*
     * 
     * ---------------UNITY EVENTS---------------
     * 
     */

    private void Start()
    {
        InitializeReferences();
    }

    /*
     * 
     * ---------------PRIVATE METHODS---------------
     * 
     */

    void InitializeReferences()
    {
        //Check serialized field references
        ObjectReference.IsNull(finishVFX, NULL_FINISH_VFX_STRING);
        ObjectReference.IsNull(audioSource, NULL_AUDIO_SOURCE_STRING);

        //Get reference to player
        player = FindObjectOfType<Player>();
        ObjectReference.IsNull(player, NULL_PLAYER_STRING);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("TRIGGERED");
        Debug.Log(other.gameObject.name);
        Debug.Log(player.gameObject.name);
        Debug.Log(hasFinishedLevel);

        //Conditions preventing trigger enter
        if (other.gameObject != player.gameObject) return;
        if (!player.IsAlive) return;
        if (hasFinishedLevel) return;

        if (ObjectReference.IsNull(finishVFX, NULL_FINISH_VFX_STRING)) return;
        if (ObjectReference.IsNull(audioSource, NULL_AUDIO_SOURCE_STRING)) return;


        //play particle effects
        finishVFX.Play(); 
        audioSource.Play();

        //Trigger logic
        hasFinishedLevel = true;
        player.SetFinishLevelState(true);
        LoadNextScene(reloadDelay);
    } 

    void LoadNextScene(float delay) => LevelLoader.Instance.LoadNextLevel(delay);
}
