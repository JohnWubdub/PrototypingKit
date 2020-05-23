using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinWaveMove : MonoBehaviour //some simple sin movement
{
    public enum MoveDirection
    {
        x,
        y,
        z
    };
    public MoveDirection moveDirection;
    public float moveAmount;
    public float moveSpeed;
    public float lerpSpeed;
    private Vector3 originalPos;
    private void Start()
    {
        originalPos = transform.position;
    }
    private void Update()
    {
        Vector3 targetPosition = Vector3.zero; //center of the scene
        switch (moveDirection) //good ol switch case
        {
            case MoveDirection.x:
                targetPosition = new Vector3((Mathf.Sin(Time.time * moveSpeed) * moveAmount) + originalPos.x, originalPos.y, originalPos.z);
                break;
            case MoveDirection.y:
                targetPosition = new Vector3(originalPos.x, (Mathf.Sin(Time.time * moveSpeed) * moveAmount) + originalPos.y, originalPos.z);
                break;
            case MoveDirection.z:
                targetPosition = new Vector3(originalPos.x, originalPos.y, (Mathf.Sin(Time.time * moveSpeed) * moveAmount) + originalPos.z);
                break;
            default:
                break;
        }
        transform.position = Vector3.Lerp(transform.position, targetPosition, lerpSpeed * Time.deltaTime);
    }
}