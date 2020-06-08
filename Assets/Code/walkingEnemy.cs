﻿using UnityEngine;
using System.Collections;
using System.Threading;

public class walkingEnemy : MonoBehaviour
{
    public float speed = 1f;
    float direction = -1f;
    public bool angry = false;
    Timer timer;
    private Animator animator;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        TimerCallback callBack = new TimerCallback(setTurtleState);
        timer = new Timer(callBack, null, 0, 3000);
    }

    void setTurtleState(object obj)
    {
        angry = !angry;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(speed * direction, GetComponent<Rigidbody2D>().velocity.y);
        transform.localScale = new Vector3(direction, 1, 1);
        animator.SetBool("angry", angry);
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "limiter" || col.gameObject.tag == "spikes")
            direction *= -1f;
    }

    void OnCollision2D(Collision2D col)
    {
       
    }
}