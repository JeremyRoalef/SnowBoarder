using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The player movement script attached to this prefab")]
    PlayerMovement playerMovement;

    [SerializeField]
    [Tooltip("The crash detector script within this prefab")]
    CrashDetector crashDetector;

    bool _isAlive;
    public bool IsAlive {
        get
        {
            return _isAlive;
        } 
        private set
        {
            _isAlive = value;
            HandleLifeStateChange();
        }
    }

    bool _hasFinishedLevel;
    public bool HasFinishedLevel
    {
        get
        {
            return _hasFinishedLevel;
        }
        set
        {
            _hasFinishedLevel = value;
            HandleLevelFinishedStateChanged();
        }
    }


    const string NULL_PLAYER_MOVEMENT_REFERENCE = "Warning: No PlayerMovement script on game object!";
    const string NULL_CRASH_DETECTOR_REFERENCE = "Warning: No CrashDetector script on game object!";

    /*
     * 
     * ---------------UNITY EVENTS---------------
     * 
     */

    private void Awake()
    {
        InitializeReferences();
    }

    /*
     * 
     * ---------------PUBLIC METHODS---------------
     * 
     */

    /// <summary>
    /// Method to set the IsAlive bool
    /// </summary>
    /// <param name="crashState">new IsAlive state</param>
    public void SetLifeState(bool crashState)
    {
        if (ObjectReference.IsNull(playerMovement, NULL_PLAYER_MOVEMENT_REFERENCE)) return;
        if (ObjectReference.IsNull(NULL_CRASH_DETECTOR_REFERENCE, NULL_CRASH_DETECTOR_REFERENCE)) return;

        IsAlive = crashState;
    }

    /// <summary>
    /// Method to set the HasFinishedLevel bool
    /// </summary>
    /// <param name="finishedLevelState">new HasFinishedLevel state</param>
    public void SetFinishLevelState(bool finishedLevelState)
    {
        if (ObjectReference.IsNull(playerMovement, NULL_PLAYER_MOVEMENT_REFERENCE)) return;
        if (ObjectReference.IsNull(NULL_CRASH_DETECTOR_REFERENCE, NULL_CRASH_DETECTOR_REFERENCE)) return;

        HasFinishedLevel = finishedLevelState;
    }

    /*
     * 
     * ---------------PRIVATE METHODS---------------
     * 
     */

    /// <summary>
    /// Method to initialize all references in the player script
    /// </summary>
    private void InitializeReferences()
    {
        ObjectReference.IsNull(playerMovement, NULL_PLAYER_MOVEMENT_REFERENCE);
        ObjectReference.IsNull(NULL_CRASH_DETECTOR_REFERENCE, NULL_CRASH_DETECTOR_REFERENCE);

        IsAlive = true;
        HasFinishedLevel = false;
    }

    /// <summary>
    /// Method to handle the logic for life state changing
    /// </summary>
    void HandleLifeStateChange()
    {
        if (_isAlive)
        {

        }
        else
        {
            playerMovement.HandleDeath();
        }
    }

    /// <summary>
    /// Method to handle the logic for level finish state changing
    /// </summary>
    void HandleLevelFinishedStateChanged()
    {
        if (_hasFinishedLevel)
        {
            crashDetector.HasFinishedLevel = true;
        }
        else
        {

        }
    }
}
