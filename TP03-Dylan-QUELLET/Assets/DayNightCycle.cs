using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public Light directionalLight;

    // Vitesse du cycle jour/nuit (en degrés par seconde)
    public float dayCycleSpeed = 10f;

    public Color dayColor = Color.white;

    public Color duskDawnColor = new Color(1f, 0.64f, 0.38f);

    public Color nightColor = new Color(0.1f, 0.1f, 0.35f);

    public float maxLightIntensity = 1.0f;

    public float minLightIntensity = 0.2f;

    void Update()
    {
        transform.Rotate(Vector3.right, dayCycleSpeed * Time.deltaTime);

        float sunAngle = transform.eulerAngles.x;

        UpdateLighting(sunAngle);
    }

    void UpdateLighting(float sunAngle)
    {
        if (sunAngle > 0 && sunAngle < 180) // Soleil au-dessus de l'horizon (jour)
        {
            // Interpolation entre la lumière du jour et du crépuscule selon l'angle
            directionalLight.intensity = Mathf.Lerp(minLightIntensity, maxLightIntensity, (sunAngle / 90f));
            directionalLight.color = Color.Lerp(duskDawnColor, dayColor, (sunAngle / 90f));
        }
        else // Soleil sous l'horizon (nuit)
        {
            // Interpolation entre crépuscule et nuit
            directionalLight.intensity = Mathf.Lerp(minLightIntensity, 0f, ((sunAngle - 180f) / 90f));
            directionalLight.color = Color.Lerp(duskDawnColor, nightColor, ((sunAngle - 180f) / 90f));
        }
    }
}
