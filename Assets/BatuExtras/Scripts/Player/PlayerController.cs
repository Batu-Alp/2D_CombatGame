using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private float movementInputDirection;
    private float jumpTimer;
    private float turnTimer;
    private float wallJumpTimer;
    //private float dashTimeLeft;
    private float lastImageXpos;
    //private float lastDash = -100f;

    [SerializeField]
    private float knockbackStartTime;

    private int amountOfJumpsLeft;
    private int facingDirection = 1;
    private int lastWallJumpDirection;

    private bool isFacingRight = true;
    private bool isWalking;
    private bool isGrounded;
    private bool isTouchingWall;
    private bool isWallSliding;
    private bool canNormalJump;
    private bool canWallJump;
    private bool isAttemptingToJump;
    private bool checkJumpMultiplier;
    private bool canMove;
    private bool canFlip;
    private bool hasWallJumped;
    private bool isTouchingLedge;
    //private bool canClimbLedge = false;
    private bool ledgeDetected;
    //private bool isDashing;
    private bool knockback;

    //private Vector2 ledgePosBot;
    //private Vector2 ledgePos1;
    //private Vector2 ledgePos2;

    private Rigidbody2D rb;
    private Animator anim;

    public int amountOfJumps = 1;

    //public float movementSpeed = 10.0f;
    //public float jumpForce = 12.0f;
    public float movementSpeed;
    public float jumpForce;
    public float groundCheckRadius;
    public float wallCheckDistance;
    public float wallSlideSpeed;
    public float movementForceInAir;
    public float airDragMultiplier = 0.95f;
    public float variableJumpHeightMultiplier = 0.5f;
    public float wallHopForce;
    public float wallJumpForce;
    public float jumpTimerSet = 0.15f;
    public float turnTimerSet = 0.1f;
    public float wallJumpTimerSet = 0.5f;
    //public float ledgeClimbXOffset1 = 0f;
    //public float ledgeClimbYOffset1 = 0f;
    //public float ledgeClimbXOffset2 = 0f;
    //public float ledgeClimbYOffset2 = 0f;
    //public float dashTime;
    //public float dashSpeed;
    public float distanceBetweenImages;

    private float knockbackDuration;
    //public float dashCoolDown;

    [SerializeField]
    private Vector2 knockbackSpeed;

    public Vector2 wallHopDirection;
    public Vector2 wallJumpDirection;

    public Transform groundCheck;
    public Transform wallCheck;
    //public Transform ledgeCheck;

    public LayerMask whatIsGround;


    //public int maxHealth = 100;
    //public int currentHealth;
    //public HealthBar healthBar;

    //public GameOver GameOver;
    //int maxPlatform = 0;

    private Vector3 respawnPoint;
    public GameObject fallDetector;

    private bool jumpBoost = false;
    private bool speedBoost = false;

    private float temp_jump;
    private float temp_speed;

    //public GameObject player;

    //public PlayerStats obj;

    private Slot slotObj;

    [SerializeField] private AudioSource jumpSoundEffect;
    [SerializeField] private AudioSource walkEffect;
    public int Money { get; set; }




    // Start is called before the first frame update
    void Start()
    {
        temp_jump = jumpForce;
        temp_speed = movementSpeed;

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        amountOfJumpsLeft = amountOfJumps;
        wallHopDirection.Normalize();
        wallJumpDirection.Normalize();
        //currentHealth = maxHealth;
        //healthBar.SetMaxHealth(maxHealth);

        respawnPoint = transform.position;
        slotObj = GameObject.FindGameObjectWithTag("Slot").GetComponent<Slot>();

        //player = GameObject.Find("NewPlayer");

    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        CheckMovementDirection();
        UpdateAnimations();
        CheckIfCanJump();
        CheckIfWallSliding();
        CheckJump();
        //CheckLedgeClimb();
        //CheckDash();
        CheckKnockback();

        //inventory = new Inventory();

        //obj.DecreaseHealth();

        /*if (Input.GetKeyDown(KeyCode.Q))
        {
            TakeDamage(20);
        }

        if (currentHealth == 0)
        {
            GameOverScreen();
        }*/

        fallDetector.transform.position = new Vector2(transform.position.x, fallDetector.transform.position.y);
        //Debug.Log("Fall ?");

        /*if (player.transform.position.y <= -10)
        {

            player.transform.position = new Vector2(-8, -2);

        }*/

    }

    public void Save(int gameNumber)
    { //Dilara
        var json = JsonUtility.ToJson(transform.position);
        PlayerPrefs.SetString(gameNumber + "PlayerPosition", json);
        PlayerPrefs.SetInt(gameNumber + "PlayerMoney", Money);
    }

    public void Load(int gameNumber)
    {
        if (PlayerPrefs.HasKey(gameNumber + "PlayerPosition"))
        { //Dilara
            var json = PlayerPrefs.GetString(gameNumber + "PlayerPosition");
            transform.position = JsonUtility.FromJson<Vector2>(json);
            Money = PlayerPrefs.GetInt(gameNumber + "PlayerMoney");

        }
    }
    public void AddMoney()
    {
        Money++;
    }


    public void AddJumpBoost(float value)
    {
        //jumpForce += value;
        StartCoroutine(Jump_Coroutine(value));


    }

    private IEnumerator Jump_Coroutine(float value)
    {
        if (!jumpBoost)
        {
            jumpForce += value;
            //graphics.GetComponent<Animator>().SetTrigger("damage");
            jumpBoost = true;
            StartCoroutine(IndicateImmortal(jumpBoost));
            yield return new WaitForSeconds(30);
            jumpBoost = false;
            jumpForce = temp_jump;
        }
    }


    private IEnumerator IndicateImmortal(bool check)
    {
        while (check)
        {
            //spriteRenderer.enabled = false;
            yield return new WaitForSeconds(.1f);
            //spriteRenderer.enabled = true;
            //yield return new WaitForSeconds(.1f);
        }
    }

    public void SpeedingBoost(float value)
    {

        //movementSpeed += value;
        StartCoroutine(Speed_Coroutine(value));

    }
    private IEnumerator Speed_Coroutine(float value)
    {
        if (!jumpBoost)
        {
            movementSpeed += value;
            //graphics.GetComponent<Animator>().SetTrigger("damage");
            speedBoost = true;
            StartCoroutine(IndicateImmortal(speedBoost));
            yield return new WaitForSeconds(30);
            speedBoost = false;
            movementSpeed = temp_speed;
        }
    }

    public void Knockback(int direction)
    {
        knockback = true;
        knockbackStartTime = Time.time;
        rb.velocity = new Vector2(knockbackSpeed.x * direction, knockbackSpeed.y);
    }

    private void CheckKnockback()
    {
        if (Time.time >= knockbackStartTime + knockbackDuration && knockback)
        {
            knockback = false;
            rb.velocity = new Vector2(0.0f, rb.velocity.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Removed ?");


        if (collision.tag == "FallDetector")
        //if (collision.tag == "FallSensor")
        {
            transform.position = respawnPoint;
            slotObj.RemoveItem();
            //Debug.Log("Removed");
        }
        //Debug.Log("Not Removed");

    }


    /*public void GameOverScreen()
    {

        GameOver.Setup();
    }*/

    /*void TakeDamage(int damage)
    {


        if (currentHealth > 0)
        {
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);
        }

        else
        {
            currentHealth = 0;
        }
    }*/


    private void FixedUpdate()
    {
        ApplyMovement();
        CheckSurroundings();
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

    /*private void CheckLedgeClimb()
    {
        if (ledgeDetected && !canClimbLedge)
        {
            canClimbLedge = true;

            if (isFacingRight)
            {
                ledgePos1 = new Vector2(Mathf.Floor(ledgePosBot.x + wallCheckDistance) - ledgeClimbXOffset1, Mathf.Floor(ledgePosBot.y) + ledgeClimbYOffset1);
                ledgePos2 = new Vector2(Mathf.Floor(ledgePosBot.x + wallCheckDistance) + ledgeClimbXOffset2, Mathf.Floor(ledgePosBot.y) + ledgeClimbYOffset2);
            }
            else
            {
                ledgePos1 = new Vector2(Mathf.Ceil(ledgePosBot.x - wallCheckDistance) + ledgeClimbXOffset1, Mathf.Floor(ledgePosBot.y) + ledgeClimbYOffset1);
                ledgePos2 = new Vector2(Mathf.Ceil(ledgePosBot.x - wallCheckDistance) - ledgeClimbXOffset2, Mathf.Floor(ledgePosBot.y) + ledgeClimbYOffset2);
            }

            canMove = false;
            canFlip = false;

            anim.SetBool("canClimbLedge", canClimbLedge);
        }

        if (canClimbLedge)
        {
            transform.position = ledgePos1;
        }
    }*/

    /*public void FinishLedgeClimb()
    {
        canClimbLedge = false;
        transform.position = ledgePos2;
        canMove = true;
        canFlip = true;
        ledgeDetected = false;
        anim.SetBool("canClimbLedge", canClimbLedge);
    }*/

    private void CheckSurroundings()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);
        //isTouchingLedge = Physics2D.Raycast(ledgeCheck.position, transform.right, wallCheckDistance, whatIsGround);

        /*if (isTouchingWall && !isTouchingLedge && !ledgeDetected)
        {
            ledgeDetected = true;
            ledgePosBot = wallCheck.position;
        }*/
    }

    private void CheckIfCanJump()
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

    private void CheckMovementDirection()
    {
        if (isFacingRight && movementInputDirection < 0)
        {
            Flip();
        }
        else if (!isFacingRight && movementInputDirection > 0)
        {
            Flip();
        }

        if (Mathf.Abs(rb.velocity.x) >= 0.01f)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
    }

    private void UpdateAnimations()
    {
        anim.SetBool("isWalking", isWalking);
        //anim.SetBool("isGrounded", isGrounded);
        //anim.SetFloat("yVelocity", rb.velocity.y);
        anim.SetBool("isWallSliding", isWallSliding);
    }

    private void CheckInput()
    {
        movementInputDirection = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded || (amountOfJumpsLeft > 0 && !isTouchingWall))
            {
                NormalJump();
                jumpSoundEffect.Play();
            }
            else
            {
                jumpTimer = jumpTimerSet;
                isAttemptingToJump = true;
            }
        }

        if (Input.GetButtonDown("Horizontal") && isTouchingWall)
        {
            if (!isGrounded && movementInputDirection != facingDirection)
            {
                canMove = false;
                canFlip = false;

                turnTimer = turnTimerSet;
                //walkEffect.Play();
            }
        }

        if (turnTimer >= 0)
        {
            turnTimer -= Time.deltaTime;

            if (turnTimer <= 0)
            {
                canMove = true;
                canFlip = true;
            }
        }

        if (checkJumpMultiplier && !Input.GetButton("Jump"))
        {
            checkJumpMultiplier = false;
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * variableJumpHeightMultiplier);
        }

        /*if (Input.GetButtonDown("Dash"))
        {
            if (Time.time >= (lastDash + dashCoolDown))
                AttemptToDash();
        }*/

    }

    /*private void AttemptToDash()
    {
        isDashing = true;
        dashTimeLeft = dashTime;
        lastDash = Time.time;

        PlayerAfterImagePool.Instance.GetFromPool();
        lastImageXpos = transform.position.x;
    }*/

    public int GetFacingDirection()
    {
        return facingDirection;
    }

    /*private void CheckDash()
    {
        if (isDashing)
        {
            if (dashTimeLeft > 0)
            {
                canMove = false;
                canFlip = false;
                rb.velocity = new Vector2(dashSpeed * facingDirection, 0.0f);
                dashTimeLeft -= Time.deltaTime;

                if (Mathf.Abs(transform.position.x - lastImageXpos) > distanceBetweenImages)
                {
                    PlayerAfterImagePool.Instance.GetFromPool();
                    lastImageXpos = transform.position.x;
                }
            }

            if (dashTimeLeft <= 0 || isTouchingWall)
            {
                isDashing = false;
                canMove = true;
                canFlip = true;
            }

        }
    }*/

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
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
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
            canMove = true;
            canFlip = true;
            hasWallJumped = true;
            wallJumpTimer = wallJumpTimerSet;
            lastWallJumpDirection = -facingDirection;

        }
    }

    private void ApplyMovement()
    {

        if (!isGrounded && !isWallSliding && movementInputDirection == 0)
        {
            rb.velocity = new Vector2(rb.velocity.x * airDragMultiplier, rb.velocity.y);
        }
        else if (canMove)
        {
            rb.velocity = new Vector2(movementSpeed * movementInputDirection, rb.velocity.y);
        }


        if (isWallSliding)
        {
            if (rb.velocity.y < -wallSlideSpeed)
            {
                rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
            }
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
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);

        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y, wallCheck.position.z));
    }
}
