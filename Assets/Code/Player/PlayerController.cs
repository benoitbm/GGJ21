using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField, Tooltip("Max speed, in units per second, that the character moves.")]
    float m_Speed = 9;

    [SerializeField, Tooltip("Acceleration while grounded.")]
    float m_WalkAcceleration = 75;

    [SerializeField, Tooltip("Acceleration while in the air.")]
    float m_AirAcceleration = 30;

    [SerializeField, Tooltip("Deceleration applied when character is grounded and not attempting to move.")]
    float m_GroundDeceleration = 70;

    [SerializeField, Tooltip("Max height the character will jump regardless of gravity")]
    float m_JumpHeight = 8;

    BoxCollider2D m_PlayerCollider;

    Vector2 m_Velocity;

    bool m_TouchGround;
    bool m_RequestedJump;

    [SerializeField, Tooltip("The aimed timescale when slowing down time to charge power")]
    float m_SlowdownTimescale = 0.5f;
    float m_WantedTimeScale = 1.0f;

    Vector2 m_DashDirection;
    float m_DashIntensity;
    bool m_IsAiming;

    [SerializeField, Tooltip("The maximum power intensity when dashing")]
    float m_DashMaxIntensity = 20.0f;
    [SerializeField, Tooltip("The charging speed modifier when preparing a dash")]
    float m_DashChargingSpeed = 5.0f;
    [SerializeField, Range(1,100), Tooltip("The minimum required percentage to trigger a dash")]
    int m_RequiredPercentageToDash = 20;

    public Vector2 GetDashDirection() { return m_DashDirection; }
    public bool IsAiming() { return m_IsAiming; }

    public float GetDashIntensityCharge() { return m_DashIntensity / m_DashMaxIntensity; }

    // Start is called before the first frame update
    void Start()
    {
        m_PlayerCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) || Mathf.Abs(Input.GetAxisRaw("Aim")) >= 0.2)
        {
            m_IsAiming = true;
            CaptureMouse();
            m_WantedTimeScale = Mathf.MoveTowards(m_WantedTimeScale, m_SlowdownTimescale, Time.unscaledDeltaTime * 0.5f);
            m_DashIntensity += Time.unscaledDeltaTime * m_DashChargingSpeed;
            m_DashIntensity = Mathf.Clamp(m_DashIntensity, 0.0f, m_DashMaxIntensity);
        }
        else
        {
            ReleaseDash();
            m_WantedTimeScale = Mathf.MoveTowards(m_WantedTimeScale, 1.0f, Time.unscaledDeltaTime);
        }
        Time.timeScale = m_WantedTimeScale;

        // Putting an OR, as the Update and FixedUpdate (which is a regular physics based, regardless of FPS) don't run at same frame rate.
        // So you can have done 5 updates for 1 fixed update.
        m_RequestedJump |= Input.GetButtonDown("Jump");

        float acceleration = m_TouchGround ? m_WalkAcceleration : m_AirAcceleration;
        float deceleration = m_TouchGround ? m_GroundDeceleration : 0;

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        if (horizontalInput != 0)
        {
            // We have an input, adds some velocity
            m_Velocity.x = Mathf.MoveTowards(m_Velocity.x, m_Speed * horizontalInput, acceleration * Time.deltaTime);
        }
        else
        {
            // No input, natural deceleration
            m_Velocity.x = Mathf.MoveTowards(m_Velocity.x, 0.0f, deceleration * Time.deltaTime);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (m_TouchGround)
        {
            m_Velocity.y = 0;

            if (m_RequestedJump)
            {
                m_Velocity.y = Mathf.Sqrt(m_JumpHeight * Mathf.Abs(Physics2D.gravity.y));
            }
        }

        // Applying the gravity
        m_Velocity.y += Physics2D.gravity.y * Time.fixedDeltaTime;

        transform.Translate(m_Velocity * Time.fixedDeltaTime);

        // Hit detection part
        m_TouchGround = false;

        Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, m_PlayerCollider.size, 0); 
        foreach (Collider2D hit in hits)
        {
            if (hit == m_PlayerCollider)
                continue;

            ColliderDistance2D hitDistance = hit.Distance(m_PlayerCollider);
            if (hitDistance.isOverlapped)
            {
                // Move back from the collision
                transform.Translate(hitDistance.pointA - hitDistance.pointB);

                // Checking if there's a ground below (90 degrees = bottom)
                if (Vector2.Angle(hitDistance.normal, Vector2.up) < 90 && m_Velocity.y < 0)
                {
                    m_TouchGround = true;
                }
            }
        }

        // Physics update was done, we can clear the jump flag.
        m_RequestedJump = false;
    }


    private void CaptureMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        Vector3 mousePosWorld = Camera.main.ScreenToWorldPoint(mousePos);

        m_DashDirection = new Vector2(mousePosWorld.x - transform.position.x, mousePosWorld.y - transform.position.y).normalized;
    }

    private void ReleaseDash()
    {
        if (m_IsAiming && GetDashIntensityCharge() >= ((float)m_RequiredPercentageToDash/100.0f))
        {
            m_Velocity += (m_DashDirection * m_DashIntensity);
            m_TouchGround = false;
        }

        m_DashIntensity = 0.0f;
        m_IsAiming = false;
    }
}
