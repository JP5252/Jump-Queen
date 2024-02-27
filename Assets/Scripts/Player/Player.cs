using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float offsetValue;

    [Header("Jump")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpTimeMax;

    [Header("Jump")]
    [SerializeField] private LayerMask whatIsGround;

    [HideInInspector] public bool isFacingRight;

    private Rigidbody2D rb;
    private Collider2D col;
    private Animator anim;
    private float moveInput;

    private bool isJumping;
    private bool isFalling;
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
        Move();
        Jump();
    }

    #region Movement Functions

    private void Move()
    {
        moveInput = UserInput.instance.moveInput.x;

        if (moveInput > 0 || moveInput < 0)
        {
            anim.SetBool("isWalking", true);
            TurnCheck();
        }
        else
        {
            anim.SetBool("isWalking", false);
        }

        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
    }

    private void Jump()
    {
        if (UserInput.instance.controls.Jumping.Jump.WasPressedThisFrame() && IsGrounded())
        {
            jumpTimeCounter = 0f;
        }

        if (UserInput.instance.controls.Jumping.Jump.IsPressed()) 
        {
            jumpTimeCounter += Time.deltaTime;
        }

        if (UserInput.instance.controls.Jumping.Jump.WasReleasedThisFrame())
        {
            //if the jump is less than max charge
            if (jumpTimeCounter < jumpTimeMax)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce * jumpTimeCounter * 2);
            }
            //if the jump is charged to max
            else
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce * jumpTimeMax * 2);
            }
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
        groundHit = Physics2D.BoxCast(col.bounds.center, col.bounds.size, 0f, Vector2.down, 0.25f, whatIsGround);

        if (groundHit.collider != null)
        {
            return true;
        }
        else
        {
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
