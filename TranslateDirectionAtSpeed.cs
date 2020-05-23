// TranslateDirectionAtSpeed.cs
// Last edited 7:43 PM 04/15/2015 by Aaron Freedman

using UnityEngine;

namespace Assets.PrototypingKit
{
    public class TranslateDirectionAtSpeed : MonoBehaviour
    {
        [SerializeField] private Vector3 direction;
        public float speed;

        private void Start() {}

        private void Update()
        {
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }
}