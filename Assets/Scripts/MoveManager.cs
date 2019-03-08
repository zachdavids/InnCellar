using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
Handles selection and movement of heroes
TODO continue to move even if selection has changedas
*/

[System.Serializable]
public class MoveManager
{
    private GameObject selectedHero;

    public void Setup()
    {
        //CharacterAnimator = GetComponent<Animator>();
        //CharacterNavMeshAgent = GetComponent<NavMeshAgent>();
    }

    public void Select()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100))
        {
            GameObject selection = hit.collider.gameObject;
            if (selection.CompareTag("Hero"))
            {
                Debug.Log(selection + " selected");
                selectedHero = selection;
            }
            else if (selectedHero)
            {
                selectedHero.GetComponent<NavMeshAgent>().destination = hit.point;
                //CharacterNavMeshAgent.isStopped = false;
            }
        }

    }
}
