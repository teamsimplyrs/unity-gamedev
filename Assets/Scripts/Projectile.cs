using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public ProjectileSO ProjectileObject;
    public Sprite ProjectileSprite;
    private GameObject ProjectileSource;
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    public Boolean isMoving;
    public string direction;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = ProjectileObject.ProjectileSprite;
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            rb.AddForce(direction switch
            {
                "up" => new Vector2(0f,5f),
                "down" => new Vector2(0f, -5),
                "left" => new Vector2(-5, 0f),
                "right" => new Vector2(5, 0f),
                _ => new Vector2(1f, 0f)
            }, ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            float projectileBaseDamage = 1;
            float projectileCritChance = 0;
            float projectileCritMultiplier = 0;

            foreach (var param in ProjectileObject.ProjectileParameters)
            {
                switch (param.projectileParameter.ParameterName)
                {
                    case "Projectile Damage":
                        projectileBaseDamage = param.value;
                        break;
                    case "Projectile Critical Chance":
                        projectileCritChance = param.value;
                        break;
                    case "Projectile Critical Multiplier":
                        projectileCritMultiplier = param.value;
                        break;
                }
            }

            EnemyDamageHandler enemy = collision.gameObject.GetComponent<EnemyDamageHandler>();
            enemy.hit(
                this.ProjectileSource,
                CalcProjectileDamage(
                    projectileBaseDamage, projectileCritChance, projectileCritMultiplier
                    )
                );
            Destroy(this);
        }
    }

    public void SetProjectileSource(GameObject gameObject)
    {
        this.ProjectileSource = gameObject;
    }

    public GameObject GetProjectileSource()
    {
        return this.ProjectileSource;
    }

    public float CalcProjectileDamage(float projectileBaseDamage, float critChance, float critMultiplier)
    {
        float finalDamage = projectileBaseDamage;

        if (critChance != 0)
        {
            int critProc;
            while (critChance > 0)
            {
                critProc = UnityEngine.Random.Range(0, 100);
                Debug.Log("crit proc: " + critProc);
                if (critProc <= critChance)
                {
                    finalDamage += (projectileBaseDamage * critMultiplier);
                }
                critChance -= 100;
            }
        }

        return finalDamage;
    }
}
