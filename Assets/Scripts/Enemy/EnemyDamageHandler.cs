using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class EnemyDamageHandler : MonoBehaviour, IDamageHandler
{
    Rigidbody2D rb;
    SpriteRenderer sprite;
    EnemyHealth health;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        health = GetComponent<EnemyHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void hit(GameObject hitter, float val)
    {
        Vector2 direction = transform.position - hitter.transform.position;
        float dirX = direction.x;
        float dirY = direction.y;
        if (Mathf.Abs(dirX) > Mathf.Abs(dirY)){
            direction = new Vector2(dirX, 0);
        }
        else if(Mathf.Abs(dirY) > Mathf.Abs(dirX)){
            direction = new Vector2(0, dirY);
        }
        rb.velocity = direction * 5f;
        sprite.color = Color.red;
        health.TakeDamage((int)val);
        Invoke("ResetSprite", 0.1f);
    }   

    void ResetSprite()
    {
        sprite.color = Color.white;
    }
}
