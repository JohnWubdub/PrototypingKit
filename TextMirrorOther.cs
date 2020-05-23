// TextMirrorOther.cs
// Last edited 7:43 PM 04/15/2015 by Aaron Freedman

using UnityEngine;
using UnityEngine.UI;

namespace Assets.PrototypingKit
{
    [RequireComponent(typeof (Text))]
    /// <summary>
    /// This script forces the Text it is attached to to mirror the other Text object
    /// </summary>
    public class TextMirrorOther : MonoBehaviour
    {
        private Text _thisText;
        public Text TextToMirror;

        private void Start()
        {
            _thisText = GetComponent<Text>();
        }

        private void Update()
        {
            _thisText.text = TextToMirror.text;
        }
    }
}