using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Oscillator : MonoBehaviour
{
    public double frequency = 440.0;
    private double increment;
    private double phase;
    private double sampling_frequency = 48000.0;

    public float gain;
    public float volume = 0.1f;

    public float[] frequencies;
    public int thisFreq;


    void Start()
    {
        frequencies = new float[8];
        frequencies[0] = 440;
        frequencies[1] = 494;
        frequencies[2] = 554;
        frequencies[3] = 587;
        frequencies[4] = 659;
        frequencies[5] = 740;
        frequencies[6] = 831;
        frequencies[7] = 880;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            gain = volume;
            frequency = frequencies[thisFreq];
            thisFreq += 1;
            thisFreq = thisFreq % frequencies.Length;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            gain = 0;
        }
    }

    void OnAudioFilterRead(float[] data, int channels)
    {
        increment = frequency * 2.0 * Math.PI / sampling_frequency;

        for (int i = 0; i < data.Length; i += channels)
        {
            phase += increment;
            
            //sin wave
            //data[i] = (float) (gain * Mathf.Sin((float) phase));
            
            
            //square wave
            if (gain * Mathf.Sin((float) phase) >= 0 * gain)
            {
                data[i] = (float) gain * 0.6f;
            }
            else
            {
                data[i] = (-(float) gain) * 0.6f;
            }
             
            
            //triangle wave
            //data[i] /= (float)(gain * (double)Mathf.PingPong((float) phase, 1.0f));
            
            

            if (channels == 2)
            {
                data[i + 1] = data[i];
            }

            if (phase > (Mathf.PI * 2))
            {
                phase = 0.0;
            }
        }
    }

}
