using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float horizontal;
    float vertical;
    public float Speed;
    Rigidbody2D rb;
    SpriteRenderer sprite;
    Animator anim;
    AudioSource footstep;
    GameObject particleComponent;

    public string currentDir;
    private string currentState;
    private bool isWalking;
    private bool isRunning;
    public bool playerInteracting;

    private const string PLAYER_IDLE_DOWN = "player_idle_down";
    private const string PLAYER_IDLE_SIDE = "player_idle_side";
    private const string PLAYER_IDLE_UP = "player_idle_up";
    private const string WALK_DOWN = "player_walk_down";
    private const string WALK_UP = "player_walk_up";
    private const string WALK_SIDE = "player_walk_side";

    // Start is called before the first frame update
    void Start()
    {
        currentDir = "down";
        playerInteracting = false;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        footstep = GetComponent<AudioSource>();
        footstep.enabled = false;
        particleComponent = transform.GetChild(1).gameObject;
        particleComponent.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerAnimationLogic();

        if (isRunning)
        {
            anim.speed = 2;
            Speed = 6;
            footstep.pitch = 2;
        }
        else
        {
            anim.speed = 1;
            Speed = 3;
            footstep.pitch = 1;
        }

        PlayerParticleControl();
    }
    private void FixedUpdate()
    {
        if (!playerInteracting)
        {
            rb.velocity = new Vector2(horizontal * Speed, vertical * Speed);
        }
    }

    void SetIdleAnimationState(string direction)
    {
        switch (direction)
        {
            case "down":
                ChangeAnimationState(PLAYER_IDLE_DOWN);
                break;

            case "up":
                ChangeAnimationState(PLAYER_IDLE_UP);
                break;

            case "right":
                ChangeAnimationState(PLAYER_IDLE_SIDE);
                sprite.flipX = false;
                break;

            case "left":
                ChangeAnimationState(PLAYER_IDLE_SIDE);
                sprite.flipX = true;
                break;

        }
    }

    void ChangeAnimationState(string newState)
    {
        if(currentState == newState) return;

        anim.Play(newState);

        currentState = newState;
    }

    void PlayerAnimationLogic()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        isRunning = Input.GetKey(KeyCode.LeftShift);

        if (horizontal < 0)
        {
            currentDir = "left";
        }
        else if (vertical < 0)
        {
            currentDir = "down";
        }
        else if (horizontal > 0)
        {
            currentDir = "right";
        }
        else if (vertical > 0)
        {
            currentDir = "up";
        }

        isWalking = !(horizontal == 00 && vertical == 0);

        if (isWalking)
        {
            if (!footstep.enabled)
            {
                footstep.enabled = true;
            }
            switch (currentDir)
            {
                case "down":
                    ChangeAnimationState(WALK_DOWN);
                    break;

                case "up":
                    ChangeAnimationState(WALK_UP);
                    break;

                case "right":
                    ChangeAnimationState(WALK_SIDE);
                    sprite.flipX = false;
                    break;

                case "left":
                    ChangeAnimationState(WALK_SIDE);
                    sprite.flipX = true;
                    break;

                default:
                    SetIdleAnimationState(currentDir);
                    break;
            }
        }
        else
        {
            SetIdleAnimationState(currentDir);
            footstep.enabled = false;
        }
    }

    void PlayerParticleControl()
    {
        if(rb.velocity == Vector2.zero)
        {
            particleComponent.SetActive(false);
        }
        else
        {
            particleComponent.SetActive(true);
        }
    }

    



}
