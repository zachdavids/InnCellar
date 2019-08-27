using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Controls Artisan related variable and states
 */

public class ArtisanScript : MonoBehaviour
{
    #region Attributes

    private static int _maxMaterials = 3;
    private GameManager _model;
    private ResourceManager _resourceManager;
    private List<GameObject> _ratTraps;

    private int _materials;
    public int materials
    {
        get { return _materials; }
    }

    #endregion

    #region Artisan Logic

    private void CreateTrap()
    {
        GameObject newTrap = new GameObject("RatTrap");
        newTrap.AddComponent<Rigidbody>();
        newTrap.GetComponent<Rigidbody>().useGravity = false;
        newTrap.AddComponent<CapsuleCollider>();

        CapsuleCollider collider = newTrap.GetComponent<CapsuleCollider>();
        collider.center.Set(-0.403f, 0.23134f, -0.01555f);
        collider.radius = 1.684219f;
        collider.height = 6.20584f;

        GameObject cylinderMesh = new GameObject("Cylinder");
        cylinderMesh.AddComponent<MeshFilter>().mesh = _resourceManager.RatTrapMesh1;
        cylinderMesh.AddComponent<MeshRenderer>().material = _resourceManager.RatTrapMaterial1;
        cylinderMesh.transform.parent = newTrap.transform;

        GameObject spikeMesh = new GameObject("Spikes");
        spikeMesh.AddComponent<MeshFilter>().mesh = _resourceManager.RatTrapMesh2;
        spikeMesh.AddComponent<MeshRenderer>().material = _resourceManager.RatTrapMaterial2;
        spikeMesh.transform.parent = newTrap.transform;

        newTrap.transform.position = gameObject.transform.position;
        newTrap.AddComponent<TrapBehaviour>();
        newTrap.GetComponent<TrapBehaviour>().SetCreator(this);

        _ratTraps.Add(newTrap);
    }

    public void NotifyTrapDestroyed(GameObject obj)
    {
        _ratTraps.Remove(obj);
    }

    public void ReplenishMaterials()
    {
        _materials = _maxMaterials;
    }

    #endregion

    #region Monobehaviour Functions

    // Start is called before the first frame update
    void Start()
    {
        _materials = _maxMaterials;
        _ratTraps = new List<GameObject>();
        _model = GameObject.Find("GameManager").GetComponent<GameManager>();
        _resourceManager = GameObject.Find("ResourceManager").GetComponent<ResourceManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            if (_materials >= 1)
            {
                Debug.Log("Placing trap");
                --_materials;
                CreateTrap();
            }
        }
    }

    void OnCollisionEnter(Collision coll)
    {
        if (_model.isToolStation(coll.gameObject))
        {
            Debug.Log("Artisan's tools replenished");
            ReplenishMaterials();
        }
    }

    #endregion
}