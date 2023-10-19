using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed = 3.0f; // Movement speed of the enemy
    [SerializeField] bool chasing;
    private Transform playerTransform;
    private Transform enemyTransform; // This is the main enemy (parent) transform
    Rigidbody2D rb;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        enemyTransform = transform.parent; // get the parent transform
        rb = enemyTransform.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            chasing = true;
            StartCoroutine(ChasePlayer());
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            chasing = false;
            rb.velocity = Vector3.zero;
            StopCoroutine(ChasePlayer());
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