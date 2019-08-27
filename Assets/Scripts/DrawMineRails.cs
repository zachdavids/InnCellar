using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawMineRails : MonoBehaviour
{
    [SerializeField] private Mesh RailMesh;
    [SerializeField] private Material RailMaterial;

    private List<GameObject> _rails;

    void Start()
    {
        HermiteSpline spline = this.gameObject.GetComponent<HermiteSpline>();

        _rails = new List<GameObject>();

        for (float i = 0.0f; i < 1.0f; i = i + 0.02f)
        {
            GameObject rail = new GameObject("MineRail");

            rail.AddComponent<MeshFilter>().mesh = RailMesh;
            rail.AddComponent<MeshRenderer>().material = RailMaterial;

            Transform railTransform = rail.transform;
            railTransform.localScale = 0.15f * this.transform.localScale;
            railTransform.position = spline.InterpolateSpline(i);
            railTransform.LookAt(rail.transform.position + spline.GetTangeantAtStep(i), Vector3.up);
            railTransform.Rotate(0, 90, 0);
            railTransform.parent = this.transform;

            _rails.Add(rail);
        }
    }
}
