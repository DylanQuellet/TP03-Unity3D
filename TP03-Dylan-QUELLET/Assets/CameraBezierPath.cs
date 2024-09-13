using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBezierPath : MonoBehaviour
{
    public Transform[] controlPoints; // Points de contrôle pour la courbe de Bézier
    public float duration = 10f;       // Durée totale du trajet
    public bool loop = false;          // Pour répéter le trajet en boucle

    private float t;                   // Paramètre de la courbe
    private float timeElapsed;         // Temps écoulé

    void Update()
    {
        timeElapsed += Time.deltaTime;
        t = timeElapsed / duration;

        Vector3 position = CalculateBezierPoint(t, controlPoints);
        transform.position = position;

        // Orienter la caméra dans la direction du mouvement
        if (controlPoints.Length > 1)
        {
            Vector3 nextPoint = CalculateBezierPoint(Mathf.Clamp01(t + 0.01f), controlPoints);
            transform.LookAt(nextPoint);
        }

        if (loop && t >= 1f)
        {
            timeElapsed = 0f;
        }
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