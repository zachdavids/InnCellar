using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClickToMove : MonoBehaviour
{
    private bool bWalking = false;
    private Animator CharacterAnimator;
    private NavMeshAgent CharacterNavMeshAgent;

    // Start is called before the first frame update
    void Awake()
    {
        CharacterAnimator = GetComponent<Animator>();
        CharacterNavMeshAgent = GetComponent<NavMeshAgent>();
    }

    //TODO Implement animation, fix stutter, break link between camera and character rotation
    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetButtonDown("Fire"))
        {
            if (Physics.Raycast(ray, out hit, 100))
            {
                bWalking = true;
                CharacterNavMeshAgent.destination = hit.point;
                //CharacterNavMeshAgent.isStopped = false;
            }
        }
    }
}
