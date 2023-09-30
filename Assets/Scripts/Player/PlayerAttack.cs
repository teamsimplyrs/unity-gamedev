using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class PlayerAttack : MonoBehaviour
{

    GameObject sword;
    PlayerMovement movement;
    Animator swordAnimator;
    string local_current_dir;
    bool attacking;

    // Start is called before the first frame update
    void Start()
    {
        sword = transform.Find("Hand").gameObject;
        sword.SetActive(false);
        movement = GetComponent<PlayerMovement>();
        local_current_dir = movement.currentDir;
        swordAnimator = sword.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            sword.SetActive(true);
            attacking = swordAnimator.GetBool("attacking");
            if (local_current_dir != movement.currentDir && !attacking)
            {
                local_current_dir = movement.currentDir;
                switch (movement.currentDir)
                {
                    case "down":
                        sword.transform.rotation = Quaternion.Euler(0, 0, 0f);
                        break;

                    case "up":
                        sword.transform.rotation = Quaternion.Euler(0, 0, 180f);
                        break;

                    case "right":
                        sword.transform.rotation = Quaternion.Euler(0, 0, 90f);
                        break;

                    case "left":
                        sword.transform.rotation = Quaternion.Euler(0, 0, 270f);
                        break;

                    default:
                        sword.transform.rotation = Quaternion.Euler(0, 0, 0f);
                        break;
                }

                GameObject sword_sprite = sword.transform.GetChild(0).GetChild(0).gameObject;
                if (movement.currentDir == "up")
                {
                    sword_sprite.transform.localPosition = new Vector3(0, -1.2f, 0);
                }
                else
                {
                    sword_sprite.transform.localPosition = new Vector3(0, -0.8f, 0);
                }
            }
            sword.GetComponent<Animator>().SetTrigger("attacking");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageHandler damageHander = collision.GetComponent<IDamageHandler>();
        if(damageHander != null)
        {
            damageHander.hit();
        }
    }
}
