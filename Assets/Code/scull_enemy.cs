using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scull_enemy : MonoBehaviour
{
    public float speed = 1f;
    float direction = -1f;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(speed * direction, GetComponent<Rigidbody2D>().velocity.y);
        transform.localScale = new Vector3(direction, 1, 1);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "limiter" || col.gameObject.tag == "spikes")
            direction *= -1f;
    }
}
