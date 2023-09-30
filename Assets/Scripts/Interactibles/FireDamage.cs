using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDamage : MonoBehaviour
{
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerHealth player_hp = collision.gameObject.GetComponent<PlayerHealth>();
            PlayerMovement movement = collision.gameObject.GetComponent<PlayerMovement>();
            SpriteRenderer sprite = collision.gameObject.GetComponent<SpriteRenderer>();
            string player_dir = collision.gameObject.GetComponent<PlayerMovement>().currentDir;
            Rigidbody2D player_rb = collision.gameObject.GetComponent<Rigidbody2D>();
            player_hp.TakeDamage(1);
            movement.playerInteracting = true;
            Vector2 direction = player_rb.position - (Vector2)transform.position;
            direction.Normalize();
            player_rb.velocity = direction * 5f;
            sprite.color = Color.red;
            StartCoroutine(NotInteracting(movement,sprite));

        }
    }

    private IEnumerator NotInteracting(PlayerMovement movement, SpriteRenderer sprite)
    {
        yield return new WaitForSeconds(0.2f);
        movement.playerInteracting = false;
        sprite.color = Color.white;
    }
}
