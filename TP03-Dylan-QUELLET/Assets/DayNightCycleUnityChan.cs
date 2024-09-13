using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycleUnityChan : MonoBehaviour
{
    public Light directionalLight;
    public Light moonLight;

    // Vitesse du cycle jour/nuit (en degrés par seconde)
    public float dayCycleSpeed = 10f;

    public Color dayColor = Color.white;
    public Color duskDawnColor = new Color(1f, 0.64f, 0.38f);
    public Color nightColor = new Color(0.1f, 0.1f, 0.35f);

    public float maxLightIntensity = 1.0f;
    public float minLightIntensity = 0.2f;
    public float moonLightIntensity = 0.5f;

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
            moonLight.enabled = false;
            directionalLight.enabled = true;

            // Interpolation entre la lumière du jour et du crépuscule selon l'angle
            directionalLight.intensity = Mathf.Lerp(minLightIntensity, maxLightIntensity, (sunAngle / 90f));
            directionalLight.color = Color.Lerp(duskDawnColor, dayColor, (sunAngle / 90f));
        }
        else // Soleil sous l'horizon (nuit)
        {
            directionalLight.enabled = false;
            moonLight.enabled = true;

            // Interpolation entre crépuscule et nuit pour la couleur lunaire
            moonLight.intensity = Mathf.Lerp(0f, moonLightIntensity, ((sunAngle - 180f) / 90f));
            moonLight.color = nightColor;
        }
    }



}
