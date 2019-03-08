using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private MoveManager m_MoveManager = new MoveManager();

    void Start()
    {
        m_MoveManager.Setup();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire"))
        {
            m_MoveManager.Select();
        }
    }
}
