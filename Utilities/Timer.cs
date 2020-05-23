// Timer.cs
// Last edited 12:21 PM 05/16/2015 by Aaron Freedman

using UnityEngine;

public static class Timer
{
    public static void DecrementTimer(float _value)
    {
        _value = _value -= Time.deltaTime;
    }
}