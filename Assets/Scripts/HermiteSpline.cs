using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HermiteSpline : MonoBehaviour
{
    public List<GameObject> SplinePoints;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 InterpolateSpline(float step)
    {
        // by and large we're gonna be pretty lazy and not consider relative size of our splines, thus, each concatinated curve will be assumed to require the same traversal time.
        // We could fix this later by using some length approximation metric such as the total distance between the lines joining the start and end points to the curve's center.
        float linelengthfactor = 1.0f / (SplinePoints.Count - 1);
        int curveIndex = Mathf.FloorToInt(step / linelengthfactor);
        if (curveIndex >= SplinePoints.Count - 1) curveIndex = SplinePoints.Count - 2;
        float substep = (step - (curveIndex * linelengthfactor)) / linelengthfactor;

        HermiteSplinePoint origin = SplinePoints[curveIndex].GetComponent<HermiteSplinePoint>();
        HermiteSplinePoint destination = SplinePoints[curveIndex + 1].GetComponent<HermiteSplinePoint>();

        return InterpolateCurve(origin, destination, substep);
    }

    private Vector3 InterpolateCurve(HermiteSplinePoint origin, HermiteSplinePoint dest, float step)
    {
        Vector3 position = (HermBasisOrigin(step) * origin.transform.position) + (HermBasisDestination(step) * dest.transform.position) + (HermBasisInitTangent(step) * origin.GetTangent()) + (HermBasisDestTangent(step) * dest.GetTangent());
        return position;
    }

    private float HermBasisOrigin(float step)
    {
        return (2.0f * Mathf.Pow(step, 3)) - (3.0f * Mathf.Pow(step, 2)) + 1.0f;
    }

    private float HermBasisDestination(float step)
    {
        return (-2.0f * Mathf.Pow(step, 3)) + (3.0f * Mathf.Pow(step, 2));
    }

    private float HermBasisInitTangent(float step)
    {
        return (Mathf.Pow(step, 3)) - (2.0f * Mathf.Pow(step, 2)) + step;
    }

    private float HermBasisDestTangent(float step)
    {
        return Mathf.Pow(step, 3) - Mathf.Pow(step, 2);
    }

    public Vector3 GetTangeantAtStep(float step)
    {
        // Estimate the tangeant a given step
        float interval = 0.02f;
        float fStep = step + interval;
        if (fStep > 1.0f)
            fStep = 1.0f;
        Vector3 estimatedTangeant = InterpolateSpline(fStep) - InterpolateSpline(step);
        return estimatedTangeant.normalized;
    }

    public Vector3 GetInitialPoint()
    {
        return SplinePoints[0].transform.position;
    }

    public Vector3 GetFinalPoint()
    {
        return SplinePoints[SplinePoints.Count - 1].transform.position;
    }

}
