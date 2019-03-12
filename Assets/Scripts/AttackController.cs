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
    [Header("Attack Settings")]
    public float m_AttackDamage = 15;
    public float m_AttackSpeed = 2;
    public float m_AttackRange = 3;

    private float m_LastAttack = 0;
    private GameObject m_Target;
    private NavMeshAgent m_CharacterNavMeshAgent;
    private Animator m_CharacterAnimator;
    private HealthController m_EnemyHealthUnit;

    public void SetTarget(GameObject target)
    {
        m_Target = target;
        if (!target) { return; }
        m_EnemyHealthUnit = m_Target.GetComponent<HealthController>();
    }

    void Start()
    {
        m_CharacterNavMeshAgent = GetComponent<NavMeshAgent>();
        m_CharacterAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!m_Target) { return; }
        Attack();   
    }

    private void Attack()
    {
        if (m_CharacterNavMeshAgent.remainingDistance > m_AttackRange) { return; }

        if (m_AttackSpeed < Time.time - m_LastAttack)
        {
            Debug.Log(this + " attacks " + m_Target + "(HP: " + m_EnemyHealthUnit.GetCurrentHealth() + ") for " + m_AttackDamage + "damage");
            this.transform.forward = m_Target.transform.position;
            m_EnemyHealthUnit.SustainDamage(m_AttackDamage);
            m_LastAttack = Time.time;
            m_CharacterAnimator.SetBool("bIsAttacking", true);
        }
        else
        {
            m_CharacterAnimator.SetBool("bIsAttacking", false);
        }
    }
}
