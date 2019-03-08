using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Moves and zooms the camera to accommodate all four characters
TODO fix culling issue
 */

public class CameraControl : MonoBehaviour
{
    public float m_EdgeBuffer = 4.0f;
    public float m_SmoothTime = 0.2f;
    public float m_MinSize = 6.5f;
    public Transform[] m_Targets;

    private Camera m_Camera;
    private float m_ZoomSpeed;
    private Vector3 m_MoveVelocity;

    private void Awake()
    {
        m_Camera = GetComponent<Camera>();
    }

    private void FixedUpdate()
    {
        Refocus();
    }

    private void Refocus()
    {
        Vector3 newPosition = CalculateIdealPosition();
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref m_MoveVelocity, m_SmoothTime);

        float newSize = CalculateIdealSize(newPosition);
        m_Camera.orthographicSize = Mathf.SmoothDamp(m_Camera.orthographicSize, newSize, ref m_ZoomSpeed, m_SmoothTime);
    }

    Vector3 CalculateIdealPosition()
    {
        Vector3 averagePosition = new Vector3();
        for (int i = 0; i < m_Targets.Length; i++)
        {
            averagePosition += m_Targets[i].position;
        }
        averagePosition /= m_Targets.Length;
        averagePosition.y = transform.position.y;
        return averagePosition;
    }

    private float CalculateIdealSize(Vector3 newPosition)
    {
        Vector3 newLocalPosition = transform.InverseTransformPoint(newPosition);
        float size = 0.0f;
        for (int i = 0; i < m_Targets.Length; i++)
        {
            Vector3 targetLocalPosition = transform.InverseTransformPoint(m_Targets[i].position);
            Vector3 distanceToTarget = targetLocalPosition - newLocalPosition;
            size = Mathf.Max(size, Mathf.Abs(distanceToTarget.y));
            size = Mathf.Max(size, Mathf.Abs(distanceToTarget.x) / m_Camera.aspect);
        }
        size += m_EdgeBuffer;
        size = Mathf.Max(size, m_MinSize);
        return size;
    }
}
