using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minecart : MonoBehaviour
{
    #region Attributes

    [SerializeField] private GameObject _track;
    [SerializeField] private bool _broken;

    private bool _isMoving;
    private bool _isReturning;
    private float _velocity;
    private float _currentPosition;
    private GameObject _mountedPlayer;

    #endregion

    #region Monobehaviour Functions

    void Start()
    {
        _currentPosition = 0;

        HermiteSpline spline = _track.GetComponent<HermiteSpline>();
        transform.position = spline.InterpolateSpline(_currentPosition);
        transform.LookAt(transform.position + spline.GetTangeantAtStep(0), Vector3.up);
        transform.Rotate(270, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (_isMoving)
        {
            HermiteSpline spline = _track.GetComponent<HermiteSpline>();
            float buildupAccel = 0.02f;
            float max_velocity = 0.25f;

            if (_isReturning)
            {
                max_velocity = 0.1f;
            }

            if (_velocity < max_velocity)
            {
                _velocity += buildupAccel;
            }

            if (_currentPosition > 0.7f && !_isReturning)
            {
                _velocity = max_velocity * ((1.0f - _currentPosition) / 0.2f);
            }

            if (_currentPosition < 0.2f && _isReturning)
            {
                _velocity = max_velocity * (_currentPosition / 0.2f);
            }


            if (_isReturning)
            {
                _currentPosition -= _velocity * Time.deltaTime;
            }
            else
            {
                _currentPosition += _velocity * Time.deltaTime;
            }

            if (_currentPosition >= 0.995f && _velocity < buildupAccel)
            {
                _currentPosition = 0.995f;
                HasArrived();
            }

            if (_currentPosition <= 0.005f && _velocity < buildupAccel)
            {
                _currentPosition = 0.0f;
                HasReturned();
            }

            transform.position = spline.InterpolateSpline(_currentPosition);
            transform.LookAt(transform.position + spline.GetTangeantAtStep(_currentPosition), Vector3.up);
            transform.Rotate(270, 0, 0);

            if (_mountedPlayer)
            {
                _mountedPlayer.transform.position = transform.position;
                _mountedPlayer.transform.LookAt(transform.position + spline.GetTangeantAtStep(_currentPosition), Vector3.up);
            }
        }
        else
        {
            if (Input.GetKeyDown("e"))
            {
                GameManager manager = GameObject.Find("GameManager").GetComponent<GameManager>();
                float distanceThreshold = 7.0f;

                if (Vector3.Distance(manager.Artisan.transform.position, transform.position) < distanceThreshold)
                {
                    Debug.Log("Artisan can mount");
                    Board(manager.Artisan);
                }
                else if (Vector3.Distance(manager.Bard.transform.position, transform.position) < distanceThreshold)
                {
                    Debug.Log("Bard can mount");
                    Board(manager.Bard);
                }
                else if (Vector3.Distance(manager.Warrior.transform.position, transform.position) < distanceThreshold)
                {
                    Debug.Log("Warrior can mount");
                    Board(manager.Warrior);
                }
                else if (Vector3.Distance(manager.Thief.transform.position, transform.position) < distanceThreshold)
                {
                    Debug.Log("Thief can mount");
                    Board(manager.Thief);
                }
            }
        }
    }

    #endregion

    #region Minecart Logic

    private void HasArrived()
    {
        _isReturning = true;
        _velocity = 0.0f;

        if (_mountedPlayer)
        {
            HermiteSpline spline = _track.GetComponent<HermiteSpline>();
            //_mountedPlayer.transform.position = spline.InterpolateSpline(1.0f) + spline.GetTangeantAtStep(0.98f);
            _mountedPlayer.GetComponent<HeroMovement>().GotOffMinecart(spline.InterpolateSpline(1.0f) + spline.GetTangeantAtStep(0.98f));
            _mountedPlayer = null;
        }
    }

    private void HasReturned()
    {
        _isMoving = false;
        _isReturning = false;
        _velocity = 0.0f;
    }

    private void Board(GameObject character)
    {
        _isMoving = true;
        _velocity = 0.0f;
        _mountedPlayer = character;
        _mountedPlayer.GetComponent<HeroMovement>().BoardedMinecart();
    }

    private void TestStart()
    {
        _isMoving = true;
        _velocity = 0.0f;
    }

    #endregion
}
