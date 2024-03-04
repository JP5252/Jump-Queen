using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float offsetValue;

    [Header("Jump")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpTimeMax;
    [SerializeField] private float jumpTimeMin;
    [SerializeField] private float jumpAngle;

    [Header("Jump")]
    [SerializeField] private LayerMask whatIsGround;

    [HideInInspector] public bool isFacingRight;

    private Rigidbody2D rb;
    private Collider2D col;
    private Animator anim;
    private float moveInput;

    private float jumpTimeCounter = 0f;

    private RaycastHit2D groundHit;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        col = GetComponent<Collider2D>();

        isFacingRight = true;
    }

    private void Update()
    {
        if (IsGrounded())
        {
            Move();
        }
        
        Jump();
    }

    #region Movement Functions

    private void Move()
    {
        moveInput = UserInput.instance.moveInput.x;

        if (moveInput > 0 || moveInput < 0 && IsGrounded())
        {
            // dont know why i have to set this isJumping false, isGrounded should set it to that
            anim.SetBool("isJumping", false);
            anim.SetBool("isWalking", true);
            TurnCheck();
        }
        else
        {
            anim.SetBool("isWalking", false);
        }

        // check if player is crouched before moving them
        if (!anim.GetBool("isCrouching") && !anim.GetBool("isJumping"))
        {
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        }
    }

    private void Jump()
    {
        if (UserInput.instance.controls.Jumping.Jump.WasPressedThisFrame() && IsGrounded())
        {
            jumpTimeCounter = 0f;
            anim.SetBool("isCrouching", true);
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }

        if (UserInput.instance.controls.Jumping.Jump.IsPressed() && IsGrounded())
        {
            jumpTimeCounter += Time.deltaTime;
        }

        if (UserInput.instance.controls.Jumping.Jump.WasReleasedThisFrame() && IsGrounded())
        {
            // Get the player's input direction using the moveInput
            moveInput = UserInput.instance.moveInput.x;

            // Calculate jump force based on charge time
            float chargeFactor = Mathf.Clamp01(jumpTimeCounter / jumpTimeMax);
            float appliedJumpForce = Mathf.Lerp(jumpForce * jumpTimeMin, jumpForce * jumpTimeMax, chargeFactor);

            // Apply constant horizontal velocity during jump
            float horizontalVelocity = moveInput * jumpAngle;

            // Apply the calculated velocities
            rb.velocity = new Vector2(horizontalVelocity, appliedJumpForce);

            // Set appropriate animation states
            anim.SetBool("isCrouching", false);
            anim.SetBool("isJumping", true);
        }
    }

    #endregion

    #region Ground Check
    /// <summary>
    /// This is where we perform ground checks to see if the player is currently on the ground using boxcasting.
    /// </summary>
    /// <returns>
    /// true if player is on the ground
    /// false if player is in the air
    /// </returns>
    private bool IsGrounded()
    {
        // Perform boxcast to check for any collision
        groundHit = Physics2D.BoxCast(col.bounds.center, col.bounds.size, 0f, Vector2.down, 0.25f, whatIsGround);

        // Check if the player is touching any collider
        if (groundHit.collider != null)
        {
            // Check if the normal of the collision is not completely vertical (indicating a wall)
            if (Vector2.Dot(groundHit.normal, Vector2.up) < 0.95f)
            {
                // Bounce off the wall
                Vector2 wallNormal = groundHit.normal;
                rb.velocity = Vector2.Reflect(rb.velocity, wallNormal);
                return false;
            }
            else
            {
                // If the normal is vertical, treat it as ground
                anim.SetBool("isJumping", false);
                anim.SetBool("isFalling", false);
                return true;
            }
        }
        else
        {
            // If no collider is hit, treat it as falling
            if (rb.velocity.y < 0)
            {
                anim.SetBool("isFalling", true);
            }
            else
            {
                anim.SetBool("isFalling", false);
            }
            return false;
        }
    }





    #endregion

    #region Turn Checks
    /// <summary>
    /// This checks if the player needs to turn
    /// </summary>
    private void TurnCheck()
    {
        if (UserInput.instance.moveInput.x > 0 && !isFacingRight)
        {
            Turn();
        }

        else if (UserInput.instance.moveInput.x < 0 && isFacingRight)
        {
            Turn();
        }
    }
    /// <summary>
    /// this turns the player if the player needs to turn
    /// </summary>
    private void Turn()
    {
        if (isFacingRight)
        {
            transform.position += new Vector3(-offsetValue, 0f, 0f);
            Vector3 rotator = new Vector3(transform.rotation.x, 180f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            isFacingRight = !isFacingRight;
        }
        else
        {
            transform.position += new Vector3(offsetValue, 0f, 0f);
            Vector3 rotator = new Vector3(transform.rotation.x, 0f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            isFacingRight = !isFacingRight;
        }
    }

    #endregion
}
