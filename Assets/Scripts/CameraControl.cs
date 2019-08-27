using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Moves and zooms the camera to accommodate all targets
 */

public class CameraControl : MonoBehaviour
{
    #region Attributes

    [Header("Camera Settings")]
    [SerializeField] private float _edgeBuffer = 1.0f;
    [SerializeField] private float _smoothTime = 0.2f;
    [SerializeField] private float _minSize = 6.5f;
    [SerializeField] private Transform[] _targets = null;

    private float __zoomSpeed;
    private Vector3 _moveVelocity;
    private Camera _camera;

    #endregion

    private void Refocus()
    {
        Vector3 newPosition = CalculateIdealPosition();
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref _moveVelocity, _smoothTime);

        float newSize = CalculateIdealSize(newPosition);
        _camera.orthographicSize = Mathf.SmoothDamp(_camera.orthographicSize, newSize, ref __zoomSpeed, _smoothTime);
    }

    Vector3 CalculateIdealPosition()
    {
        Vector3 averagePosition = new Vector3();
        for (int i = 0; i < _targets.Length; i++)
        {
            averagePosition += _targets[i].position;
        }

        averagePosition /= _targets.Length;
        averagePosition.y = transform.position.y;

        return averagePosition;
    }

    private float CalculateIdealSize(Vector3 newPosition)
    {
        float size = 0.0f;

        Vector3 newLocalPosition = transform.InverseTransformPoint(newPosition);

        for (int i = 0; i < _targets.Length; i++)
        {
            Vector3 targetLocalPosition = transform.InverseTransformPoint(_targets[i].position);
            Vector3 distanceToTarget = targetLocalPosition - newLocalPosition;

            size = Mathf.Max(size, Mathf.Abs(distanceToTarget.y));
            size = Mathf.Max(size, Mathf.Abs(distanceToTarget.x) / _camera.aspect);
        }

        size += _edgeBuffer;
        size = Mathf.Max(size, _minSize);

        return size;
    }

    #region Monobehaviour Functions

    private void Awake()
    {
        _camera = GetComponentInChildren<Camera>();
        _camera.orthographicSize = CalculateIdealSize(transform.position);
        transform.position = CalculateIdealPosition();
    }

    private void FixedUpdate()
    {
        Refocus();
    }

    #endregion
}
