using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
Controls attack related variable and states
 */

public class AttackController : MonoBehaviour
{
    #region Attributes

    [Header("Attack Settings")]
    [SerializeField] private float _attackDamage = 15;
    [SerializeField] private float _attackSpeed = 2;
    [SerializeField] private float _attackRange = 3;

    private float _lastAttack;
    private GameObject _target;
    private NavMeshAgent _navMeshAgent;
    private Animator _animator;
    private HealthController _enemyHealth;

    #endregion

    #region Attack Logic

    private void Attack()
    {
        if (_navMeshAgent.remainingDistance > _attackRange) return;

        if (_attackSpeed < Time.time - _lastAttack)
        {
            Debug.Log(this + " attacks " + _target + "(HP: " + _enemyHealth.GetCurrentHealth() + ") for " + _attackDamage + "damage");
            this.transform.forward = _target.transform.position;
            _animator.SetBool("bIsAttacking", true);
            _enemyHealth.SustainDamage(_attackDamage);
            _lastAttack = Time.time;
        }
        else
        {
            _animator.SetBool("bIsAttacking", false);
        }
    }

    public void SetTarget(GameObject target)
    {
        if (!target) return;

        _target = target;
        _enemyHealth = _target.GetComponent<HealthController>();
    }

    #endregion

    #region Monobehaviour Functions

    void Start()
    {
        _lastAttack = 0;

        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!_target) return;

        Attack();
    }

    #endregion
}
