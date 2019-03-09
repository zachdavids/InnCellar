using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableHero : MonoBehaviour
{
    private HealthUnit health;
    private bool selected;
    private float speedMod;
    private GameManager model;

    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<HealthUnit>();
        selected = false;
        speedMod = 1.0f;
        model = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnCollisionEnter(Collision col)
    {
        // A playable hero colliding with health station should heal
        if (model.isHealthStation(col.gameObject))
        {
            Debug.Log(this.gameObject.name + " was just healed");
            this.health.replenish();
        }
    }
}
