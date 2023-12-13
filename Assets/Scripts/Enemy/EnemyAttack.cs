using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using static UnityEngine.ParticleSystem;

public class EnemyAttack : MonoBehaviour
{

    GameObject enemyHandObject;
    EnemyMovement movement;
    Animator swordAnimator;
    string localCurrentDir;
    bool attacking;
    public AnimationClip spinClip;
    public AnimationClip swingClip;
    private EnemyWeapon weapon;

    public bool canPlayerAttack;

    // Start is called before the first frame update
    void Start()
    {
        enemyHandObject = transform.Find("Hand").gameObject;
        enemyHandObject.SetActive(false);
        movement = GetComponent<EnemyMovement>();
        localCurrentDir = movement.currentDir;
        swordAnimator = enemyHandObject.GetComponent<Animator>();
        weapon = GetComponent<EnemyWeapon>();
        canPlayerAttack = true;
    }

    // Call to initiate an attack from another class
    public void InitiateAttack()
    {
        Debug.Log("enemy attack initiated");
        Debug.Log("enemy weapon is " + weapon.GetWeapon());
        if (weapon.GetWeapon() != null)
        {
            Debug.Log("enemy has weapon");
            enemyHandObject.SetActive(true);
            attacking = swordAnimator.GetBool("attacking");
            if (localCurrentDir != movement.currentDir && !attacking)
            {
                localCurrentDir = movement.currentDir;
                switch (movement.currentDir)
                {
                    case "down":
                        enemyHandObject.transform.rotation = Quaternion.Euler(0, 0, 0f);
                        break;

                    case "up":
                        enemyHandObject.transform.rotation = Quaternion.Euler(0, 0, 180f);
                        break;

                    case "right":
                        enemyHandObject.transform.rotation = Quaternion.Euler(0, 0, 90f);
                        break;

                    case "left":
                        enemyHandObject.transform.rotation = Quaternion.Euler(0, 0, 270f);
                        break;

                    default:
                        enemyHandObject.transform.rotation = Quaternion.Euler(0, 0, 0f);
                        break;
                }

                GameObject sword_sprite = enemyHandObject.transform.GetChild(0).GetChild(0).gameObject;
                if (movement.currentDir == "up")
                {
                    sword_sprite.transform.localPosition = new Vector3(0, -1.2f, 0);
                }
                else
                {
                    sword_sprite.transform.localPosition = new Vector3(0, -0.8f, 0);
                }
            }
            enemyHandObject.GetComponent<Animator>().SetTrigger("attacking");
        }
    }
}
