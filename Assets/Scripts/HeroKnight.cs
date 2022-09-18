using UnityEngine;
using System.Collections;

public class PlayerControllerHeroKnight : MonoBehaviour
{

    [SerializeField] float m_speed = 4.0f;
    [SerializeField] float m_jumpForce = 7.5f;
    [SerializeField] float m_rollForce = 6.0f;
    [SerializeField] bool m_noBlood = false;
    [SerializeField] float m_health = 100;
    [SerializeField] GameObject m_slideDust;



    private Animator m_animator;
    private Rigidbody2D m_body2d;
    private Sensor_HeroKnight m_groundSensor;
    private Sensor_HeroKnight m_wallSensorR1;
    private Sensor_HeroKnight m_wallSensorR2;
    private Sensor_HeroKnight m_wallSensorL1;
    private Sensor_HeroKnight m_wallSensorL2;
    private bool m_isWallSliding = false;
    private bool m_grounded = false;
    private bool m_rolling = false;
    private int m_facingDirection = 1;
    private int m_currentAttack = 0;
    private float m_timeSinceAttack = 0.0f;
    private float m_delayToIdle = 0.0f;
    private float m_rollDuration = 8.0f / 14.0f;
    private float m_rollCurrentTime;

    // Batu Eklemeler

    [SerializeField]
    private float maxHealth;
    [SerializeField]
    public float currentHealth;
    public HealthBar health_bar;

    [SerializeField]
    public GameObject fallDetector;

    [SerializeField]

    private Vector3 respawnPoint;
    private Slot slotObj;
    private int facingDirection = 1;
    private int lastWallJumpDirection;
    private bool isTouchingWall;
    private bool isWallSliding;
    private float movementInputDirection;
    private Rigidbody2D rb;

    private float jumpTimer;
    private float turnTimer;
    private float wallJumpTimer;
    private int amountOfJumpsLeft;
    private bool canNormalJump;
    private bool canWallJump;
    private bool isAttemptingToJump;
    private bool checkJumpMultiplier;
    //private bool canMove;
    private bool canFlip;
    private bool hasWallJumped;

    public Vector2 wallHopDirection;
    public Vector2 wallJumpDirection;

    public Transform groundCheck;
    public Transform wallCheck;

    private bool isGrounded;
    public float jumpTimerSet = 0.15f;
    public int amountOfJumps = 1;
    public float wallJumpTimerSet = 0.5f;
    public float wallJumpForce;
    private bool isFacingRight = true;
    public float variableJumpHeightMultiplier = 0.5f;












    // Use this for initialization
    void Start()
    {
        // Batu Eklemeler
        /* currentHealth = maxHealth;
         health_bar.SetMaxHealth(maxHealth);
         respawnPoint = transform.position;
         rb = GetComponent<Rigidbody2D>();
         amountOfJumpsLeft = amountOfJumps;
         wallHopDirection.Normalize();
         wallJumpDirection.Normalize();*/


        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_HeroKnight>();
        m_wallSensorR1 = transform.Find("WallSensor_R1").GetComponent<Sensor_HeroKnight>();
        m_wallSensorR2 = transform.Find("WallSensor_R2").GetComponent<Sensor_HeroKnight>();
        m_wallSensorL1 = transform.Find("WallSensor_L1").GetComponent<Sensor_HeroKnight>();
        m_wallSensorL2 = transform.Find("WallSensor_L2").GetComponent<Sensor_HeroKnight>();
        slotObj = GameObject.FindGameObjectWithTag("Slot").GetComponent<Slot>();

    }

    // Update is called once per frame
    void Update()
    {
        // Increase timer that controls attack combo
        m_timeSinceAttack += Time.deltaTime;

        // Increase timer that checks roll duration
        if (m_rolling)
            m_rollCurrentTime += Time.deltaTime;

        // Disable rolling if timer extends duration
        if (m_rollCurrentTime > m_rollDuration)
            m_rolling = false;

        //Check if character just landed on the ground
        if (!m_grounded && m_groundSensor.State())
        {
            m_grounded = true;
            m_animator.SetBool("Grounded", m_grounded);
        }

        //Check if character just started falling
        if (m_grounded && !m_groundSensor.State())
        {
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
        }

        //Batu ekleme
        /*if (turnTimer >= 0)
        {
            turnTimer -= Time.deltaTime;

            if (turnTimer <= 0)
            {
                //canMove = true;
                canFlip = true;
            }
        }
        */
        // -- Handle input and movement --
        float inputX = Input.GetAxis("Horizontal");

        // Flip Character Face
        flipFace(inputX);

        move(inputX);


        //Set AirSpeed in animator
        m_animator.SetFloat("AirSpeedY", m_body2d.velocity.y);

        // -- Handle Animations --
        //Wall Slide
        m_isWallSliding = (m_wallSensorR1.State() && m_wallSensorR2.State()) || (m_wallSensorL1.State() && m_wallSensorL2.State());
        //m_animator.SetBool("WallSlide", m_isWallSliding);

        //Death
        if (Input.GetKeyDown("e") && !m_rolling)
        {
            m_animator.SetBool("noBlood", m_noBlood);
            m_animator.SetTrigger("Death");
            slotObj.RemoveItem();
            Debug.Log("Removed");

        }

        //Hurt
        else if (Input.GetKeyDown("q") && !m_rolling)
            m_animator.SetTrigger("Hurt");

        //Attack
        else if (Input.GetMouseButtonDown(0) && m_timeSinceAttack > 0.25f && !m_rolling)
        {
            m_currentAttack++;

            // Loop back to one after third attack
            if (m_currentAttack > 3)
                m_currentAttack = 1;

            // Reset Attack combo if time since last attack is too large
            if (m_timeSinceAttack > 1.0f)
                m_currentAttack = 1;

            // Call one of three attack animations "Attack1", "Attack2", "Attack3"
            m_animator.SetTrigger("Attack" + m_currentAttack);

            // Reset timer
            m_timeSinceAttack = 0.0f;
        }

        // Block
        else if (Input.GetMouseButtonDown(1) && !m_rolling)
        {
            m_animator.SetTrigger("Block");
            m_animator.SetBool("IdleBlock", true);
        }

        else if (Input.GetMouseButtonUp(1))
            m_animator.SetBool("IdleBlock", false);

        // Roll
        else if (Input.GetKeyDown("left shift") && !m_rolling && !m_isWallSliding)
        {
            m_rolling = true;
            //m_animator.SetTrigger("Roll");
            m_body2d.velocity = new Vector2(m_facingDirection * m_rollForce, m_body2d.velocity.y);
        }


        //Jump
        else if (Input.GetKeyDown("space") && m_grounded && !m_rolling)
        {
            m_animator.SetTrigger("Jump");
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
            m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
            m_groundSensor.Disable(0.2f);

            /*if (isGrounded || (amountOfJumpsLeft > 0 && !isTouchingWall))
            {
                NormalJump();
            }
            else
            {
                jumpTimer = jumpTimerSet;
                isAttemptingToJump = true;
            }*/
            m_groundSensor.Disable(0.2f);
        }

        // Batu Ekleme
        /*if (checkJumpMultiplier && !Input.GetKeyDown("space"))
        {
            checkJumpMultiplier = false;
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * variableJumpHeightMultiplier);
        }*/

        //Run
        else if (Mathf.Abs(inputX) > Mathf.Epsilon)
        {
            // Reset timer
            m_delayToIdle = 0.05f;
            m_animator.SetInteger("AnimState", 1);
        }

        //Idle
        else
        {
            // Prevents flickering transitions to idle
            m_delayToIdle -= Time.deltaTime;
            if (m_delayToIdle < 0)
                m_animator.SetInteger("AnimState", 0);
        }

        // Batu Ekleme
        /*CheckIfWallSliding();
        CheckIfCanJump();

        fallDetector.transform.position = new Vector2(transform.position.x, fallDetector.transform.position.y);*/


    }
    // Batu Ekleme
    /* private void CheckIfCanJump()
     {
         if (isGrounded && rb.velocity.y <= 0.01f)
         {
             amountOfJumpsLeft = amountOfJumps;
         }

         if (isTouchingWall)
         {
             checkJumpMultiplier = false;
             canWallJump = true;
         }

         if (amountOfJumpsLeft <= 0)
         {
             canNormalJump = false;
         }
         else
         {
             canNormalJump = true;
         }

     }


     private void OnTriggerEnter2D(Collider2D collision)
     {

         if (collision.tag == "FallDetector")
         {
             transform.position = respawnPoint;
             slotObj.RemoveItem();
             Debug.Log("Removed");
         }
     }

     private void CheckIfWallSliding()
     {
         if (isTouchingWall && movementInputDirection == facingDirection && rb.velocity.y < 0)
         {
             isWallSliding = true;
         }
         else
         {
             isWallSliding = false;
         }
     }

     private void CheckJump()
     {
         if (jumpTimer > 0)
         {
             //WallJump
             if (!isGrounded && isTouchingWall && movementInputDirection != 0 && movementInputDirection != facingDirection)
             {
                 WallJump();
             }
             else if (isGrounded)
             {
                 NormalJump();
             }
         }

         if (isAttemptingToJump)
         {
             jumpTimer -= Time.deltaTime;
         }

         if (wallJumpTimer > 0)
         {
             if (hasWallJumped && movementInputDirection == -lastWallJumpDirection)
             {
                 rb.velocity = new Vector2(rb.velocity.x, 0.0f);
                 hasWallJumped = false;
             }
             else if (wallJumpTimer <= 0)
             {
                 hasWallJumped = false;
             }
             else
             {
                 wallJumpTimer -= Time.deltaTime;
             }
         }
     }

     private void NormalJump()
     {
         if (canNormalJump)
         {
             rb.velocity = new Vector2(rb.velocity.x, m_jumpForce);
             amountOfJumpsLeft--;
             jumpTimer = 0;
             isAttemptingToJump = false;
             checkJumpMultiplier = true;
         }
     }

     private void WallJump()
     {
         if (canWallJump)
         {
             rb.velocity = new Vector2(rb.velocity.x, 0.0f);
             isWallSliding = false;
             amountOfJumpsLeft = amountOfJumps;
             amountOfJumpsLeft--;
             Vector2 forceToAdd = new Vector2(wallJumpForce * wallJumpDirection.x * movementInputDirection, wallJumpForce * wallJumpDirection.y);
             rb.AddForce(forceToAdd, ForceMode2D.Impulse);
             jumpTimer = 0;
             isAttemptingToJump = false;
             checkJumpMultiplier = true;
             turnTimer = 0;
             //canMove = true;
             canFlip = true;
             hasWallJumped = true;
             wallJumpTimer = wallJumpTimerSet;
             lastWallJumpDirection = -facingDirection;

         }
     }

     public void DisableFlip()
     {
         canFlip = false;
     }

     public void EnableFlip()
     {
         canFlip = true;
     }

     private void Flip()
     {
         if (!isWallSliding && canFlip)
         {
             facingDirection *= -1;
             isFacingRight = !isFacingRight;
             transform.Rotate(0.0f, 180.0f, 0.0f);
         }
     }*/


    // Ekleme Son

    // Animation Events
    // Called in slide animation.
    void AE_SlideDust()
    {
        Vector3 spawnPosition;

        if (m_facingDirection == 1)
            spawnPosition = m_wallSensorR2.transform.position;
        else
            spawnPosition = m_wallSensorL2.transform.position;

        if (m_slideDust != null)
        {
            // Set correct arrow spawn position
            GameObject dust = Instantiate(m_slideDust, spawnPosition, gameObject.transform.localRotation) as GameObject;
            // Turn arrow in correct direction
            dust.transform.localScale = new Vector3(m_facingDirection, 1, 1);
        }
    }

    public void death()
    {
        if (Input.GetKeyDown("e") && !m_rolling && m_health <= 0)
        {
            m_animator.SetBool("noBlood", m_noBlood);
            m_animator.SetTrigger("Death");
        }
    }


    public Rigidbody2D GetRigidbody2D()
    {
        return m_body2d;
    }

    public void hurt(float damage)
    {
        if (!m_rolling)
        {
            m_animator.SetTrigger("Hurt");
            m_health -= damage;
        }

    }




    void flipFace(float inputX)
    {
        // Swap direction of sprite depending on walk direction
        if (inputX > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            m_facingDirection = 1;
        }

        else if (inputX < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            m_facingDirection = -1;
        }
    }

    void move(float inputX)
    {
        // Move
        if (!m_rolling)
            m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);
    }
}
