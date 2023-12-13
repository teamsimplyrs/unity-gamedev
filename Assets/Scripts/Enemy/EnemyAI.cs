using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed = 3.0f; 
    [SerializeField] bool chasing;
    private Transform playerTransform;
    private Transform enemyTransform;
    private EnemyDamageHandler enemyDamageHandler;
    private EnemyAttack enemyAttackHandler;
    private bool insideDetectionRange;
    Rigidbody2D rb;
    AIPath pathfinder;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        enemyTransform = transform.parent;
        rb = enemyTransform.GetComponent<Rigidbody2D>();
        pathfinder = enemyTransform.GetComponent<AIPath>();
        enemyDamageHandler = enemyTransform.GetComponent<EnemyDamageHandler>();
        enemyAttackHandler = enemyTransform.GetComponent<EnemyAttack>();
        pathfinder.canMove = false;
        pathfinder.canSearch = false;
    }

    private void Update()
    {
        float distanceToPlayer = Vector2.Distance(enemyTransform.position, playerTransform.position);

        if (insideDetectionRange)
        {
            if (distanceToPlayer < 1.5f)
            {
                chasing = false;
                enemyAttackHandler.InitiateAttack();
                pathfinder.canMove = false;
            }
            else
            {
                if (enemyDamageHandler.isStunned == false)
                {
                    pathfinder.canMove = true;
                }
            }
        }

       
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            chasing = true;
            pathfinder.canMove = true;
            pathfinder.canSearch = true;
            insideDetectionRange=true;
            //StartCoroutine(ChasePlayer());
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            chasing = false;
            rb.velocity = Vector3.zero;
            pathfinder.canMove = false;
            pathfinder.canSearch = false;
            insideDetectionRange=false;
            //StopCoroutine(ChasePlayer());
        }
    }

    IEnumerator ChasePlayer()   
    {
        //while (Vector2.Distance(enemyTransform.position, playerTransform.position) > 1f)
        //{
        while (chasing)
        {
            Vector2 direction = (playerTransform.position - enemyTransform.position).normalized;
            rb.velocity = direction * speed;
            //enemyTransform.position += (Vector3)direction * speed * Time.deltaTime;
            if(Vector2.Distance(enemyTransform.position, playerTransform.position) < 1f)
            {
                chasing = false;
                rb.velocity = Vector2.zero;
            }
            yield return null;
        }
        yield return null;
        //}
    }
}