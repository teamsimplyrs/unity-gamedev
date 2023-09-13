using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float horizontal;
    float vertical;
    public float Speed;
    Rigidbody2D rb;
    SpriteRenderer sprite;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        Debug.Log(rb.velocity);
        if (rb.velocity.y < 0.0f)
        {
            anim.SetBool("walking_down", true);
        }
        else if (rb.velocity.y > 0.0f)
        {
            anim.SetBool("walking_down", false);
            anim.SetBool("walking_up", true);
        }
        else if (rb.velocity.x > 0.0f)
        {
            anim.SetBool("walking_right", true);
            sprite.flipX = false;
        }
        else if (rb.velocity.x < 0.0f)
        {
            anim.SetBool("walking_right", true);
            sprite.flipX = true;

        }
        if (rb.velocity.x == 0.0f)
        {
            anim.SetBool("walking_right", false);
        }
        if (rb.velocity.y == 0.0f)
        {
            Debug.Log("reset reached");
            anim.SetBool("walking_down", false);
            anim.SetBool("walking_up", false);
        }
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * Speed, vertical * Speed);
    }
}
