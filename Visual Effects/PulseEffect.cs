using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseEffect : MonoBehaviour //pulse effect on the color and size of an object
{
    public float pulsePeriod;
    public float sizeMultiplier;
    public float gravity;
    private float lastPulse;
    private Vector3 originalScale;
    public bool flashOnPulse;
    public Color flashColor;
    public float colorGravity;
    private Color originalColor;
    public bool autoTween;

    private void Start()
    {
        lastPulse = Time.time;
        originalScale = transform.localScale;
        originalColor = GetComponent<Renderer>().material.color;
    }

    private void FixedUpdate()
    {
        if (autoTween)
        {
            if (Time.time > lastPulse + pulsePeriod)
            {
                Pulse();
            }
        }
        if (transform.localScale != originalScale)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, originalScale, gravity * Time.deltaTime);
        }
        if (GetComponent<Renderer>().material.color != originalColor)
        {
            if (flashOnPulse)
            {
                GetComponent<Renderer>().material.color = Color.Lerp(GetComponent<Renderer>().material.color, originalColor,
                    colorGravity * Time.deltaTime);
            }
        }
    }

    public void Pulse()
    {
        Pulse(flashColor);
    }

    public void Pulse(Color color)
    {
        Pulse(color, sizeMultiplier);
    }

    public void Pulse(Color color, float scale)
    {
        lastPulse = Time.time;
        transform.localScale *= scale;
        if (flashOnPulse)
        {
            GetComponent<Renderer>().material.color = color;
        }
    }
}

