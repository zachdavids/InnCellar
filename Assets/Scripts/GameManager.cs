using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private MoveManager m_MoveManager = new MoveManager();

    public GameObject Artisan;
    public GameObject Bard;
    public GameObject Thief;
    public GameObject Warrior;

    public GameObject[] HealthStations;
    public GameObject[] ToolStations;


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


    public bool isHealthStation(GameObject obj)
    {
        for (int i = 0; i < this.HealthStations.Length; ++i)
        {
            if (this.HealthStations[i].name.Equals(obj.name))
            {
                return true;
            }
        }

        return false;
    }

    public bool isToolStation(GameObject obj)
    {
        for (int i = 0; i < this.ToolStations.Length; ++i)
        {
            if (this.ToolStations[i].name.Equals(obj.name))
            {
                return true;
            }
        }

        return false;
    }

    public int getMaterials()
    {

        ArtisanScript art = Artisan.GetComponent<ArtisanScript>();
        if (art != null)
        {
                return art.getMaterials();
        }

        return -1;
    }
}
