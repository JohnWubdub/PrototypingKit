// MathExtensions.cs
// Last edited 7:43 PM 04/15/2015 by Aaron Freedman

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.PrototypingKit.Utilities
{
    public class MathExtensions : MonoBehaviour
    {
        public static float AngleRad(Vector2 a, Vector2 b)
        {
            return Mathf.Atan2(a.x * b.y - a.y * b.x, a.x * b.y + a.y * b.x);
        }

        public static float Angle(Vector2 a, Vector2 b)
        {
            return Mathf.Atan2(a.x * b.y - a.y * b.x, a.x * b.y + a.y * b.x) * Mathf.Rad2Deg;
        }

        public static Func<double, double> SimpleMovingAverage(int window)
        {
            Queue<double> s = new Queue<double>(window);
            return x =>
            {
                if (s.Count >= window)
                    s.Dequeue();

                s.Enqueue(x);
                return s.Average();
            };
        }

        public static float Map(float value, float min, float max, float mapMin, float mapMax)
        {
            return (value / (max - min)) * (mapMax - mapMin);
        }
    }
}