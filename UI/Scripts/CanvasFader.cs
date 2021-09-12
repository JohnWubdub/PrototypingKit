using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//script by John Wanamaker
public class CanvasFader : MonoBehaviour 
{
    //add it to your canvas element,
    //add a canvas group to the element,
    //change it's alpha value to what you want it to start at
    
    public float timer;
    public float baseT = 1f;
    public CanvasGroup cg;

    void Start()
    {
        timer = baseT;
        cg = this.gameObject.GetComponent<CanvasGroup>();
    }

    public void Run(bool fadeIn, bool fadeOut) //true, false = fade in    false, true = fade out
    {
        
        if (fadeIn)
        {
            StartCoroutine("FadeIn");
        }
        if (fadeOut)
        {
            StartCoroutine("FadeOut");
        }
    }

    IEnumerator FadeIn()
    {
        for (float f = 0.05f; f <= 1; f += 0.05f)
        {
            cg.alpha = f;
            yield return new WaitForSeconds(0.1f);
        }
        cg.alpha = 1;
    }
    
    
    IEnumerator FadeOut()
    {
        for (float f = 1f; f >= 0; f -= 0.05f)
        {
            cg.alpha = f;
            yield return new WaitForSeconds(0.1f);
        }
        cg.alpha = 0;
    }
}

