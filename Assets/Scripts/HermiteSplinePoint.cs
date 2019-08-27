using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HermiteSplinePoint : MonoBehaviour
{
    public GameObject TangenceMarker;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 GetTangent()
    {
        Vector3 tangent = TangenceMarker.transform.position - this.transform.position;

        // for certain tight changes in destination, the tangeant marker has to be unintuitively far away to clear something non-linear. The constant multiplier is meant to strengthen the influence of distance on the tangeant.
        return 3.0f * tangent;
    }
}
