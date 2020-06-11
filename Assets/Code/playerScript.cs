using UnityEngine;
using System.Collections;
using System;
using System.Threading;

public class playerScript : MonoBehaviour
{
    private const int MAX_HEALTH = 100;
    private const int TURTLE_HIT = 50;
    private const int SCULL_HIT = 20;
    private const int STRAWBERRY_ADDITION = 10;
    private const int LIFE_POTION_ADDITION = 1;
    private const int GEM_ADDITION = 1;
    private const int START_LIVES_AMOUNT = 3;
    private const int START_GEMS_AMOUNT = 0;

    private Animator animator;

    [SerializeField] Rigidbody2D rigidBody;
    [SerializeField] float maxSpeed = default;
    [SerializeField] float jumpForce = default;
    private bool isFacingRight = true;
    [SerializeField] bool isGrounded = false;
    [SerializeField] Transform groundCheck = default;
    private float groundRadius = 0.2f;
    [SerializeField] LayerMask whatIsGround;
    private Timer timer;

    private SpriteRenderer sprite;
    private float move;
    private Vector3 respawnPoint;

    private int lives;
    private int health;
    private int gems;

    public int Lives
    {
        get
        {
            return lives;
        }
    }

    public int Health
    {
        get
        {
            return health;
        }
    }

    public int Gems
    {
        get
        {
            return gems;
        }
    }

    // Use this for initialization
    void Start()
    {
        lives = START_LIVES_AMOUNT ;
        health = MAX_HEALTH;
        gems = START_GEMS_AMOUNT;

        animator = GetComponent<Animator>();
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        rigidBody = GetComponent<Rigidbody2D>();
        respawnPoint = transform.position;

        PlayerInstance.setInstance(this);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        animator.SetBool("grounded",isGrounded);
        move = Input.GetAxis("Horizontal");
        killPain();
    }

    void Update()
    {
        if (isGrounded && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)))
        {
            rigidBody.AddForce(new Vector2(0f, jumpForce));
            isGrounded = false;
            animator.SetBool("grounded", isGrounded);
        }
        rigidBody.velocity = new Vector2(move * maxSpeed, rigidBody.velocity.y);

        if (move > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (move < 0 && isFacingRight)
        {
            Flip();
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (Input.GetKey(KeyCode.R))
        {
            Application.LoadLevel(Application.loadedLevel);
        }

        animator.SetFloat("Speed", Math.Abs(rigidBody.velocity.x));
        animator.SetBool("onGround", Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround));
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "offWay")
        {
            if (lives > 1)
            {
                lives -= 1;
                transform.position = respawnPoint;
            }
            else
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }
        if (col.GetComponent<CheckpointController>() != null)
        {
            respawnPoint = col.transform.position;
        }
        if (col.gameObject.tag == "spikes")
        {
            if (lives > 1)
            {
                lives -= 1;
                transform.position = respawnPoint;
            }
            else
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }
        if (col.gameObject.tag == "door")
        {
            Application.LoadLevel("second_level");
        }
        if (col.gameObject.tag == "lifePotion")
        {
            lives += LIFE_POTION_ADDITION;
            col.gameObject.SetActive(false);
        }
        if (col.gameObject.tag == "gem")
        {
            gems += GEM_ADDITION;
            col.gameObject.SetActive(false);
        }
        if (col.gameObject.tag == "jam")
        {
            health = MAX_HEALTH;
            col.gameObject.SetActive(false);
        }
        if (col.gameObject.tag == "strawberry")
        {
            if (health < MAX_HEALTH)
            {
                health += STRAWBERRY_ADDITION; 
            }
            col.gameObject.SetActive(false);
        }
        if (col.GetComponent<scull_enemy>() != null)
        {
            if (health > SCULL_HIT)
            {
                health -= SCULL_HIT;
            }
            else
            {
                if (lives > 1)
                {
                    lives -= 1;
                    transform.position = respawnPoint;
                }
                else
                {
                    Application.LoadLevel(Application.loadedLevel);
                }
            }
            animator.SetInteger("isHurt", 1);
        }
        if (col.GetComponent<walkingEnemy>() != null)
        {
            if (health > TURTLE_HIT)
            {
                health -= TURTLE_HIT;
            }
            else
            {
                if (lives > 1)
                {
                    lives -= 1;
                    transform.position = respawnPoint;
                }
                else
                {
                    Application.LoadLevel(Application.loadedLevel);
                }

            }
            animator.SetInteger("isHurt", 1);
        }
    }

    void killPain()
    {
        animator.SetInteger("isHurt", 0);
    }
}