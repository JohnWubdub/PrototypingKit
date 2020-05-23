// These are taken from the Cinder implementation of Penner's easing functions
// Starting at the elastic ones I switched to a C# implementation from: http://wpf-animation.googlecode.com/svn/trunk/src/WPF/Animation/PennerDoubleAnimation.cs
// When there's time switch those over to Cinder's implementation as well

using System;
using System.Collections.Generic;
using UnityEngine;

public static class Easing
{
    public delegate float Function(float t);

    public enum FunctionType
    {
        Linear,
        ExpoEaseOut,
        ExpoEaseIn,
        ExpoEaseInOut,
        ExpoEaseOutIn,
        CircEaseOut,
        CircEaseIn,
        CircEaseInOut,
        CircEaseOutIn,
        QuadEaseOut,
        QuadEaseIn,
        QuadEaseInOut,
        QuadEaseOutIn,
        SineEaseOut,
        SineEaseIn,
        SineEaseInOut,
        SineEaseOutIn,
        CubicEaseOut,
        CubicEaseIn,
        CubicEaseInOut,
        CubicEaseOutIn,
        QuartEaseOut,
        QuartEaseIn,
        QuartEaseInOut,
        QuartEaseOutIn,
        QuintEaseOut,
        QuintEaseIn,
        QuintEaseInOut,
        QuintEaseOutIn,
        ElasticEaseOut,
        ElasticEaseIn,
        BounceEaseOut,
        BounceEaseIn,
        BackEaseOut,
        BackEaseIn,
        BackEaseInOut,
        BackEaseOutIn
    }

    private static readonly Dictionary<FunctionType, Function> FunctionMap = new Dictionary<FunctionType, Function>();

    public static Function GetFunctionWithTypeEnum(FunctionType t)
    {
        Function f;
        if (!FunctionMap.TryGetValue(t, out f))
        {
            var name = t.ToString();
            Type[] parameters = {typeof(float)};
            var methodInfo = typeof (Easing).GetMethod(name, parameters);
            f = (Function) Delegate.CreateDelegate(typeof (Function), methodInfo, true);
            FunctionMap[t] = f;
        }
        return f;
    }

    /// Easing equation function for a simple linear tweening, with no easing.
    public static float Linear(float t)
    {
        return t;
    }

    /// Easing equation function for an exponential (2^t) easing out:
    /// decelerating from zero velocity.
    public static float ExpoEaseOut(float t)
    {
        return t == 1 ? 1 : -Mathf.Pow(2, -10 * t) + 1;
    }

    /// Easing equation function for an exponential (2^t) easing in:
    /// accelerating from zero velocity.
    public static float ExpoEaseIn(float t)
    {
        return t == 0 ? 0 : Mathf.Pow(2, 10 * (t - 1));
    }

    /// Easing equation function for an exponential (2^t) easing in/out:
    /// acceleration until halfway, then deceleration.
    public static float ExpoEaseInOut(float t)
    {
        if (t == 0) return 0;
        if (t == 1) return 1;
        t *= 2;
        if (t < 1) return 0.5f * Mathf.Pow(2, 10 * (t - 1));
        return 0.5f * (-Mathf.Pow(2, -10 * (t - 1)) + 2);
    }

    /// Easing equation function for an exponential (2^t) easing out/in:
    /// deceleration until halfway, then acceleration.
    public static float ExpoEaseOutIn(float t)
    {
        if (t < 0.5f) return ExpoEaseOut(2 * t) / 2;
        return ExpoEaseIn(2 * t - 1) / 2 + 0.5f;
    }

    /// Easing equation function for a circular (sqrt(1-t^2)) easing out:
    /// decelerating from zero velocity.
    public static float CircEaseOut(float t)
    {
        t -= 1;
        return Mathf.Sqrt(1 - t * t);
    }

    /// Easing equation function for a circular (sqrt(1-t^2)) easing in:
    /// accelerating from zero velocity.
    public static float CircEaseIn(float t)
    {
        return -(Mathf.Sqrt(1 - t * t) - 1);
    }

    /// Easing equation function for a circular (sqrt(1-t^2)) easing in/out:
    /// acceleration until halfway, then deceleration.
    public static float CircEaseInOut(float t)
    {
        t *= 2;
        if (t < 1)
        {
            return -0.5f * (Mathf.Sqrt(1 - t * t) - 1);
        }
        else {
            t -= 2;
            return 0.5f * (Mathf.Sqrt(1 - t * t) + 1);
        }
    }

    /// Easing equation function for a circular (sqrt(1-t^2)) easing in/out:
    /// acceleration until halfway, then deceleration.
    public static float CircEaseOutIn(float t)
    {
        if (t < 0.5f) return CircEaseOut(2 * t) / 2;
        return CircEaseIn(2 * t - 1) / 2 + 0.5f;
    }

    /// Easing equation function for a quadratic (t^2) easing out:
    /// decelerating from zero velocity.
    public static float QuadEaseOut(float t)
    {
        return -t * (t - 2);
    }

    /// Easing equation function for a quadratic (t^2) easing in:
    /// accelerating from zero velocity.
    public static float QuadEaseIn(float t)
    {
        return t * t;
    }

    /// Easing equation function for a quadratic (t^2) easing in/out:
    /// acceleration until halfway, then deceleration.
    public static float QuadEaseInOut(float t)
    {
        if (t < 0.5f) return QuadEaseOut(t * 2) * 0.5f;
        return QuadEaseIn((2 * t) - 1) * 0.5f + 0.5f;
    }

    /// Easing equation function for a quadratic (t^2) easing out/in:
    /// deceleration until halfway, then acceleration.
    public static float QuadEaseOutIn(float t)
    {
        if (t < 0.5f) return QuadEaseOut(t * 2) * 0.5f;
        return QuadEaseIn((2 * t) - 1) * 0.5f + 0.5f;
    }

    /// Easing equation function for a sinusoidal (sin(t)) easing out:
    /// decelerating from zero velocity.
    public static float SineEaseOut(float t)
    {
        return Mathf.Sin(t * Mathf.PI / 2);
    }

    /// Easing equation function for a sinusoidal (sin(t)) easing in:
    /// accelerating from zero velocity.
    public static float SineEaseIn(float t)
    {
        return -Mathf.Cos(t * Mathf.PI / 2) + 1;
    }

    /// Easing equation function for a sinusoidal (sin(t)) easing in/out:
    /// acceleration until halfway, then deceleration.
    public static float SineEaseInOut(float t)
    {
        if (t < 0.5f) return SineEaseOut(2 * t) / 2;
        return SineEaseIn(2 * t - 1) / 2 + 0.5f;
    }

    /// Easing equation function for a sinusoidal (sin(t)) easing in/out:
    /// deceleration until halfway, then acceleration.
    public static float SineEaseOutIn(float t)
    {
        if (t < 0.5f) return SineEaseOut(2 * t) / 2;
        return SineEaseIn(2 * t - 1) / 2 + 0.5f;
    }

    /// Easing equation function for a cubic (t^3) easing out:
    /// decelerating from zero velocity.
    public static float CubicEaseOut(float t)
    {
        t -= 1;
        return t * t * t + 1;
    }

    /// Easing equation function for a cubic (t^3) easing in:
    /// accelerating from zero velocity.
    public static float CubicEaseIn(float t)
    {
        return t * t * t;
    }

    /// Easing equation function for a cubic (t^3) easing in/out:
    /// acceleration until halfway, then deceleration.
    public static float CubicEaseInOut(float t)
    {
        t *= 2;
        if (t < 1) return 0.5f * t * t * t;
        t -= 2;
        return 0.5f * (t * t * t + 2);
    }

    /// Easing equation function for a cubic (t^3) easing out/in:
    /// deceleration until halfway, then acceleration.
    public static float CubicEaseOutIn(float t)
    {
        if (t < 0.5f) return CubicEaseOut(2 * t) / 2;
        return CubicEaseIn(2 * t - 1) / 2 + 0.5f;
    }

    /// Easing equation function for a quartic (t^4) easing out:
    /// decelerating from zero velocity.
    public static float QuartEaseOut(float t)
    {
        t -= 1;
        return -(t * t * t * t - 1);
    }

    /// Easing equation function for a quartic (t^4) easing in:
    /// accelerating from zero velocity.
    public static float QuartEaseIn(float t)
    {
        return t * t * t * t;
    }

    /// Easing equation function for a quartic (t^4) easing in/out:
    /// acceleration until halfway, then deceleration.
    public static float QuartEaseInOut(float t)
    {
        if (t < 0.5f) return CubicEaseOut(2 * t) / 2;
        return CubicEaseIn(2 * t - 1) / 2 + 0.5f;
    }

    /// Easing equation function for a quartic (t^4) easing out/in:
    /// deceleration until halfway, then acceleration.
    public static float QuartEaseOutIn(float t)
    {
        if (t < 0.5f) return CubicEaseOut(2 * t) / 2;
        return CubicEaseIn(2 * t - 1) / 2 + 0.5f;
    }

    /// Easing equation function for a quintic (t^5) easing out:
    /// decelerating from zero velocity.
    public static float QuintEaseOut(float t)
    {
        t -= 1;
        return t * t * t * t * t + 1;
    }

    /// Easing equation function for a quintic (t^5) easing in:
    /// accelerating from zero velocity.
    public static float QuintEaseIn(float t)
    {
        return t * t * t * t * t;
    }

    /// Easing equation function for a quintic (t^5) easing in/out:
    /// acceleration until halfway, then deceleration.
    public static float QuintEaseInOut(float t)
    {
        t *= 2;
        if (t < 1) return 0.5f * t * t * t * t * t;
        t -= 2;
        return 0.5f * (t * t * t * t * t + 2);
    }

    /// Easing equation function for a quintic (t^5) easing in/out:
    /// acceleration until halfway, then deceleration.
    public static float QuintEaseOutIn(float t)
    {
        if (t < 0.5f) return QuintEaseOut(2 * t) / 2;
        return QuintEaseIn(2 * t - 1) / 2 + 0.5f;
    }

    /// Easing equation function for an elastic (exponentially decaying sine wave) easing out:
    /// decelerating from zero velocity.
    public static float ElasticEaseOut(float t)
    {
        if (t == 1) return 1;

        var p = 0.3f;
        var s = p / 4;

        return (Mathf.Pow(2, -10 * t) * Mathf.Sin((t - s) * (2 * Mathf.PI) / p) + 1);
    }

    /// Easing equation function for an elastic (exponentially decaying sine wave) easing in:
    /// accelerating from zero velocity.
    public static float ElasticEaseIn(float t)
    {
        if (t == 1) return 1;

        const float p = 0.3f;
        const float s = p / 4;

        return -(Mathf.Pow(2, 10 * (t -= 1)) * Mathf.Sin((t - s) * (2 * Mathf.PI) / p));
    }

    /// Easing equation function for a bounce (exponentially decaying parabolic bounce) easing out:
    /// decelerating from zero velocity.
    public static float BounceEaseOut(float t)
    {
        if (t < (1 / 2.75f))
            return (7.5625f * t * t);
        else if (t < (2 / 2.75f))
            return (7.5625f * (t -= (1.5f / 2.75f)) * t + .75f);
        else if (t < (2.5 / 2.75f))
            return (7.5625f * (t -= (2.25f / 2.75f)) * t + .9375f);
        else
            return (7.5625f * (t -= (2.625f / 2.75f)) * t + .984375f);
    }

    /// Easing equation function for a bounce (exponentially decaying parabolic bounce) easing in:
    /// accelerating from zero velocity.
    public static float BounceEaseIn(float t)
    {
        return 1 - BounceEaseOut(1 - t);
    }

    /// Easing equation function for a back (overshooting cubic easing: (s+1)*t^3 - s*t^2) easing out:
    /// decelerating from zero velocity.
    public static float BackEaseOut(float t)
    {
        t -= 1;
        return (t * t * ((1.70158f + 1) * t + 1.70158f) + 1);
    }

    /// Easing equation function for a back (overshooting cubic easing: (s+1)*t^3 - s*t^2) easing in:
    /// accelerating from zero velocity.
    public static float BackEaseIn(float t)
    {
        return t * t * ((1.70158f + 1) * t - 1.70158f);
    }

    /// Easing equation function for a back (overshooting cubic easing: (s+1)*t^3 - s*t^2) easing in/out:
    /// acceleration until halfway, then deceleration.
    public static float BackEaseInOut(float t)
    {
        float s = 1.70158f;
        t *= 2;
        if (t < 1)
        {
            s *= 1.525f;
            return 0.5f * (t * t * ((s + 1) * t - s));
        }
        else {
            t -= 2;
            s *= 1.525f;
            return 0.5f * (t * t * ((s + 1) * t + s) + 2);
        }
    }

    /// Easing equation function for a back (overshooting cubic easing: (s+1)*t^3 - s*t^2) easing out/in:
    /// deceleration until halfway, then acceleration.
    public static float BackEaseOutIn(float t)
    {
        if (t < 0.5f) return BackEaseOut(2 * t) / 2;
        return BackEaseIn(2 * t - 1) / 2 + 0.5f;
    }

}
