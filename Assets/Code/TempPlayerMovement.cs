using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempPlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 15.0f;
    private Vector2 velocityBeforePhysicsUpdate;

    [System.NonSerialized] 
    private float speedPercentageModifier = 1.0f;
    private float speedModifierTimer = 0;
    private Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        velocityBeforePhysicsUpdate = rb2d.velocity;
    }
    
    void Update()
    {
        //Store the current horizontal input in the float moveHorizontal.
        float moveHorizontal = Input.GetAxis("Horizontal");

        //Store the current vertical input in the float moveVertical.
        float moveVertical = Input.GetAxis("Vertical");

        //Use the two store floats to create a new Vector2 variable movement.
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
        if(speedModifierTimer > 0)
        {
            speedModifierTimer -= Time.deltaTime;
        }
        else if(speedPercentageModifier != 1)
        {
            speedPercentageModifier = 1;
        }
        rb2d.AddForce(movement * (speed* speedPercentageModifier));
    }
    public void SetSpeedModifyer(float speedPrecModifier, float time)
    {
        speedPercentageModifier = speedPrecModifier;
        speedModifierTimer = time;
    }
    public Vector2 GetPreviousFrameVelocity()
    {
        return velocityBeforePhysicsUpdate;
    }
}
