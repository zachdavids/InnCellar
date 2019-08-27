using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
Handles selection and movement of heroes
*/

public class HeroMovement : MonoBehaviour
{
    #region Attributes

    private bool _isSelected;
    private NavMeshAgent _navMeshAgent;
    private Animator _animator;
    private AttackController _attackScript;

    #endregion

    #region Movement Logic

    private void Select()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100))
        {
            GameObject selection = hit.collider.gameObject;
            if (selection == this.gameObject)
            {
                _isSelected = true;
            }
        }
    }

    private void Move()
    {
        if (!_isSelected) { return; }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100))
        {
            GameObject selection = hit.collider.gameObject;
            if (selection.CompareTag("Enemy"))
            {
                _navMeshAgent.destination = hit.point;
                _animator.SetBool("bIsRunning", true);
                _attackScript.SetTarget(selection);
            }
            else
            {
                _navMeshAgent.destination = hit.point;
                _animator.SetBool("bIsRunning", true);
                _attackScript.SetTarget(null);
            }
        }
    }

    private bool IsStopped()
    {
        if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
        {
            return true;
        }

        return false;
    }

    #endregion

    #region Minecart Logic

    public void BoardedMinecart()
    {
        _animator.SetBool("bIsRunning", false);
    }

    public void GotOffMinecart(Vector3 expectedArrivalPosition)
    {
        _navMeshAgent.destination = expectedArrivalPosition;
        _navMeshAgent.Warp(expectedArrivalPosition);
        _animator.SetBool("bIsRunning", true);
    }

    #endregion

    #region Monobehaviour Functions

    public void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _attackScript = GetComponent<AttackController>();
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
            _isSelected = false;
        }
        if (IsStopped())
        {
            _animator.SetBool("bIsRunning", false);
        }
    }

    #endregion
}
