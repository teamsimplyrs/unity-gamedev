using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSword : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] ItemSO sword;
    SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sword.ItemSprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject playerSword = collision.gameObject.transform.GetChild(0).GetChild(0).GetChild(0).gameObject;
            SpriteRenderer playerSwordSprite = playerSword.GetComponent<SpriteRenderer>();
            playerSwordSprite.sprite = sword.ItemSprite;
            Destroy(gameObject);
        }
    }
}
