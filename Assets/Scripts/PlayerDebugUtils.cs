using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDebugUtils : MonoBehaviour
{

    private PlayerHealth health;
    private PlayerMovement movement;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<PlayerHealth>();
        rb = GetComponent<Rigidbody2D>();
        movement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            health.TakeDamage(1);
        }
        else if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            health.TakeDamage(-1);
        }

        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            movement.playerInteracting = true;
            rb.velocity = Vector2.right * 10;
            Invoke("NotInteracting", 0.1f);
        }
    }

    void NotInteracting()
    {
        movement.playerInteracting = false;
    }
}
