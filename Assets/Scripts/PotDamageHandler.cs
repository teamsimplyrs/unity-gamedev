using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D;
using UnityEngine;

public class PotDamageHandler : MonoBehaviour, IDamageHandler
{

    Animator anim;
    AudioSource sound;
    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        sound = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void HidePot()
    {
        spriteRenderer.enabled = false;
        boxCollider.enabled = false;
    }

    public void Damage()
    {
        Destroy(gameObject);
    }

    public void hit()
    {
        anim.SetTrigger("hit");
        sound.Play();
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }
}
