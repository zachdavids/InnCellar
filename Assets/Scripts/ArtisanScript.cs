using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtisanScript : MonoBehaviour
{
    private int materials;
    private int maxMaterials;
    private GameManager model;


    // Start is called before the first frame update
    void Start()
    {
        materials = 2;
        maxMaterials = 3;
        model = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision coll)
    {
        if (model.isToolStation(coll.gameObject))
        {
            Debug.Log("Artisan's tools replenished");
            this.replenishMaterials();
        }
    }

    public void replenishMaterials()
    {
        this.materials = this.maxMaterials;
    }

    public int getMaxMaterials()
    {
        return this.maxMaterials;
    }

    public void setMaxMaterials(int amm)
    {
        this.maxMaterials = amm;
    }

    public int getMaterials()
    {
        return this.materials;
    }

    public void setMaterials(int amm)
    {
        if(amm > this.maxMaterials)
        {
            this.materials = this.maxMaterials;
        }
        else
        {
            this.materials = amm;
        }
    }



}
