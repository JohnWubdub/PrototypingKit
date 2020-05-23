// ComponentExtensions.cs
// Last edited 9:57 AM 07/20/2015 by Aaron Freedman

using System;
using System.Reflection;
using UnityEngine;

// http://answers.unity3d.com/questions/530178/how-to-get-a-component-from-an-object-and-add-it-t.html
public static class ComponentExtensions
{
    public static T GetCopyOf<T>(this Component comp, T other) where T : Component
    {
        Type type = comp.GetType();
        if (type != other.GetType()) return null; // type mis-match
        BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Default |
                             BindingFlags.DeclaredOnly;
        PropertyInfo[] pinfos = type.GetProperties(flags);
        foreach (PropertyInfo pinfo in pinfos)
        {
            if (pinfo.CanWrite)
            {
                try
                {
                    pinfo.SetValue(comp, pinfo.GetValue(other, null), null);
                }
                catch {}
                    // In case of NotImplementedException being thrown. For some reason specifying that exception didn't seem to catch it, so I didn't catch anything specific.
            }
        }
        FieldInfo[] finfos = type.GetFields(flags);
        foreach (FieldInfo finfo in finfos)
        {
            finfo.SetValue(comp, finfo.GetValue(other));
        }
        return comp as T;
    }

    public static T AddComponent<T>(this GameObject go, T toAdd) where T : Component
    {
        return go.AddComponent<T>().GetCopyOf(toAdd);
    }

    public static bool GetComponent<T>(this GameObject obj, T component, out T _out) where T : Component
    {
        _out = null;
        var c = obj.GetComponent<T>();
        if (!c) return false;
        _out = c;
        return true;
    }

    public static Vector2 ToVector2(this Vector3 v)
    {
        return new Vector2(v.x, v.y);
    }

    public static Vector3 ToVector3(this Vector2 v, Transform originTransform)
    {
        return new Vector3(v.x, v.y, originTransform.position.z);
    }
}