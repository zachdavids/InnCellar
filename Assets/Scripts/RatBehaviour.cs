using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatBehaviour : MonoBehaviour
{
    #region Attributes

    private GameManager _gameManager;
    private HealthController _healthController;

    #endregion

    #region Monobehaviour Functions

    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _healthController = GetComponent<HealthController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_healthController.currentHealth == 0)
        {
            Debug.Log(this + " has been killed.");
            GetComponent<Rigidbody>().AddForce(new Vector3(0, 10, 0));
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.GetComponent<TrapBehaviour>())
        {
            Debug.Log("Rat killed by a trap!");
            _gameManager.NotifyRatKilled(gameObject);
            GameObject.Destroy(gameObject);
        }
    }

    #endregion
}
