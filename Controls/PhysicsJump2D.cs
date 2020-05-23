using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//script by Aaron Freedman
//edits by John Wanamaker
[RequireComponent(typeof (Rigidbody2D))]
public class PhysicsJump2D : MonoBehaviour //simple physics jump, call it in update yourself, then reset it when you want it to be null again
{
    public bool extendedJump; // additional jump force if button is held down
    public float force;
    public float forceInit;
    public float extendedJumpForce;
    public float extendedJumpTimeMax;
    public float extendedJumpTime;
    public bool canJump;
    private void Start()
    {
        extendedJumpTime = 0;
        forceInit = force;
    }

    private void Update()
    {
        //call it how you may
    }
    
    
    public void Jump(float overrideForce = 0)
    {
        float _force = (overrideForce != 0) ? overrideForce : force;
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * _force);
        extendedJumpTime = extendedJumpTimeMax;
        canJump = false;
    }
    public void ExtendJump()
    {
        extendedJumpTime -= Time.deltaTime;
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * extendedJumpForce * (Time.deltaTime / extendedJumpTimeMax));
    }
    
    public void Reset()
    {
        extendedJumpTime = 0;
    }
}