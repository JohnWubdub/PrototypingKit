using System.Collections;
using UnityEngine;

public static class Coroutines
{
    public delegate void TimedAction(float t);

    public static IEnumerator DoOverTime(float duration, TimedAction action)
    {
        var startTime = Time.time;
        var endTime = startTime + duration;
        var currentTime = startTime;
        while (currentTime <= endTime)
        {
            var t = (currentTime - startTime) / duration;
            action(t);
            yield return null;
            currentTime = Time.time;
        }
    }

    public static IEnumerator DoOverEasedTime(float duration, Easing.Function func, TimedAction timedAction)
    {
        return DoOverTime(duration, t=> { timedAction(func(t)); });
    }

}