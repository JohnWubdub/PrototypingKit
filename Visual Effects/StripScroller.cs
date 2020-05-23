// StripScroller.cs
// Last edited 7:43 PM 04/15/2015 by Aaron Freedman

using UnityEngine;

namespace Assets.PrototypingKit
{
    /// <summary>
    ///     Makes a textured object behave like a parallax background image
    /// </summary>
    public class StripScroller : MonoBehaviour
    {
        public float scrollSpeed;
        public float tileSizeZ;
        private Vector2 _savedOffset;
        private Vector3 _startPosition;

        private void Start()
        {
            _startPosition = transform.position;
            _savedOffset = GetComponent<Renderer>().sharedMaterial.GetTextureOffset("_MainTex");
        }

        private void Update()
        {
            float x = Mathf.Repeat(Time.time * scrollSpeed, tileSizeZ * 4);
            x = x / tileSizeZ;
            x = Mathf.Floor(x);
            x = x / 4;
            var offset = new Vector2(x, _savedOffset.y);
            GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", offset);
            float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSizeZ);
            transform.position = _startPosition + Vector3.back * newPosition;
        }

        private void OnDisable()
        {
            GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", _savedOffset);
        }
    }
}