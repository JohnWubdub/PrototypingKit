// ExpandTween.cs
// Last edited 6:15 PM 07/14/2015 by Aaron Freedman

using UnityEngine;

public class ExpandTween : MonoBehaviour
{
    public Vector3 targetSize;
    public Vector3 originalSize;
    public float scaleSpeed;
    public bool active;
    public  new Renderer renderer;

    private void Start()
    {
        originalSize = transform.localScale;
        renderer = GetComponent<Renderer>();
        renderer.enabled = false;
        active = false;
    }

    private void Update()
    {
        if (active && transform.localScale.sqrMagnitude < targetSize.sqrMagnitude * 0.95f) transform.localScale = Vector3.Lerp(transform.localScale, targetSize, scaleSpeed * Time.deltaTime);
        else renderer.enabled = false;
    }

    public void Activate()
    {
        active = true;
        transform.localScale = originalSize;
        renderer.enabled = true;
    }
}