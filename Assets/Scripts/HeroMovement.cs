using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
Handles selection and movement of heroes
*/

public class HeroMovement : MonoBehaviour
{
    private bool m_bSelected = false;
    private NavMeshAgent m_CharacterNavMeshAgent;
    private Animator m_CharacterAnimator;
    private AttackController m_AttackScript;

    public void Start()
    {
        m_CharacterNavMeshAgent = GetComponent<NavMeshAgent>();
        m_CharacterAnimator = GetComponent<Animator>();
        m_AttackScript = GetComponent<AttackController>();
    }

    public void Update()
    {
        if (Input.GetButtonDown("Fire"))
        {
            Select();
        }
        if (Input.GetButtonDown("Fire2"))
        {
            Move();
        }
        if (Input.GetButtonDown("Cancel"))
        {
            ClearSelection();
        }
        if (IsStopped())
        {
            m_CharacterAnimator.SetBool("bIsRunning", false);
        }
    }

    private void Select()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100))
        {
            GameObject selection = hit.collider.gameObject;
            if (selection == this.gameObject)
            {
                m_bSelected = true;
            }
        }
    }

    private void Move()
    {
        if (!m_bSelected) { return; }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100))
        {
            GameObject selection = hit.collider.gameObject;
            if (selection.CompareTag("Enemy"))
            {
                m_CharacterNavMeshAgent.destination = hit.point;
                m_CharacterAnimator.SetBool("bIsRunning", true);
                m_AttackScript.SetTarget(selection);
            }
            else
            {
                m_CharacterNavMeshAgent.destination = hit.point;
                m_CharacterAnimator.SetBool("bIsRunning", true);
                m_AttackScript.SetTarget(null);
            }
        }
    }

    private bool IsStopped()
    {
        if (m_CharacterNavMeshAgent.remainingDistance <= m_CharacterNavMeshAgent.stoppingDistance)
        {
            return true;
        }
        return false;
    }

    private void ClearSelection()
    {
        m_bSelected = false;
    }

    // Special handling may be required when boarding a minecart and getting off a minecart to ensure it doesn't trip up the state of the nav mesh. This can be handled here.
    // The following functions (BoardedMinecart and GetOffMinecart) will be called automatically in the appropriate situations
    public void BoardedMinecart()
    {
        m_CharacterAnimator.SetBool("bIsRunning", false);
    }

    public void GotOffMinecart(Vector3 expectedArrivalPosition)
    {
        m_CharacterNavMeshAgent.destination = expectedArrivalPosition;
        m_CharacterNavMeshAgent.Warp(expectedArrivalPosition);
        m_CharacterAnimator.SetBool("bIsRunning", true);
    }
}
