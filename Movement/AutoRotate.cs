using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//script by Aaron Freedman
//edits by John Wanamaker
public class AutoRotate : MonoBehaviour
{
    public Vector3 rotationSpeed;
    public bool randomize;
    public float min, max;
    public bool world;

    private void Start()
    {
        if (randomize)
        {
            rotationSpeed = new Vector3(Random.Range(min, max), Random.Range(min, max), Random.Range(min, max));
        }
    }

    private void Update()
    {
        if (world)
        {
            transform.Rotate(rotationSpeed * Time.deltaTime, Space.World);
        }
        else
        {
            transform.Rotate(rotationSpeed * Time.deltaTime);
    
        }
    }
}