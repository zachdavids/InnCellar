using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FlockManager : MonoBehaviour
{
    [Header("Setup")]
    public int m_NumFish;
    public GameObject m_FishPrehab;
    public GameObject[] m_Fish;
    public Vector3 m_SpawnBoundary = new Vector3(5, 5, 5);
    public Vector3 m_TargetPosition;

    [Header("Settings")]
    [Range(0.0f, 5.0f)]
    public float m_MinSpeed;
    [Range(0.0f, 5.0f)]
    public float m_MaxSpeed;
    [Range(1.0f, 10.0f)]
    public float m_NeighbourRange;
    [Range(1.0f, 10.0f)]
    public float m_AvoidRange;
    [Range(0.0f, 5.0f)]
    public float m_RotationSpeed;

    public void Start()
    {
        m_Fish = new GameObject[m_NumFish];
        for (int i = 0; i != m_NumFish; i++)
        {
            Vector3 position = transform.position + new Vector3(Random.Range(-m_SpawnBoundary.x, m_SpawnBoundary.x),
                                                                transform.position.y,
                                                                Random.Range(-m_SpawnBoundary.z, m_SpawnBoundary.z));
            m_Fish[i] = (GameObject)Instantiate(m_FishPrehab, position, Quaternion.identity);
            m_Fish[i].GetComponent<Flock>().m_FlockManager = this;
        }
        m_TargetPosition = transform.position;
    }

    public void Update()
    {
        m_TargetPosition = transform.position + new Vector3(Random.Range(-m_SpawnBoundary.x, m_SpawnBoundary.x),
                                                            transform.position.y,
                                                            Random.Range(-m_SpawnBoundary.z, m_SpawnBoundary.z));
    }
}
