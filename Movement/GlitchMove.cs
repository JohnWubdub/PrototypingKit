using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//script by Aaron Freedman
//edits by John Wanamaker
public class GlitchMove : MonoBehaviour
{
    //break your game,
    //but for fun!

    public Vector3 magnitude;
    public Vector3 magnitudeMin;
    private Vector3 originalPos;
    public float delayMin;
    public float delayMax;
    public int glitchIterationsMin;
    public int glitchIterationsMax;
    private int glitchAmount;
    private float lastGlitch;
    private float nextGlitch;
    public float iterationGapMin;
    public float iterationGapMax;
    //public bool useAudio;
    //public AudioClip[] glitchClip;

    private void Start()
    {
        originalPos = transform.localPosition;
        lastGlitch = Time.time;
        CalculateNextGlitch();
        /*
        if (GetComponent<AudioSource>() == null)
        {
            gameObject.AddComponent<AudioSource>();
        }
        */
    }

    private void Update()
    {
        if (Time.time > nextGlitch)
        {
            StartCoroutine(Glitch());
        }
    }

    private IEnumerator Glitch()
    {
        for (var i = 0; i < glitchAmount; i++)
        {
            /*
            if (useAudio)
            {
                GetComponent<AudioSource>().PlayOneShot(glitchClip[Random.Range(0, glitchClip.Length)]);
            }
            */
            transform.Translate(Random.Range(-magnitude.x - magnitudeMin.x, magnitude.x + magnitudeMin.x),
                                Random.Range(-magnitude.y - magnitudeMin.y, magnitude.y + magnitudeMin.y),
                                Random.Range(-magnitude.z - magnitudeMin.z, magnitude.z + magnitudeMin.z));
            yield return new WaitForSeconds(Random.Range(iterationGapMin, iterationGapMax));
            transform.localPosition = originalPos;
        }
        lastGlitch = Time.time;
        transform.localPosition = originalPos;
        CalculateNextGlitch();
    }

    private void CalculateNextGlitch()
    {
        glitchAmount = Random.Range(glitchIterationsMin, glitchIterationsMax);
        nextGlitch = lastGlitch + Random.Range(delayMin, delayMax);
    }
}