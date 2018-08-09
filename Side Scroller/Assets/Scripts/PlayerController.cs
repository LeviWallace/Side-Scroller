using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{       
    // player details
    public float speed;
    public float jumpForce;
    private float moveInput;
    private bool isPlayable = true;
    private bool isGrounded;
    private bool facingRight = true;
    private int extraJumps;
    private float startX;
    private float startY;

    // general 
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;
    public int respawnTime;
    public int numExtraJumps;
    public float yDeath;
    private float currentYLocation;


    private Rigidbody2D rb;

    void Start()
    {
        // initialize starting points in constructor.
        startX = transform.position.x;
        startY = transform.position.y;
        // initialize Rigidbody component
        rb = GetComponent<Rigidbody2D>();
        // setter
        extraJumps = numExtraJumps;
    }

    // all physics updates (moving left to right)
    void FixedUpdate()
    {   

        if (isPlayable)
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

            moveInput = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

            if (facingRight == false && moveInput > 0)
            {
                Flip();
            }
            else if (facingRight == true && moveInput < 0)
            {
                Flip();
            }
        }

    }

    // jumping 
    void Update()
    {
        if (isPlayable)
        {
            if (isGrounded == true)
            {
                extraJumps = 2;
            }
            if (Input.GetKeyDown(KeyCode.UpArrow) && extraJumps > 0)
            {
                Jump();
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                Down();
            }
            currentYLocation = transform.position.y;
            //  Debug.Log(currentYLocation);
        }
        Respawn(currentYLocation);
    }

    // Fliping Sprite depending on where you are facing
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
    
    // jump called from update
    void Jump()
    {
        rb.velocity = Vector2.up * jumpForce;
        extraJumps--;
    }

    // moves player down
    void Down()
    {
        rb.velocity = (Vector2.down * jumpForce) / 2;
    }

    // Respawn, check y position; respawn if nessesary
    void Respawn(float y)
    {
        if (y <= yDeath)
        {
            isPlayable = false;
            Playable();
        }
    }

    // if player is interactable
    void Playable()
    {
        Debug.Log("Press Enter to start");

        transform.position = new Vector3(startX, startY, 0);
        if (Input.GetKeyDown(KeyCode.P))
        { 
            rb.velocity = new Vector2(0, -1);
            isPlayable = true;
        }
    }
}
