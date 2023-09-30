using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSword : MonoBehaviour
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
            GameObject playerSword = collision.gameObject.transform.GetChild(0).GetChild(0).GetChild(0).gameObject;
            SpriteRenderer playerSwordSprite = playerSword.GetComponent<SpriteRenderer>();
            playerSwordSprite.sprite = GetComponent<SpriteRenderer>().sprite;
            Destroy(gameObject);
        }
    }
}
