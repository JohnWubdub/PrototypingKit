// HighlightOnRaycast.cs
// Last edited 8:00 PM 04/15/2015 by Aaron Freedman

using UnityEngine;

namespace Assets.PrototypingKit
{
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
}