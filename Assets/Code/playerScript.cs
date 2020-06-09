using UnityEngine;
using System.Collections;
using System;
using System.Threading;

public class playerScript : MonoBehaviour
{
    private Animator animator;

    [SerializeField] float maxSpeed = default;
    [SerializeField] float jumpForce = default;
    private bool facingRight = true;
    private bool grounded = false;
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
        lives = 3;
        health = 100;
        gems = 0;

        animator = GetComponent<Animator>();
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        respawnPoint = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        move = Input.GetAxis("Horizontal");
        killPain();
    }

    void Update()
    {
        if (grounded && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)))
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));
            grounded = false;
        }
        GetComponent<Rigidbody2D>().velocity = new Vector2(move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

        if (move > 0 && !facingRight)
        {
            Flip();
        }
        else if (move < 0 && facingRight)
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

        animator.SetFloat("Speed", Math.Abs(GetComponent<Rigidbody2D>().velocity.x));
        animator.SetBool("onGround", Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround));
    }

    void Flip()
    {
        facingRight = !facingRight;
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
        if (col.gameObject.tag == "checkpoint")
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
            // SceneManager.LoadScene("second_level");
            Application.LoadLevel("second_level");
        if (col.gameObject.tag == "lifePotion")
        {
            lives += 1;
            Destroy(col.gameObject);
        }
        if (col.gameObject.tag == "gem")
        {
            gems += 1;
            Destroy(col.gameObject);
        }
        if (col.gameObject.tag == "jam")
        {
            health = 100;
            Destroy(col.gameObject);
        }
        if (col.gameObject.tag == "strawberry")
        {
            if (health < 100)
                health += 10;
            Destroy(col.gameObject);
        }
        if (col.gameObject.tag == "scull_enemy")
        {
            if (health > 20)
            {
                health -= 20;
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
        if (col.gameObject.tag == "turtle_enemy")
        {
            if (health > 50)
            {
                health -= 50;
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