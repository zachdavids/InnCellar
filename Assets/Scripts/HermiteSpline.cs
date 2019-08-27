using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HermiteSpline : MonoBehaviour
{
    public List<GameObject> _splinePoints;

    public Vector3 InterpolateSpline(float step)
    {
        float linelengthfactor = 1.0f / (_splinePoints.Count - 1);
        int curveIndex = Mathf.FloorToInt(step / linelengthfactor);

        if (curveIndex >= _splinePoints.Count - 1)
        {
            curveIndex = _splinePoints.Count - 2;
        }

        HermiteSplinePoint origin = _splinePoints[curveIndex].GetComponent<HermiteSplinePoint>();
        HermiteSplinePoint destination = _splinePoints[curveIndex + 1].GetComponent<HermiteSplinePoint>();

        float substep = (step - (curveIndex * linelengthfactor)) / linelengthfactor;

        return InterpolateCurve(origin, destination, substep);
    }

    private Vector3 InterpolateCurve(HermiteSplinePoint origin, HermiteSplinePoint dest, float step)
    {
        Vector3 position = (HermBasisOrigin(step) * origin.transform.position) + (HermBasisDestination(step) * 
            dest.transform.position) + (HermBasisInitTangent(step) * 
            origin.GetTangent()) + (HermBasisDestTangent(step) * dest.GetTangent());

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
        float interval = 0.02f;
        float fStep = step + interval;

        if (fStep > 1.0f)
        {
            fStep = 1.0f;
        }

        Vector3 estimatedTangeant = InterpolateSpline(fStep) - InterpolateSpline(step);
        return estimatedTangeant.normalized;
    }

    public Vector3 GetInitialPoint()
    {
        return _splinePoints[0].transform.position;
    }

    public Vector3 GetFinalPoint()
    {
        return _splinePoints[_splinePoints.Count - 1].transform.position;
    }
}
