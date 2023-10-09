using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;
using System.Collections.Generic;

public class PlayerAttack : MonoBehaviour
{

    GameObject playerHandObject;
    PlayerMovement movement;
    Animator swordAnimator;
    AnimatorOverrideController overrideController;
    string local_current_dir;
    bool attacking;
    public AnimationClip spinClip;
    public AnimationClip swingClip;

    // Start is called before the first frame update
    void Start()
    {
        playerHandObject = transform.Find("Hand").gameObject;
        playerHandObject.SetActive(false);
        movement = GetComponent<PlayerMovement>();
        local_current_dir = movement.currentDir;
        swordAnimator = playerHandObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            playerHandObject.SetActive(true);
            attacking = swordAnimator.GetBool("attacking");
            if (local_current_dir != movement.currentDir && !attacking)
            {
                local_current_dir = movement.currentDir;
                switch (movement.currentDir)
                {
                    case "down":
                        playerHandObject.transform.rotation = Quaternion.Euler(0, 0, 0f);
                        break;

                    case "up":
                        playerHandObject.transform.rotation = Quaternion.Euler(0, 0, 180f);
                        break;

                    case "right":
                        playerHandObject.transform.rotation = Quaternion.Euler(0, 0, 90f);
                        break;

                    case "left":
                        playerHandObject.transform.rotation = Quaternion.Euler(0, 0, 270f);
                        break;

                    default:
                        playerHandObject.transform.rotation = Quaternion.Euler(0, 0, 0f);
                        break;
                }

                GameObject sword_sprite = playerHandObject.transform.GetChild(0).GetChild(0).gameObject;
                if (movement.currentDir == "up")
                {
                    sword_sprite.transform.localPosition = new Vector3(0, -1.2f, 0);
                }
                else
                {
                    sword_sprite.transform.localPosition = new Vector3(0, -0.8f, 0);
                }
            }
            playerHandObject.GetComponent<Animator>().SetTrigger("attacking");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageHandler damageHander = collision.GetComponent<IDamageHandler>();
        if(damageHander != null)
        {
            damageHander.hit(gameObject);
        }
    }
}
