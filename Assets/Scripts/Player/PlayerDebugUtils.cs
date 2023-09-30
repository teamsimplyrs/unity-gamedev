using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerDebugUtils : MonoBehaviour
{

    private Tilemap tilemap;

    // Start is called before the first frame update
    void Start()
    {
        tilemap = GameObject.Find("Grid").transform.GetChild(3).gameObject.GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()   
    {
        TestTile ruletile = tilemap.GetTile<TestTile>(Vector3Int.FloorToInt(transform.position));
        if(ruletile != null)
        {
            ParticleSystem particle = transform.GetChild(1).GetComponent<ParticleSystem>();
            ParticleSystem.MainModule psmain = particle.main;
            psmain.startColor = ruletile.footstepColor;
        }
    }
}
