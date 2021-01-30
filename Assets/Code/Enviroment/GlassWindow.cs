using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassWindow : MonoBehaviour
{
    public float m_PercentageCollisionResistance = 0.1f;
    public float m_CollisionSpeedReductionTime = 0.1f;
    public float m_ForcePlayerMultiplayer = 15f;
    public float m_SpeedToBreakWindow = 3f;
    public float m_ScreenShakeDuration = 0.1f;
    public float m_ScreenShakeAmount = 0.08f;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerImpact(other.gameObject);
        }    
    }

    public void PlayerImpact(GameObject player)
    {
        Transform playerTransform = player.GetComponent<Transform>();
        PlayerController playerMovement = player.GetComponent<PlayerController>();

        Vector2 moveSpeed = playerMovement.GetVelocity();
        if (moveSpeed.sqrMagnitude > (m_SpeedToBreakWindow * m_SpeedToBreakWindow))
        {
            playerMovement.SetSpeedModifier(m_PercentageCollisionResistance, m_CollisionSpeedReductionTime);
            Break(moveSpeed, playerTransform);
            ScreenShake screenshake = Camera.main.GetComponent<ScreenShake>();
            screenshake.shakeAmount = m_ScreenShakeAmount;
            screenshake.shakeDuration = m_ScreenShakeDuration;
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
            child.gameObject.GetComponent<BreakingGlassWindow>().ApplyForceOnFragments(moveSpeed * m_ForcePlayerMultiplayer, playerTransform);
        }
    }
}
