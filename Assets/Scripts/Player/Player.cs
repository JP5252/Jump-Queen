using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float offsetValue;

    [HideInInspector] public bool isFacingRight;

    private Rigidbody2D rb;
    private Animator anim;
    private float moveInput;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        isFacingRight = true;
    }

    private void Update()
    {
        Move();
    }

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
}
