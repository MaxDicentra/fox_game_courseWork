using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scull_enemy : MonoBehaviour
{
    private float speed = 1f;
    private float direction = -1f;
    private Animator animator;
    private Rigidbody2D rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rigidBody.velocity = new Vector2(speed * direction, rigidBody.velocity.y);
        transform.localScale = new Vector3(direction, 1, 1);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("limiter") || col.gameObject.CompareTag("spikes"))
        {
            direction *= -1f;
        }
    }
}
