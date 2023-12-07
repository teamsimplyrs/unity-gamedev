using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAnimation : MonoBehaviour
{
    public Animator anim;
    GameObject sword;
    private TrailRenderer trailRenderer;
    PolygonCollider2D swordCollider;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        sword = transform.GetChild(0).GetChild(0).gameObject;
        swordCollider = sword.GetComponent<PolygonCollider2D>();
        trailRenderer = sword.GetComponent<TrailRenderer>();
        trailRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ClearAnimation()
    {
        anim.ResetTrigger("attacking");
        gameObject.SetActive(false);
    }

    void EnableTrail()
    {
        trailRenderer.enabled = true;
        swordCollider.enabled = true;
    }

    void DisableTrail()
    {
        trailRenderer.enabled = false;
        swordCollider.enabled = false;
    }

    public void UpdateEquippedSword(GameObject character)
    {
        PlayerWeapon playerWeapon = character.GetComponent<PlayerWeapon>();
        EquippablesSO sword = playerWeapon.GetWeapon();

        //Same as the sword object above but can't access it because it's not set sometimes when updateequippedsword is called
        GameObject swordGameObject = transform.GetChild(0).GetChild(0).gameObject;
        SpriteRenderer swordSpriteRenderer = swordGameObject.GetComponent<SpriteRenderer>();
        swordSpriteRenderer.sprite = sword.ItemSprite;
    }
}
