using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using static UnityEngine.ParticleSystem;

public class PlayerAttack : MonoBehaviour
{

    GameObject playerHandObject;
    PlayerMovement movement;
    Animator swordAnimator;
    AnimatorOverrideController overrideController;
    public GameObject critParticle;
    string local_current_dir;
    bool attacking;
    public AnimationClip spinClip;
    public AnimationClip swingClip;
    private PlayerWeapon weapon;

    public bool canPlayerAttack;

    // Start is called before the first frame update
    void Start()
    {
        playerHandObject = transform.Find("Hand").gameObject;
        playerHandObject.SetActive(false);
        movement = GetComponent<PlayerMovement>();
        local_current_dir = movement.currentDir;
        swordAnimator = playerHandObject.GetComponent<Animator>();
        weapon = GetComponent<PlayerWeapon>();
        canPlayerAttack = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && weapon.GetWeapon() != null && canPlayerAttack)
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

            //PrintWeaponCurrentState(weapon);
        }
    }

    private void PrintWeaponCurrentState(PlayerWeapon pWeapon)
    {
        foreach (var param in pWeapon.weaponCurrentState)
        {
            Debug.Log(param.itemParameter.ParameterName + ": " + param.value);
        }
    }

    private float calcPlayerDamage(float weaponBaseDamage, float critChance, float critMultiplier)
    {
        float finalDamage = weaponBaseDamage;
        
        if (critChance != 0)
        {
            int critProc;
            while (critChance > 0)
            {
                critProc = Random.Range(0, 100);
                Debug.Log("crit proc: " + critProc);
                if (critProc <= critChance)
                {
                    finalDamage += (weaponBaseDamage * critMultiplier);
                }
                critChance -= 100;
            }
        }

        return finalDamage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageHandler damageHandler = collision.GetComponent<IDamageHandler>();
        if (damageHandler != null && canPlayerAttack)
        {
            float weaponMeleeDamage = 1;
            float critChance = 0;
            float critMultiplier = 0;

            foreach (var param in weapon.weaponCurrentState)
            {
                switch (param.itemParameter.ParameterName)
                {
                    case "Melee Damage":
                        weaponMeleeDamage = param.value;
                        break;
                    case "Critical Chance":
                        critChance = param.value;
                        break;
                    case "Critical Multiplier":
                        critMultiplier = param.value;
                        break;
                }
            }
            Debug.Log("melee dmg: " + weaponMeleeDamage);
            Debug.Log("crit chance: " + critChance);
            Debug.Log("crit dmg: " + critMultiplier);


            float weaponFinalDamage = calcPlayerDamage(weaponMeleeDamage, critChance, critMultiplier);

            if (collision.CompareTag("Enemy"))
            {
                float critProcCount = (weaponFinalDamage - weaponMeleeDamage) / (weaponMeleeDamage * critMultiplier);
                Debug.Log("CritToNormalRatio: " + critProcCount);
                GameObject critParticleInstance = Instantiate(critParticle, collision.transform.position, Quaternion.identity);
                ParticleSystem particleSystem = critParticleInstance.GetComponent<ParticleSystem>();
                if (particleSystem != null)
                {
                    ParticleSystem.MainModule main = particleSystem.main;
                    ParticleSystem.EmissionModule emission = particleSystem.emission;
                    if (critProcCount == 1)
                    {
                        main.startColor = Color.yellow;
                        main.startSpeed = main.startSpeed.constantMax + 3f;
                        main.startSize = main.startSize.constant + 0.1f;
                    }
                    else if(critProcCount == 2) {
                        main.startColor = new MinMaxGradient(new Color32(252, 144, 3, 255)); //Orange
                        main.startSpeed = main.startSpeed.constantMax + 5f;
                        main.startSize = main.startSize.constant + 0.2f;
                
                    }
                    else if(critProcCount >= 3)
                    {
                        main.startColor = Color.red;
                        main.startSpeed = main.startSpeed.constantMax + 7f;
                        emission.rateOverTime = 100;
                        main.startSize = main.startSize.constant + 0.3f;
                    }
                    particleSystem.Play();
                }

            }

            damageHandler.hit(gameObject, weaponFinalDamage);
            Debug.Log("Damage dealt: " + weaponFinalDamage);
            weapon.ReduceDurability(5);
        }
    }
}
