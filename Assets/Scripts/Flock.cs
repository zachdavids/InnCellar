using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
Flocking related variable and states for the warrior's wolves
 */

public class Flock : MonoBehaviour
{
    #region Attributes

    private float _speed;
    private bool _isTurning;
    private FlockManager _flockManager; 

    public FlockManager flockManager
    {
        set { _flockManager = value; }
    }

    #endregion

    #region Flock Logic

    private void FlockBehaviour()
    {
        int num_neightbours = 0;
        float average_speed = 0.0f;
        Vector3 average_position = Vector3.zero;
        Vector3 average_avoid = Vector3.zero;
        GameObject[] flock = _flockManager.animals;

        foreach (GameObject animal in flock)
        {
            if (animal == gameObject) { continue; }

            float distance = Vector3.Distance(animal.transform.position, transform.position);
            if (distance <= _flockManager.neighbourRange)
            {
                if (distance < _flockManager.avoidRange)
                {
                    average_avoid += (animal.transform.position - transform.position);
                }
                average_position += animal.transform.position;
                average_speed += animal.GetComponent<Flock>()._speed;
                num_neightbours++;
            }
        }

        if (num_neightbours > 0)
        {
            average_position /= num_neightbours;
            average_position.x += _flockManager.targetPosition.x - transform.position.x;
            average_position.y = transform.position.y;
            average_position.z += _flockManager.targetPosition.z - transform.position.z;
            average_speed /= num_neightbours;

            Vector3 direction = average_position + average_avoid - transform.position;
            transform.rotation = Quaternion.Slerp(
                transform.rotation, 
                Quaternion.LookRotation(direction), 
                _flockManager.rotationSpeed * Time.deltaTime
            );
        }
    }

    #endregion

    #region Monobehaviour Functions

    public void Start()
    {
        _speed = Random.Range(_flockManager.minSpeed, _flockManager.maxSpeed);
    }

    public void Update()
    {
        Transform flockTransform = transform;
        Transform flockManagerTransform = _flockManager.transform;

        Bounds boundary = new Bounds(flockManagerTransform.position, _flockManager.spawnBoundary * 2);
        _isTurning = !boundary.Contains(flockTransform.position) ? true : false;

        if (_isTurning)
        {
            Vector3 direction = flockManagerTransform.position - flockTransform.position;
            flockTransform.rotation = Quaternion.Slerp(
                flockTransform.rotation,
                Quaternion.LookRotation(direction),
                _flockManager.rotationSpeed * Time.deltaTime
            );
        }
        else
        {
            FlockBehaviour();
            flockTransform.Translate(0, 0, Time.deltaTime * _speed);
        }
    }

    #endregion
}
