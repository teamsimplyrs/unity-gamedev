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

    private string current_dir;
    private string currentState;
    private bool isWalking;
    private bool isRunning;

    private const string PLAYER_IDLE_DOWN = "player_idle_down";
    private const string PLAYER_IDLE_SIDE = "player_idle_side";
    private const string PLAYER_IDLE_UP = "player_idle_up";
    private const string WALK_DOWN = "player_walk_down";
    private const string WALK_UP = "player_walk_up";
    private const string WALK_SIDE = "player_walk_side";

    // Start is called before the first frame update
    void Start()
    {
        current_dir = "down";
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        if(Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }


        if(horizontal == 0 && vertical == 0)
        {
            isWalking = false;
            if (current_dir == "down")
            {
                ChangeAnimationState(PLAYER_IDLE_DOWN);
            }
            else if (current_dir == "up")
            {
                ChangeAnimationState(PLAYER_IDLE_UP);
            }
            else if (current_dir == "right")
            {
                ChangeAnimationState(PLAYER_IDLE_SIDE);
                sprite.flipX = false;
            }
            else if (current_dir == "left")
            {
                ChangeAnimationState(PLAYER_IDLE_SIDE);
                sprite.flipX = true;
            }
        }
        else
        {
            isWalking = true;
        }

        if (isWalking)
        {
            if(rb.velocity.y < 0.0f)
            {
                ChangeAnimationState(WALK_DOWN);
                current_dir = "down";
            }
            else if(rb.velocity.y > 0.0f)
            {
                ChangeAnimationState(WALK_UP);
                current_dir = "up";
            }
            else if(rb.velocity.x > 0.0f)
            {
                ChangeAnimationState(WALK_SIDE);
                sprite.flipX = false;
                current_dir = "right";
            }
            else if(rb.velocity.x < 0.0f)
            {
                ChangeAnimationState(WALK_SIDE);
                sprite.flipX = true;
                current_dir = "left";
            }
            else
            {
                if(current_dir == "down")
                {
                    ChangeAnimationState(PLAYER_IDLE_DOWN);
                }
                else if(current_dir == "up")
                {
                    ChangeAnimationState(PLAYER_IDLE_UP);
                }
                else if(current_dir == "right")
                {
                    ChangeAnimationState(PLAYER_IDLE_SIDE);
                    sprite.flipX = false;
                }
                else if(current_dir == "left")
                {
                    ChangeAnimationState(PLAYER_IDLE_SIDE);
                    sprite.flipX = true;
                }
            }
        }
        if (isRunning)
        {
            anim.speed = 2;
            Speed = 6;
        }
        else
        {
            anim.speed = 1;
            Speed = 3;
        }
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * Speed, vertical * Speed);
    }

    void ChangeAnimationState(string newState)
    {
        if(currentState == newState) return;

        anim.Play(newState);

        currentState = newState;
    }

    



}
