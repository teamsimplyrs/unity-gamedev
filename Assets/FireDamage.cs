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
        Debug.Log("Fire collision tick");
        if (collision.gameObject.tag == "Player")
        {
            PlayerHealth player_hp = collision.gameObject.GetComponent<PlayerHealth>();
            string player_dir = collision.gameObject.GetComponent<PlayerMovement>().current_dir;
            Rigidbody2D player_rb = collision.gameObject.GetComponent<Rigidbody2D>();

            player_hp.TakeDamage(1);

            player_rb.velocity = player_dir switch
            {
                "up" => new Vector2(0f, -3f),
                "left" => new Vector2(3f, 0f),
                "right" => new Vector2(-3f, 0f),
                "down" => new Vector2(0f, 3f),
                _ => new Vector2(0f, -3f)
            };
        }
    }
}
