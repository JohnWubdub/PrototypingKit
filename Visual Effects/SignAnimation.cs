// SignAnimation.cs
// Last edited 9:58 PM 04/21/2015 by Aaron Freedman

using System.Collections;
using Assets.PrototypingKit.Utilities;
using UnityEngine;

namespace Assets.PrototypingKit
{
    /// <summary>
    ///     Uses a glitch-like flicker coroutine to turn the brightness of associated <c>MeshRenderers</c> on and off
    /// </summary>
    public class SignAnimation : MonoBehaviour
    {
        public GameObject signElement;
        private MeshRenderer[] elements;
        public float delayMin;
        public float delayMax;
        public int glitchIterationsMin;
        public int glitchIterationsMax;
        private int glitchAmount;
        private float lastGlitch;
        private float nextGlitch;
        public float iterationGapMin;
        public float iterationGapMax;
        private float[] origRed;
        private float[] origBlue;
        private float[] origGreen;

        private void Start()
        {
            elements = signElement.GetComponentsInChildren<MeshRenderer>();
            int size = elements.Length;
            lastGlitch = Time.time;
            CalculateNextGlitch();
            origBlue = new float[size];
            origRed = new float[size];
            origGreen = new float[size];
            for (var i = 0; i < elements.Length; i++)
            {
                origBlue[i] = elements[i].material.color.b;
                origRed[i] = elements[i].material.color.r;
                origGreen[i] = elements[i].material.color.g;
            }
        }

        private void Update()
        {
            if (Time.time > nextGlitch)
            {
                StartCoroutine(GlitchColors());
            }
        }

        private void CalculateNextGlitch()
        {
            glitchAmount = Random.Range(glitchIterationsMin, glitchIterationsMax);
            nextGlitch = lastGlitch + Random.Range(delayMin, delayMax);
        }

        private IEnumerator GlitchColors()
        {
            for (var i = 0; i < glitchAmount; i++)
            {
                foreach (MeshRenderer t in elements)
                {
                    HSBColor hsb = HSBColor.FromColor(t.material.color);
                    hsb.b = 0;
                    t.material.color = HSBColor.ToColor(hsb);
                }
                yield return new WaitForSeconds(Random.Range(iterationGapMin, iterationGapMax)); // wait
                for (var j = 0; j < elements.Length; j++)
                {
                    elements[j].material.color = new Color(origRed[j], origGreen[j], origBlue[j]);
                }
            }

            lastGlitch = Time.time;
            CalculateNextGlitch();
        }
    }
}