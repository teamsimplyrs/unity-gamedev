using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotDamageHandler : MonoBehaviour, IDamageHandler
{

    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage()
    {
        Destroy(gameObject);
    }

    public void hit()
    {
        anim.SetTrigger("hit");
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }
}
