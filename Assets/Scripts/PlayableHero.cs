using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableHero : MonoBehaviour
{
    #region Attributes

    private bool _isSelected;
    private float _speedModifier;
    private float _damageCooldown = 0;
    private GameManager model;
    private HealthController health;

    #endregion

    #region Monobehaviour Functions

    void Start()
    {
        _isSelected = false;
        _speedModifier = 1.0f;
        health = GetComponent<HealthController>();
        model = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        if(_damageCooldown > 0)
        {
            float updatedCooldown = _damageCooldown - Time.deltaTime;
            if (updatedCooldown < 0) { updatedCooldown = 0; }
            _damageCooldown = updatedCooldown;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (model.isHealthStation(col.gameObject))
        {
            health.Replenish();
        }
    }

    void OnCollisionStay(Collision col)
    {
        if (col.gameObject.GetComponent<RatBehaviour>())
        {
            if (_damageCooldown == 0)
            {
                Debug.Log("A hero sustained damage!");
                health.SustainDamage(20);
                _damageCooldown = 2;
            }
        }
    }

    #endregion
}
