// GrowOnSelect.cs
// Last edited 11:27 AM 06/09/2015 by Aaron Freedman

using System;
using UnityEngine;

public class GrowOnSelect : MonoBehaviour
{
    public Vector3 originalPos;
    public Vector3 originalScale;
    private Vector3 dir;
    public float percentMove;
    public float percentToGrow;
    public bool selected;
    public float smoothSpeed;

    private void Start()
    {
        originalScale = transform.localScale;
        originalPos = transform.localPosition;
        dir = transform.localPosition - transform.parent.transform.localPosition;
        dir.Normalize();
    }

    private void Update()
    {
        if (selected)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, originalScale * percentToGrow, smoothSpeed * Time.deltaTime);
            transform.localPosition = Vector3.Lerp(transform.localPosition, originalPos + dir * percentMove, smoothSpeed * Time.deltaTime);
        }
        else
        {
            transform.localScale = Vector3.Lerp(transform.localScale, originalScale, smoothSpeed * 2 * Time.deltaTime);
            transform.localPosition = Vector3.Lerp(transform.localPosition, originalPos, smoothSpeed * Time.deltaTime);
        }
    }

    public void Select()
    {
        selected = !selected;
    }

    public void Select(Action _doIfSelected)
    {
        if (selected) _doIfSelected();
        else selected = !selected;
    }

    public void Deselect()
    {
        selected = false;
    }

    public Transform GetOriginalTransform()
    {
        Transform _t = transform;
        _t.localScale = originalScale;
        _t.localPosition = originalPos;
        return _t;
    }
}