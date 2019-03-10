using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtisanScript : MonoBehaviour
{
    private int materials;
    private int maxMaterials;
    private GameManager model;
    private ResourceManager resourceManager;
    private List<GameObject> RatTraps;


    // Start is called before the first frame update
    void Start()
    {
        materials = 2;
        maxMaterials = 3;
        model = GameObject.Find("GameManager").GetComponent<GameManager>();
        resourceManager = GameObject.Find("ResourceManager").GetComponent<ResourceManager>();
        RatTraps = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            if (this.materials >= 1)
            {
                Debug.Log("Placing trap");
                this.materials -= 1;
                CreateTrap();
            }
        }
    }

    void OnCollisionEnter(Collision coll)
    {
        if (model.isToolStation(coll.gameObject))
        {
            Debug.Log("Artisan's tools replenished");
            this.replenishMaterials();
        }
    }

    private void CreateTrap()
    {
        GameObject New_Trap = new GameObject("RatTrap");

        New_Trap.AddComponent<Rigidbody>();
        New_Trap.GetComponent<Rigidbody>().useGravity = false;
        New_Trap.AddComponent<CapsuleCollider>();

        CapsuleCollider collider = New_Trap.GetComponent<CapsuleCollider>();
        collider.center.Set(-0.403f, 0.23134f, -0.01555f);
        collider.radius = 1.684219f;
        collider.height = 6.20584f;

        GameObject SubMesh = new GameObject("Cylinder");
        GameObject SubMesh2 = new GameObject("Spikes");

        SubMesh.AddComponent<MeshFilter>().mesh = resourceManager.RatTrapMesh1;
        SubMesh.AddComponent<MeshRenderer>().material = resourceManager.RatTrapMaterial1;
        SubMesh2.AddComponent<MeshFilter>().mesh = resourceManager.RatTrapMesh2;
        SubMesh2.AddComponent<MeshRenderer>().material = resourceManager.RatTrapMaterial2;

        SubMesh.transform.parent = New_Trap.transform;
        SubMesh2.transform.parent = New_Trap.transform;

        New_Trap.transform.position = this.gameObject.transform.position;

        New_Trap.AddComponent<TrapBehaviour>();
        New_Trap.GetComponent<TrapBehaviour>().SetCreator(this);

        this.RatTraps.Add(New_Trap);
    }

    public void NotifyTrapDestroyed(GameObject obj)
    {
        this.RatTraps.Remove(obj);
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
