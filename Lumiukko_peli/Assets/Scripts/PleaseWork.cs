using UnityEngine;
using UnityEngine.Animations;

/*
    This script provides jumping and movement in Unity 3D - Gatsby
*/

public class Player : MonoBehaviour
{
    // Camera Rotation
    public float mouseSensitivity = 2f;
    private float verticalRotation = 0f;
    private Transform cameraTransform;

    // Ground Movement
    private Rigidbody rb;
    public float MoveSpeed = 5f;
    private float moveHorizontal;
    private float moveForward;

    // Jumping
    public float jumpForce = 10f;
    public float fallMultiplier = 2.5f; // Multiplies gravity when falling down
    public float ascendMultiplier = 2f; // Multiplies gravity for ascending to peak of jump
    private bool isGrounded = true;
    public LayerMask groundLayer;
    private float groundCheckTimer = 0f;
    private float groundCheckDelay = 0.3f;
    private float playerHeight;
    private float raycastDistance;
    [SerializeField]
    Animator animator;
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;


        // Set the raycast to be slightly beneath the player's feet
        playerHeight = GetComponent<CapsuleCollider>().height * transform.localScale.y;
        raycastDistance = (playerHeight / 2) + 0.2f;

        // Hides the mouse
    //    Cursor.lockState = CursorLockMode.Locked;
    //    Cursor.visible = false;
     //   animator = GetComponent<Animator>();
    }

    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveForward = Input.GetAxisRaw("Vertical");

        RotateCamera();


        if (Input.GetKeyDown(KeyCode.Z))
        {
            animator.SetTrigger("FinishedBall");
        }
    }

    void FixedUpdate()
    {
        MovePlayer();
        
    }

    void MovePlayer()
    {

        Vector3 movement = transform.right * moveHorizontal + transform.forward * moveForward;
        Vector3 targetVelocity = movement * MoveSpeed;
        

        // Apply movement to the Rigidbody
        Vector3 velocity = rb.linearVelocity;
        velocity.x = targetVelocity.x;
        velocity.z = targetVelocity.z;
        rb.linearVelocity = velocity;
        animator.SetBool("WalkWhenMoved", true);

        // If we aren't moving and are on the ground, stop velocity so we don't slide
        if (isGrounded && moveHorizontal == 0 && moveForward == 0)
        {
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
            animator.SetBool("WalkWhenMoved", false);
        }
        transform.position += new Vector3(targetVelocity.x, 0, targetVelocity.z) * Time.deltaTime;

    }
    void RotateCamera()
    {

        float horizontalRotation = Input.GetAxis("Horizontal");
        transform.Rotate(0, horizontalRotation, 0);


    }


 
}

