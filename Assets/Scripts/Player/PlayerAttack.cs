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

    [SerializeField]
    private Projectile projectile;

    private Vector2 projectileLaunchOffset;

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

        if (Input.GetKeyDown(KeyCode.Mouse1) && weapon.GetWeapon().HasProjectile)
        {
            
            ProjectileSO playerProjectile = weapon.GetWeapon().WeaponProjectile;
            float projectileSpeed = 1f;
            float projectileLifetime = 1f;
            foreach (var param in playerProjectile.ProjectileParameters)
            {
                switch (param.projectileParameter.ParameterName)
                {
                    case "Projectile Speed":
                        projectileSpeed = param.value;
                        break;
                    case "Projectile Lifetime":
                        projectileLifetime = param.value;
                        break;
                }
                if (param.projectileParameter.ParameterName == "Projectile Speed")
                    projectileSpeed = param.value;
            }
           
            projectileLaunchOffset = movement.currentDir switch
            {
                "up" => new Vector2(0f, 0.5f),
                "down" => new Vector2(0f, -0.2f),
                "left" => new Vector2(-0.5f, 0f),
                "right" => new Vector2(0.5f, 0f),
                _ => new Vector2(0.5f, 0f),
            };

            Quaternion projectileRotation = movement.currentDir switch
            {
                "up" => Quaternion.Euler(Vector3.forward * 90),
                "down" => Quaternion.Euler(Vector3.forward * -90),
                "left" => Quaternion.Euler(Vector3.forward * 180),
                "right" => Quaternion.Euler(Vector3.forward),
                _ => Quaternion.Euler(Vector3.forward),
            };
            projectile.direction = movement.currentDir;

            Projectile projectileInstance = Instantiate(projectile, this.gameObject.transform.position + (Vector3)projectileLaunchOffset, projectileRotation);

            projectileInstance.ProjectileObject = playerProjectile;
            projectileInstance.isMoving = true;

            Destroy(projectileInstance, projectileLifetime);
            
        }
    }

    private void PrintWeaponCurrentState(PlayerWeapon pWeapon)
    {
        foreach (var param in pWeapon.weaponCurrentState)
        {
            Debug.Log(param.itemParameter.ParameterName + ": " + param.value);
        }
    }

    private float CalcPlayerDamage(float weaponBaseDamage, float critChance, float critMultiplier)
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


            float weaponFinalDamage = CalcPlayerDamage(weaponMeleeDamage, critChance, critMultiplier);

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
