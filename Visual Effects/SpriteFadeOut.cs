using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//script by Aaron Freedman
//edits by John Wanamaker
public class SpriteFadeOut : MonoBehaviour
{
    //fades the a color value of the sprite to make it fade away
    
    public float speed;
    public float startDelay;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (startDelay > 0)
        {
            startDelay -= Time.deltaTime;
            return;
        }
        
        var sprite = GetComponent<SpriteRenderer>();
        sprite.color = Color.Lerp(sprite.color, Color.clear, Time.deltaTime * speed);
    }
}