using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterHealthModifierSO : CharacterStatModifierSO
{
    public override void AffectCharacter(GameObject character, float val)
    {
        PlayerHealth playerHP = character.GetComponent<PlayerHealth>();

        if (playerHP != null && !playerHP.IsAtFullHP())
        {
            playerHP.Heal((int)val);
        }
    }
}
