using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
Controls Flock related variable and states
 */

public class FlockManager : MonoBehaviour
{
    #region Attributes

    [Header("Setup")]
    [SerializeField] private int _numAnimals = 4;
    [SerializeField] private GameObject _animalPrehab;
    [SerializeField] private Vector3 _spawnBoundary = new Vector3(5, 5, 5);
    public Vector3 spawnBoundary
    {
        get { return _spawnBoundary; }
    }

    [Header("Settings")]
    [SerializeField, Range(0.0f, 5.0f)] private float _minSpeed;
    public float minSpeed
    {
        get { return _minSpeed; }
    }

    [SerializeField, Range(0.0f, 5.0f)] private float _maxSpeed;
    public float maxSpeed
    {
        get { return _maxSpeed; }
    }

    [SerializeField, Range(1.0f, 10.0f)] private float _neighbourRange;
    public float neighbourRange
    {
        get { return _neighbourRange; }
    }


    [SerializeField, Range(1.0f, 10.0f)] private float _avoidRange;
    public float avoidRange
    {
        get { return _avoidRange; }
    }

    [SerializeField, Range(0.0f, 5.0f)] private float _rotationSpeed;
    public float rotationSpeed
    {
        get { return _rotationSpeed; }
    }

    private Vector3 _targetPosition;
    public Vector3 targetPosition
    {
        get { return _targetPosition; }
    }

    private GameObject[] _animals;
    public GameObject[] animals
    {
        get { return _animals; }
    }

    #endregion

    #region Monobehaviour Functions

    public void Start()
    {
        _animals = new GameObject[_numAnimals];
        for (int i = 0; i != _numAnimals; i++)
        {
            Vector3 position = transform.position + new Vector3(Random.Range(-_spawnBoundary.x, _spawnBoundary.x),
                                                                transform.position.y,
                                                                Random.Range(-_spawnBoundary.z, _spawnBoundary.z));

            _animals[i] = (GameObject)Instantiate(_animalPrehab, position, Quaternion.identity);
            _animals[i].GetComponent<Flock>().flockManager = this;
        }
        _targetPosition = transform.position;
    }

    public void Update()
    {
        _targetPosition = transform.position + new Vector3(Random.Range(-_spawnBoundary.x, _spawnBoundary.x),
                                                            transform.position.y,
                                                            Random.Range(-_spawnBoundary.z, _spawnBoundary.z));
    }

    #endregion
}