using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
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
            if (!player_hp.IsAtFullHP())
            {
                player_hp.TakeDamage(-1);
                Destroy(gameObject);
            }
        }
    }
}
