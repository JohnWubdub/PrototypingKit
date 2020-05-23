// Colorx.cs
// Last edited 7:43 PM 04/15/2015 by Aaron Freedman

using UnityEngine;

namespace Assets.PrototypingKit.Utilities
{
    /// <summary>
    ///     This script provides a helper function to tie in with HSBColor that will let you slerp between two Unity Colors.
    ///     The slerp function provides much more pleasing visual results than Color.Lerp.
    ///     Author: Jonathan Czeck (aarku)
    ///     http://wiki.unity3d.com/index.php/Colorx
    /// </summary>
    public class Colorx
    {
        public static Color Slerp(Color a, Color b, float t)
        {
            return (HSBColor.Lerp(HSBColor.FromColor(a), HSBColor.FromColor(b), t)).ToColor();
        }
    }
}