using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    GameObject sword;

    // Start is called before the first frame update
    void Start()
    {
        sword = transform.Find("Hand").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            sword.GetComponent<Animator>().SetTrigger("attacking");
        }
    }
}
