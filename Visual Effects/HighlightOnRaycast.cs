using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//script by Aaron Freedman
//edits by John Wanamaker
public class HighlightOnRaycast : MonoBehaviour
{
    public Material originalMaterial;
    public Material highlightMaterial;
    [SerializeField] private bool highlighted;
    private Renderer renderer;

    private void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        if (renderer.sharedMaterial != originalMaterial)
        {
            renderer.sharedMaterial = originalMaterial;
            highlighted = false;
        }
    }

    public void Highlight()
    {
        highlighted = true;
        renderer.sharedMaterial = highlightMaterial;
    }

    public void Deselect()
    {
        highlighted = false;
        renderer.sharedMaterial = originalMaterial;
    }
}