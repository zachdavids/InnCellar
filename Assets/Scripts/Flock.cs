using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Flock : MonoBehaviour
{
    public FlockManager m_FlockManager;

    private float m_Speed;
    private bool m_IsTurning;

    public void Start()
    {
        m_Speed = Random.Range(m_FlockManager.m_MinSpeed,
                                m_FlockManager.m_MaxSpeed);
    }

    public void Update()
    {
        Bounds boundary = new Bounds(m_FlockManager.transform.position, m_FlockManager.m_SpawnBoundary * 2);
        m_IsTurning = !boundary.Contains(transform.position) ? true : false;

        if (m_IsTurning)
        {
            Vector3 direction = m_FlockManager.transform.position - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction),
                m_FlockManager.m_RotationSpeed * Time.deltaTime);
        }
        else
        {
            FlockBehaviour();
            transform.Translate(0, 0, Time.deltaTime * m_Speed);
        }
    }

    private void FlockBehaviour()
    {
        int num_neightbours = 0;
        float average_speed = 0.0f;
        Vector3 average_position = Vector3.zero;
        Vector3 average_avoid = Vector3.zero;
        GameObject[] flock = m_FlockManager.m_Fish;

        foreach(GameObject fish in flock)
        {
            if (fish == gameObject) { continue; }

            float distance = Vector3.Distance(fish.transform.position, transform.position);
            if (distance <= m_FlockManager.m_NeighbourRange)
            {
                if (distance < m_FlockManager.m_AvoidRange)
                {
                    average_avoid += (fish.transform.position - transform.position);
                }
                average_position += fish.transform.position;
                average_speed += fish.GetComponent<Flock>().m_Speed;
                num_neightbours++;
            }
        }

        if (num_neightbours > 0)
        {
            average_position /= num_neightbours;
            average_position.x += m_FlockManager.m_TargetPosition.x - transform.position.x;
            average_position.y = transform.position.y;
            average_position.z += m_FlockManager.m_TargetPosition.z - transform.position.z;
            average_speed /= num_neightbours;

            Vector3 direction = average_position + average_avoid - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 
                m_FlockManager.m_RotationSpeed * Time.deltaTime);
        }
    }   
}
