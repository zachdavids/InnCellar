using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HermiteSplinePoint : MonoBehaviour
{
    public GameObject TangenceMarker;

    public Vector3 GetTangent()
    {
        Vector3 tangent = TangenceMarker.transform.position - this.transform.position;

        return 3.0f * tangent;
    }
}
