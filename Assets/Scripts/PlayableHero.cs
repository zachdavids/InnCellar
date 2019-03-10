using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableHero : MonoBehaviour
{
    private HealthController health;
    private bool selected;
    private float speedMod;
    private GameManager model;
    private float damageCooldown = 0;

    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<HealthController>();
        selected = false;
        speedMod = 1.0f;
        model = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(this.damageCooldown > 0)
        {
            float updatedCooldown = this.damageCooldown - Time.deltaTime;
            if (updatedCooldown < 0) { updatedCooldown = 0; }
            this.damageCooldown = updatedCooldown;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        // A playable hero colliding with health station should heal
        if (model.isHealthStation(col.gameObject))
        {
            Debug.Log(this.gameObject.name + " was just healed");
            this.health.Replenish();
        }
    }

    void OnCollisionStay(Collision col)
    {
        if (col.gameObject.GetComponent<RatBehaviour>() != null)
        {
            if (this.damageCooldown == 0)
            {
                Debug.Log("A hero sustained damage!");
                this.health.SustainDamage(20);
                this.damageCooldown = 2;
            }
        }
    }
}
