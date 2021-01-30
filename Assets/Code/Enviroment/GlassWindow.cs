using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassWindow : MonoBehaviour
{
    public float percetnageCollisionResistance = 0.1f;
    public float collisionSpeedReductionTime = 0.1f;
    public float forcePlayerMultiplayer = 15f;
    public float speedToBreakWindow = 3f;
    public float screenShakeDuration = 0.1f;
    public float screenShakeAmount = 0.08f;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Transform playerTransform = other.gameObject.GetComponent<Transform>();
            TempPlayerMovement playerMovement = other.gameObject.GetComponent<TempPlayerMovement>();

            Vector2 moveSpeed = playerMovement.GetPreviousFrameVelocity();
            if (moveSpeed.magnitude > speedToBreakWindow)
            {
                playerMovement.SetSpeedModifyer(percetnageCollisionResistance, collisionSpeedReductionTime);
                Break(moveSpeed, playerTransform);
                ScreenShake screenshake = Camera.main.GetComponent<ScreenShake>();
                screenshake.shakeAmount = screenShakeAmount;
                screenshake.shakeDuration = screenShakeDuration;
            }
        }    
    }
    private void Break(Vector2 moveSpeed, Transform playerTransform)
    {
        Destroy(gameObject.GetComponent<BoxCollider2D>());
        Destroy(gameObject.GetComponent<SpriteRenderer>());

        //Enable the breaking object 
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
            child.gameObject.GetComponent<BreakingGlassWindow>().ApplyForceOnFragments(moveSpeed * forcePlayerMultiplayer, playerTransform);
        }
    }
}
