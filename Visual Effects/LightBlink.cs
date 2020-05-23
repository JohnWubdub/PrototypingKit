using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//script by Aaron Freedman
//edits by John Wanamaker
public class LightBlink : MonoBehaviour
{
    //attach to light and fuck around
    [Range(0, 1.0f)]
    public float lightMaxIntensity;
    [Range(0, 1.0f)]
    public float lightMinIntensity;
    private Light light;
    public float speed;

    private void Start()
    {
        light = GetComponent<Light>();
        light.intensity = light.intensity + (Random.Range(-0.1f, 0.1f));

        if (lightMaxIntensity < lightMinIntensity)
        {
            lightMaxIntensity = lightMinIntensity;
        }
    }

    private void Update()
    {
        light.intensity = ((Mathf.Sin(Time.time * speed) + 0.5f) * (lightMaxIntensity - lightMinIntensity)) + lightMinIntensity;
    }
}
