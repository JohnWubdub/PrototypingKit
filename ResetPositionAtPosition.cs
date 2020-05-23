// ResetPositionAtPosition.cs
// Last edited 7:43 PM 04/15/2015 by Aaron Freedman

using UnityEngine;

namespace Assets.PrototypingKit
{
    public class ResetPositionAtPosition : MonoBehaviour
    {
        public Vector3 originalPosition;
        public float resetAfterTranslation;

        // Use this for initialization
        private void Start()
        {
            originalPosition = transform.position;
        }

        // Update is called once per frame
        private void Update()
        {
            if (Mathf.Abs(transform.position.x - originalPosition.x) > resetAfterTranslation)
            {
                transform.position = originalPosition;
            }
        }
    }
}