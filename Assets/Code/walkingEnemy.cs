using UnityEngine;
using System.Collections;
using System.Threading;

public class walkingEnemy : MonoBehaviour
{
    private float speed = 1f;
    private float direction = -1f;
    private bool isAngry = false;
    private Timer timer;
    private Animator animator;
    private Rigidbody2D rigidbody;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        TimerCallback callBack = new TimerCallback(setTurtleState);
        timer = new Timer(callBack, null, 0, 3000);
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void setTurtleState(object obj)
    {
        isAngry = !isAngry;
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody.velocity = new Vector2(speed * direction, rigidbody.velocity.y);
        transform.localScale = new Vector3(direction, 1, 1);
        animator.SetBool("angry", isAngry);
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "limiter" || col.gameObject.tag == "spikes")
        {
            direction *= -1f; 
        }
    }
}