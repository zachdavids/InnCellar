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
    private GameObject m_SelectedHero;

    public void Setup()
    {
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
                m_SelectedHero = selection;
            }
            else if (m_SelectedHero)
            {
                m_SelectedHero.GetComponent<NavMeshAgent>().destination = hit.point;
                m_SelectedHero.GetComponent<Animator>().SetBool("bIsRunning", true);
                //CharacterNavMeshAgent.isStopped = false;
            }
        }

    }
}
