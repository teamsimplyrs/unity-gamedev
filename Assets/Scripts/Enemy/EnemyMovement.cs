using Pathfinding;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    float horizontal;
    float vertical;
    public float Speed;
    Rigidbody2D rb;
    SpriteRenderer sprite;
    Animator anim;
    AIPath pathfinder;

    public string currentDir;
    private string currentState;
    private bool isWalking;

    private const string ENEMY_IDLE_DOWN = "enemy_idle_down";
    private const string ENEMY_IDLE_SIDE = "enemy_idle_side";
    private const string ENEMY_IDLE_UP = "enemy_idle_up";
    private const string WALK_DOWN = "enemy_walk_down";
    private const string WALK_UP = "enemy_walk_up";
    private const string WALK_SIDE = "enemy_walk_side";

    // Start is called before the first frame update
    void Start()
    {
        currentDir = "down";
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        pathfinder = GetComponent<AIPath>();
    }

    // Update is called once per frame
    void Update()
    {
        EnemyAnimationLogic();
        //Debug.Log(pathfinder.velocity.ToString());
    }


    void SetIdleAnimationState(string direction)
    {
        switch (direction)
        {
            case "down":
                ChangeAnimationState(ENEMY_IDLE_DOWN);
                break;

            case "up":
                ChangeAnimationState(ENEMY_IDLE_UP);
                break;

            case "right":
                ChangeAnimationState(ENEMY_IDLE_SIDE);
                sprite.flipX = false;
                break;

            case "left":
                ChangeAnimationState(ENEMY_IDLE_SIDE);
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

    void EnemyAnimationLogic()
    {
        horizontal = pathfinder.velocity.x;
        vertical = pathfinder.velocity.y;


        if (horizontal < 0 && Mathf.Abs(vertical) < Mathf.Abs(horizontal))
        {
            currentDir = "left";
        }
        else if (vertical < 0 && Mathf.Abs(vertical) > Mathf.Abs(horizontal))
        {
            currentDir = "down";
        }
        else if (horizontal > 0 && Mathf.Abs(vertical) < Mathf.Abs(horizontal))
        {
            currentDir = "right";
        }
        else if (vertical > 0 && Mathf.Abs(vertical) >Mathf.Abs(horizontal))
        {
            currentDir = "up";
        }

        isWalking = !(horizontal == 00 && vertical == 0);

        if (pathfinder.canMove)
        {
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
        }
    }




}
