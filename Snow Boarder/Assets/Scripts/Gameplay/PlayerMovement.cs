using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField]
    [Tooltip("The speed in which the player will move by default on the ground")]
    float baseSpeed = 12.5f;

    [SerializeField]
    [Tooltip("The speed in which the player will move when boosting")]
    float boostSpeed = 20f;

    [SerializeField]
    [Tooltip("How quickly the player can rotate")]
    float torqueAmount = 1.0f;

    [SerializeField]
    [Tooltip("How high the player can jump")]
    float jumpForce = 5f;


    SurfaceEffector2D surfaceEffector2D;
    Rigidbody2D rb;

    bool canJump = false;
    public bool isAlive;

    //String constants
    const string NULL_SURFACE_EFFECTOR_STRING = "Warning: No surface effector 2D on given object!";
    const string NULL_RIGIDBODY_STRING = "Warning: No rigidbody2D on given object!";
    const string GROUND_TAG_STRING = "Ground";

    /*
     * 
     * ---------------UNITY EVENTS---------------
     * 
     */

    void Awake()
    {
        InitializeReferences();
    }

    void Update()
    {
        HandleRotation();
        HandleBoost();
    }

    void LateUpdate()
    {
        //jumping in lateupdate stops weird jumps from happening
        HandleJump();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag(GROUND_TAG_STRING)) return;

        //collision enter logic
        SetJump(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag(GROUND_TAG_STRING)) return;

        SetJump(false);
    }

    /*
     * 
     * ---------------PUBLIC METHODS---------------
     * 
     */

    /// <summary>
    /// When called, prevents the player from moving
    /// </summary>
    public void HandleDeath()
    {
        isAlive = false;
    }

    /// <summary>
    /// Determines whether the player is allowed to jump
    /// </summary>
    /// <param name="canJump"></param>
    public void SetJump(bool canJump)
    {
        this.canJump = canJump;
    }

    /*
     * 
     * ---------------PRIVATE METHODS---------------
     * 
     */

    /// <summary>
    /// Method to handle the logic for rotating the player
    /// </summary>
    void HandleRotation()
    {
        if (!isAlive) return;
        if (ObjectReference.IsNull(rb, NULL_RIGIDBODY_STRING)) return;


        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.AddTorque(torqueAmount * Time.deltaTime);
        }
        //else prevents player from pressing both arrows at same time
        else if (Input.GetKey(KeyCode.RightArrow)) 
        {
            rb.AddTorque(-torqueAmount * Time.deltaTime);
        }
    }

    /// <summary>
    /// Method to handle the logic for applying a boost to the player
    /// </summary>
    void HandleBoost()
    {
        //Conditions preventing boost
        if (!isAlive) return;
        if (ObjectReference.IsNull(surfaceEffector2D, NULL_SURFACE_EFFECTOR_STRING)) return;


        //if we push up, speed up. else, stay at normal speed
        if (Input.GetKey(KeyCode.UpArrow))
        {
            surfaceEffector2D.speed = boostSpeed;
        }
        else
        {
            surfaceEffector2D.speed = baseSpeed;
        }
    }

    /// <summary>
    /// Method to handle the jumping logic for the player
    /// </summary>
    void HandleJump()
    {
        //Conditions preventing jumping
        if (!isAlive) return;
        if (!canJump) return;
        if (!Input.GetKeyDown(KeyCode.Space)) return;
        if (ObjectReference.IsNull(rb, NULL_RIGIDBODY_STRING)) return;

        rb.AddForce(transform.up * jumpForce);
    }

    /// <summary>
    /// Method to initialize this script's required references
    /// </summary>
    void InitializeReferences()
    {
        isAlive = true;

        //Initialize References
        rb = GetComponent<Rigidbody2D>();
        ObjectReference.IsNull(rb, NULL_RIGIDBODY_STRING);

        surfaceEffector2D = FindObjectOfType<SurfaceEffector2D>();
        ObjectReference.IsNull(surfaceEffector2D, NULL_SURFACE_EFFECTOR_STRING);
    }
}
