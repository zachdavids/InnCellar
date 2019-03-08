using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableHero : MonoBehaviour
{
    private float health;
    private bool selected;
    private float speedMod;
    private Collider charCollider;

    // Start is called before the first frame update
    void Start()
    {
        health = 100.0f;
        selected = false;
        speedMod = 1.0f;
        charCollider = GetComponentInChildren<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
