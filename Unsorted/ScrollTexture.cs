// ScrollTexture.cs
// Last edited 11:02 AM 06/03/2015 by Aaron Freedman

using UnityEngine;

public class ScrollTexture : MonoBehaviour
{
    public Vector2 speed;
    private Renderer rend;

    private void Start()
    {
        rend = GetComponent<Renderer>();
    }

    private void Update()
    {
        float scaleX = Mathf.Cos(Time.time) * 0.5F + 1;
        float scaleY = Mathf.Sin(Time.time) * 0.5F + 1;
        rend.material.mainTextureScale = new Vector2(scaleX, scaleY);
    }
}