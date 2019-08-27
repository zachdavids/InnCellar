using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawMineRails : MonoBehaviour
{
    private List<GameObject> rails;
    // Start is called before the first frame update
    void Start()
    {
        HermiteSpline spline = this.gameObject.GetComponent<HermiteSpline>();
        ResourceManager resourceManager = GameObject.Find("ResourceManager").GetComponent<ResourceManager>();

        rails = new List<GameObject>();

        for(float i = 0.0f; i < 1.0f; i = i + 0.02f)
        {
            GameObject rail = new GameObject("MineRail");

            rail.AddComponent<MeshFilter>().mesh = resourceManager.RailMesh;
            rail.AddComponent<MeshRenderer>().material = resourceManager.RailMaterial;

            rail.transform.localScale = 0.15f * this.transform.localScale;
            rail.transform.position = spline.InterpolateSpline(i);
            rail.transform.LookAt(rail.transform.position + spline.GetTangeantAtStep(i), Vector3.up);
            rail.transform.Rotate(0, 90, 0);

            rail.transform.parent = this.transform;
            rails.Add(rail);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
