using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDebugUtils : MonoBehaviour
{

    private PlayerHealth health;

    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<PlayerHealth>();
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
    }
}
