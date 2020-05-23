// LerpToPosition.cs
// Last edited 2:57 PM 06/11/2015 by Aaron Freedman

using System.Collections;
using UnityEngine;

public class LerpToPosition : MonoBehaviour
{
    public Transform target;
    public float smoothing;
    public float autoRemoveTime;

    // Use this for initialization
    private void Start()
    {

    }

    private void OnEnable()
    {
        StartCoroutine(Disable());
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.position, smoothing * Time.deltaTime);
    }

    private IEnumerator Disable()
    {
        //Debug.Log(gameObject.name + " starting timer");
        yield return new WaitForSeconds(autoRemoveTime);
        enabled = false;
    }

    private void OnDisable()
    {
    }
}