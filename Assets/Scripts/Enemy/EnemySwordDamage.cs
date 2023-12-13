using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySwordDamage : MonoBehaviour
{

    private EnemyWeapon weapon;
    // Start is called before the first frame update
    void Start()
    {
        weapon = transform.GetComponentInParent<EnemyWeapon>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageHandler damageHandler = collision.GetComponent<IDamageHandler>();
        if (damageHandler != null)
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

            damageHandler.hit(gameObject, weaponMeleeDamage);
        }
    }
}
