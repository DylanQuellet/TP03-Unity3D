using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierCurveRecursive : MonoBehaviour
{
    public Transform[] controlPoints; // Tableau des points de contrôle
    public int curveResolution = 50;  // Nombre de points sur la courbe

    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        DrawBezierCurve();
    }

    void Update()
    {
        DrawBezierCurve();
    }

    void DrawBezierCurve()
    {
        Vector3[] positions = new Vector3[curveResolution + 1];
        float tStep = 1.0f / curveResolution;

        for (int i = 0; i <= curveResolution; i++)
        {
            float t = i * tStep;
            positions[i] = CalculateBezierPoint(t, controlPoints);
        }

        lineRenderer.positionCount = positions.Length;
        lineRenderer.SetPositions(positions);
    }

    Vector3 CalculateBezierPoint(float t, Transform[] points)
    {
        if (points.Length == 1)
        {
            return points[0].position;
        }

        Vector3[] nextPoints = new Vector3[points.Length - 1];

        for (int i = 0; i < points.Length - 1; i++)
        {
            nextPoints[i] = Vector3.Lerp(points[i].position, points[i + 1].position, t);
        }

        return CalculateBezierPoint(t, nextPoints);
    }

    Vector3 CalculateBezierPoint(float t, Vector3[] points)
    {
        if (points.Length == 1)
        {
            return points[0];
        }

        Vector3[] nextPoints = new Vector3[points.Length - 1];

        for (int i = 0; i < points.Length - 1; i++)
        {
            nextPoints[i] = Vector3.Lerp(points[i], points[i + 1], t);
        }

        return CalculateBezierPoint(t, nextPoints);
    }
}