using UnityEngine;

/// <summary>
/// Author: Justin Pearson
/// 
/// This class takes in the users input and defines character movement it includes:
/// Start() to set some of our variables like the players rigidbody
/// Update() to wait for player input and set in motion our movement functions
/// 
/// Then there is the movement region which includes:
/// Move() which is called when the player uses a move button which is implemented with Unitys input control
/// Jump() which is called when the player uses a jump button implemented with Unitys input control
/// 
/// Then we have the ground check region:
/// which has IsGrounded() which in our game must be called constantly to implement our falling functions
/// 
/// TurnCheck region which has:
/// TurnCheck() which is called from the movement function to flip the characters model
/// 
/// BigFall region:
/// CheckBigFall() which is called in the IsGrounded function when the player hits the ground after a fall
/// </summary>
public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float offsetValue;

    [Header("Jump")]
    [SerializeField] private float verticalJumpForce;
    [SerializeField] private float jumpTimeMax;
    [SerializeField] private float jumpTimeMin;
    [SerializeField] private float horizontalJumpForce;

    [Header("Wall")]
    [SerializeField] private LayerMask whatIsGround;

    [Header("Fall")]
    [SerializeField] private float bigFallHeight;

    [HideInInspector] public bool isFacingRight;

    [Header("Other")]
    [SerializeField] public GameObject Audio;
    [SerializeField] public statTracker StatTracker;


    private Rigidbody2D rb;
    private Collider2D col;
    private Animator anim;
    private float moveInput;

    private float jumpTimeCounter;

    private RaycastHit2D groundHit;

    private Vector3 lastPosition;

    private bool canJump = false;

    private bool onIce = false;

    private bool onSlant = false;

    
    private AudioSource jump;
    private AudioSource landing;
    private AudioSource hardLanding;

   
    

    /// <summary>
    /// Setting our components for rigidbody, animations, and collider
    /// </summary>
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        col = GetComponent<Collider2D>();

        isFacingRight = true;

        jump = Audio.transform.Find("jump").GetComponent<AudioSource>();
        landing = Audio.transform.Find("landing").GetComponent<AudioSource>();
        hardLanding = Audio.transform.Find("hardLanding").GetComponent<AudioSource>();

    }

    /// <summary>
    /// this just checks every frame for an input for move or jump
    /// </summary>
    private void Update()
    {
        if (IsGrounded())
        {
            Jump();
            Move();    
        }
    }

    #region Movement Functions
    /// <summary>
    /// this is the logic for movement in the game, we first check that the player is not at the end scene, then if the player is 
    /// not on a slanted terrain and have a move input.
    /// then set the player animation for iswalking on if the move input is there and peform a move check to make sure the player
    /// is facing the right direction, 
    /// after that we can set the players x velocity to the movespeed in the direction they are going if they are on normal terrain
    /// if they are not then we check if they are on ice then there are two options:
    /// 1. on flat ice they will have force applied to them so the movment is not instant and more slippery feeling
    /// 2. if they are on slanted ice the player will be translated slightly on the x axis in the direction they want to go,
    /// this allows for the player to get up slanted jump they just barely make, as well as allows the walking into slants from the edge
    /// </summary>
    private void Move()
    {
        moveInput = UserInput.instance.moveInput.x;

        // check if the player is at the ending animation by checking their constraints
        if (rb.constraints == RigidbodyConstraints2D.FreezePositionX)
        {
            anim.SetBool("isWalking", false);
        }
        //check the players input is in a direction or left or right and they are not on a slant
        else if ((moveInput > 0 || moveInput < 0) && !onSlant)
        {
            anim.SetBool("bigFall", false);
            anim.SetBool("isWalking", true);
            TurnCheck();
        }
        else
        {
            anim.SetBool("isWalking", false);
        }

        // Check if player is crouched before moving them and that they are not moving vertically at a high speed like jumping
        if (!anim.GetBool("isCrouching") && rb.velocity.y < 0.1 && rb.velocity.y > -0.1)
        {

            // movement on normal ground
            if (anim.GetBool("isWalking") && !onIce && !onSlant)
            {
                rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
            }
            // for movement on ice
            else 
            {
                // if on flat ice
                if (rb.velocity.x < moveSpeed && rb.velocity.x > -moveSpeed && !onSlant)
                {
                    rb.AddForce(new Vector2(moveInput * 0.45f, 0), ForceMode2D.Impulse);
                }
                // if on the slanted ice. this is here so that walking onto a slant works
                else
                {
                    if (rb.velocity.y == 0 && rb.velocity.x == 0)
                    {
                        transform.position += new Vector3(moveInput * .03f, 0f, 0f);
                    }
                }
            }
        }
    }



    /// <summary>
    /// This is the logic for the jump, first there will be a check if the player is grounded and if they are, then the player may begin their jump
    /// the jump is split into three parts
    /// 1. the frame the jump button was pressed
    ///     - the jumpTimeCounter will be set to current time
    ///     - the crouch animation will begin
    ///     - canJump bool will be set to true, this will prevent a jump unless this part of the function executes
    /// 2. while the jump button is being pressed down
    ///     - every frame we check the time passed is less than the max jump time, and execute the jump is it is
    /// 3. when the jump button is released
    ///     - check if the time passed is less than minimum jump, set the jump counter to min if it is
    ///     - set the jumptimecounter to Time.time - jumptimecounter otherwise
    ///     - then we will execute the jump
    /// </summary>
    private void Jump()
    {
        if (UserInput.instance.controls.Jumping.Jump.WasPressedThisFrame() && !onSlant && rb.constraints != RigidbodyConstraints2D.FreezePositionX)
        {
            jumpTimeCounter = Time.time;
            anim.SetBool("bigFall", false);
            anim.SetBool("isCrouching", true);
            canJump = true;
            
        }

        if (UserInput.instance.controls.Jumping.Jump.IsPressed() && canJump)
        {
            if (Time.time - jumpTimeCounter >= jumpTimeMax)
            { 
                moveInput = UserInput.instance.moveInput.x;

                // execute jump
                rb.velocity = new Vector2(moveInput * horizontalJumpForce, verticalJumpForce * jumpTimeMax * 2);

                // set appropriate animation states
                anim.SetBool("isJumping", true);
                anim.SetBool("isCrouching", false);

                // reset jump counter
                jumpTimeCounter = 0f;
                jump.Play();
                canJump = false;

                //increment jump counter
                StatTracker.addJump();
            }
        }

        if (UserInput.instance.controls.Jumping.Jump.WasReleasedThisFrame() && canJump)
        {
                
            // set counter to min if its below
            if (Time.time - jumpTimeCounter < jumpTimeMin)
            {
                jumpTimeCounter = jumpTimeMin;
            }
            // set to difference between that and time otherwise
            else
            {
                jumpTimeCounter = Time.time - jumpTimeCounter;
            }

            // get the player's input direction using the moveInput
            moveInput = UserInput.instance.moveInput.x;

            // execute jump
            rb.velocity = new Vector2(moveInput * horizontalJumpForce, verticalJumpForce * jumpTimeCounter * 2);

            jump.Play();

            // set appropriate animation states
            anim.SetBool("isJumping", true);
            anim.SetBool("isCrouching", false);

            // reset jump counter
            jumpTimeCounter = 0f;
            canJump = false;

            //increment jump counter
            StatTracker.addJump();
        }

    }

    #endregion

    #region Ground Check
    /// <summary>
    /// This is where we perform ground checks to see if the player is currently on the ground using boxcasting.
    /// </summary>
    /// <returns>
    /// true if player is on the ground as well as sets the jumping and falling to false on the ground.
    /// false if player is in the air as well as if the players y velocity is less than zero set falling true otherwise jumping to true
    /// </returns>
    private bool IsGrounded()
    {
        // perform boxcast to check for any collision
        groundHit = Physics2D.BoxCast(col.bounds.center, col.bounds.size, 0f, Vector2.down, 0.1f, whatIsGround);

        // check if the player is touching any collider
        if (groundHit.collider != null)
        {

            // access the collider's GameObject directly
            GameObject groundObject = groundHit.collider.gameObject;

            // set the type of ground for determining movement style
            if (groundObject.name != "Terrain")
            {
                onIce = true;
                if (groundObject.name == "Slanted Icy Terrain")
                {
                    onSlant = true;
                }
                else
                {
                    onSlant = false;
                }
            }
            else
            {
                onIce = false;
                onSlant = false;
                
            }

            if (!onSlant)
            {
                CheckBigFall();
            }
            
            // if the normal is vertical, treat it as ground
            anim.SetBool("isJumping", false);
            anim.SetBool("isFalling", false);
            return true;
        }
        else
        {
            // if no collider is hit, and velocity is negative treat it as falling
            if (rb.velocity.y <= 0 && !anim.GetBool("isFalling"))
            {
                anim.SetBool("isFalling", true);
                //set position when fall begins to determine big fall
                lastPosition = rb.position;
            }
            // otherwise treat it as jumping
            else
            {
                anim.SetBool("isJumping", true);
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
    /// this turns the player if the player needs to turn, it also transforms the player by an offset value because the character is not centered
    /// in the sprite.
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

    #region Big Fall Checks
    /// <summary>
    /// checks if the player falls a distance greater than bigFallHeight and activates bigFall animation if true
    /// </summary>
    private void CheckBigFall()
    {
        float fallDistance = lastPosition.y - rb.position.y;
        if (fallDistance > bigFallHeight)
        {
            anim.SetBool("bigFall", true);
            hardLanding.Play();

            //increment fall counter
            StatTracker.addFall();
        }
        if (1 < fallDistance && fallDistance < bigFallHeight)
        {
            landing.Play();
        }

        lastPosition = transform.position;
    }

    #endregion
}
