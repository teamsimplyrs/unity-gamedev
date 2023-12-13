using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class PlayerDamageHandler : MonoBehaviour, IDamageHandler
{
    Rigidbody2D rb;
    SpriteRenderer sprite;
    PlayerHealth health;
    PlayerMovement movement;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        health = GetComponent<PlayerHealth>();
        movement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void hit(GameObject hitter, float val)
    {
        Debug.Log("player damage handler hit called");
        Vector2 direction = transform.position - hitter.transform.position;
        direction.Normalize();
        health.TakeDamage((int)val);
        movement.playerInteracting = true;
        rb.velocity = direction * 5f;
        sprite.color = Color.red;
        StartCoroutine(NotInteracting(movement, sprite));
    }   

    private IEnumerator NotInteracting(PlayerMovement movement, SpriteRenderer sprite)
    {
        yield return new WaitForSeconds(0.2f);
        movement.playerInteracting = false;
        sprite.color = Color.white;
    }
}
