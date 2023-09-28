using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class PlayerAttack : MonoBehaviour
{

    GameObject sword;
    PlayerMovement movement;
    string local_current_dir;
    bool attacking;

    // Start is called before the first frame update
    void Start()
    {
        sword = transform.Find("Hand").gameObject;
        sword.SetActive(false);
        movement = GetComponent<PlayerMovement>();
        local_current_dir = movement.current_dir;
    }

    // Update is called once per frame
    void Update()
    {
        attacking = sword.GetComponent<Animator>().GetBool("attacking");
        if (local_current_dir != movement.current_dir && !attacking)
        {
            local_current_dir = movement.current_dir;
            switch (movement.current_dir)
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
            if (movement.current_dir == "up")
            {
                sword_sprite.transform.localPosition = new Vector3(0, -1.2f, 0);
            }
            else
            {
                sword_sprite.transform.localPosition = new Vector3(0, -0.8f, 0);
            }
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            sword.SetActive(true);
            sword.GetComponent<Animator>().SetTrigger("attacking");
        }
    }
}
