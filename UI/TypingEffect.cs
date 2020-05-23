using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypingEffect : MonoBehaviour 
{
    //attach to textbox and hook it up,
    //update the text from another script
    
    public GameObject sounds;
    public Text textBox;
    //Store all your text in this string array
    public string text = " ";
    public string prevText = " ";

    private void Start()
    {
        //start sound effect
        //StartCoroutine(AnimateText());
        //throw new NotImplementedException();
    }
    //Note that the speed you want the typewriter effect to be going at is the yield waitforseconds
    //(in my case it's 1 letter for every 0.03 seconds, replace this with a public float if you want to experiment with speed in from the editor)
    
    //StartCoroutine(AnimateText());
    public void Update()
    {
        if (prevText != text && text != "")
        {
            //call your sound effect here
            StartCoroutine(AnimateText());
        }
    }
    
    public IEnumerator AnimateText()
    {
        prevText = text;
        Debug.Log("going");
        for (int i = 0; i < (text.Length+1); i++)
        {
            textBox.text = text.Substring(0, i);
            if (i == (text.Length + 1))
            {
                //Debug.Log("done");
            }
            yield return new WaitForSeconds(.020f);
        }
        //stop sound effect
    }
}


