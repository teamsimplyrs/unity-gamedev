using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAnimation : MonoBehaviour
{
    public Animator anim;
    private TrailRenderer trailRenderer;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        trailRenderer = transform.GetChild(0).GetChild(0).GetComponent<TrailRenderer>();
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
    }

    void DisableTrail()
    {
        trailRenderer.enabled = false;
    }
}
