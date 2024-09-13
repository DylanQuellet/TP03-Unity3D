using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierCurve : MonoBehaviour
{
    public Transform[] controlPointsQuadratic = new Transform[3];  // Pour la courbe quadratique
    public Transform[] controlPointsCubic = new Transform[4];      // Pour la courbe cubique
    public int curveResolution = 50;                               // Nombre de points sur la courbe

    private LineRenderer lineRenderer;
    public bool isCubicCurve = false;  // Basculer entre quadratique et cubique

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        DrawCurve();
    }

    void Update()
    {
        DrawCurve();  // Met à jour la courbe en temps réel
    }

    // Tracer la courbe
    void DrawCurve()
    {
        Vector3[] positions = new Vector3[curveResolution + 1];

        for (int i = 0; i <= curveResolution; i++)
        {
            float t = i / (float)curveResolution;
            if (isCubicCurve)
            {
                positions[i] = CalculateCubicBezierPoint(t, controlPointsCubic[0].position, controlPointsCubic[1].position, controlPointsCubic[2].position, controlPointsCubic[3].position);
            }
            else
            {
                positions[i] = CalculateQuadraticBezierPoint(t, controlPointsQuadratic[0].position, controlPointsQuadratic[1].position, controlPointsQuadratic[2].position);
            }
        }

        lineRenderer.positionCount = positions.Length;
        lineRenderer.SetPositions(positions);
    }

    // Bézier quadratique
    Vector3 CalculateQuadraticBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        return u * u * p0 + 2 * u * t * p1 + t * t * p2;
    }

    // Bézier cubique
    Vector3 CalculateCubicBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float u = 1 - t;
        return u * u * u * p0 + 3 * u * u * t * p1 + 3 * u * t * t * p2 + t * t * t * p3;
    }
}
