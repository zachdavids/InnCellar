using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatBehaviour : MonoBehaviour
{
    private GameManager gameManager;
    private HealthController m_HealthController;

    // Start is called before the first frame update
    void Start()
    {
        this.gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        m_HealthController = GetComponent<HealthController>();
    }

    // Update is called once per frame
    void Update()
    {
        //TODO real death
        if (m_HealthController.GetCurrentHealth() == 0)
        {
            Debug.Log(this + " has been killed.");
            GetComponent<Rigidbody>().AddForce(new Vector3(0, 10, 0));
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.GetComponent<TrapBehaviour>() != null)
        {
            Debug.Log("Rat killed by a trap!");
            gameManager.NotifyRatKilled(this.gameObject);
            GameObject.Destroy(this.gameObject);
        }
    }
}
