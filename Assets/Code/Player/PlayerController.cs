using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Members
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
    PlayerInteractions m_PlayerInteractions;
    PlayerAnimationController m_PlayerAnimationController;

    Vector2 m_Velocity;
    float m_SpeedModifier = 1.0f;
    float m_SpeedModifierTimer = 0.0f;

    bool m_TouchGround;
    bool m_RequestedJump;
    bool m_Falling;

    [SerializeField, Tooltip("The aimed timescale when slowing down time to charge power")]
    float m_SlowdownTimescale = 0.5f;
    [SerializeField, Tooltip("The slowdown speed Modifier")]
    float m_SlowdownTimescaleSpeed = 1.0f;
    float m_WantedTimeScale = 1.0f;

    Vector2 m_DashDirection;
    float m_DashIntensity;
    bool m_IsAiming;

    bool m_IsInside;


    Vector3 m_PreviousFramePosition;

    [SerializeField, Tooltip("The maximum power intensity when dashing")]
    float m_DashMaxIntensity = 20.0f;
    [SerializeField, Tooltip("The charging speed modifier when preparing a dash")]
    float m_DashChargingSpeed = 5.0f;
    [SerializeField, Range(1,100), Tooltip("The minimum required percentage to trigger a dash")]
    int m_RequiredPercentageToDash = 20;

    DCVignette m_DCVignette;
    #endregion

    #region Getters

    public Vector2 GetDashDirection() { return m_DashDirection; }
    public bool IsAiming() { return m_IsAiming; }

    public float GetDashIntensityCharge() { return m_DashIntensity / m_DashMaxIntensity; }

    public Vector2 GetVelocity() { return m_Velocity; }

    public bool GetTouchGround() { return m_TouchGround; }

    public bool GetIsJumpRequested() { return m_RequestedJump; }
    #endregion

    #region Setters
    public void SetSpeedModifier(float speedMult, float timer) { m_SpeedModifier = speedMult; m_SpeedModifierTimer = timer; }
    #endregion

#region Inits
    // Start is called before the first frame update
    void Start()
    {
        m_PlayerCollider = GetComponent<BoxCollider2D>();
        m_PlayerInteractions = GetComponent<PlayerInteractions>();
        m_PlayerAnimationController = GetComponentInChildren<PlayerAnimationController>();
        m_DCVignette = (DCVignette)FindObjectOfType<ViewModelManager>().GetComponent<ViewModelManager>().CreateWidget(gui.EWidgetType.Vignette);
    }
#endregion

#region Updates
    private void Update()
    {
        if (Input.GetMouseButton(0) || Mathf.Abs(Input.GetAxisRaw("Aim")) >= 0.2)
        {
            m_IsAiming = true;
            CaptureMouse();
            m_WantedTimeScale = Mathf.MoveTowards(m_WantedTimeScale, m_SlowdownTimescale, Time.unscaledDeltaTime * m_SlowdownTimescaleSpeed);
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

        UpdateMovement();
    }

    // Update is called once per frame
    void UpdateMovement()
    {
        if (m_TouchGround)
        {
            m_Velocity.y = 0;
        }

        if (m_RequestedJump && (m_TouchGround || m_Falling))
        {
            m_Velocity.y = Mathf.Sqrt(m_JumpHeight * Mathf.Abs(Physics2D.gravity.y));
            m_Falling = false;
            m_PlayerAnimationController.OnPlayerJump();
        }
        float speedModif = 1.0f;
        if (m_SpeedModifierTimer > 0.0f)
        {
            m_SpeedModifierTimer -= Time.deltaTime;
            speedModif = m_SpeedModifier;
            m_Velocity *= (1.0f - m_SpeedModifier);
        }

        // Applying the gravity
        m_Velocity.y += Physics2D.gravity.y * Time.deltaTime;

        transform.Translate(m_Velocity * Time.deltaTime * speedModif);

        // Hit detection part
        m_TouchGround = false;
        m_PlayerInteractions.ClearInteraction();

        Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, m_PlayerCollider.size, 0, ~(LayerMask.GetMask("Glass"))); 
        foreach (Collider2D hit in hits)
        {
            if (hit == m_PlayerCollider)
                continue;

            if (hit.isTrigger)
            {
                m_PlayerInteractions.InteractivityCheck(hit);
                continue;
            }

            if (hit.GetComponent<GlassWindow>())
            {
                GlassWindow window = hit.GetComponent<GlassWindow>();
                if(!window.GetIsDestroyed())
                {
                    window.PlayerImpact(gameObject);
                }
            }

            if (hit.tag == "Appartment")
            {
                m_IsInside = true;
            }

                ColliderDistance2D hitDistance = hit.Distance(m_PlayerCollider);
            if (hitDistance.isOverlapped)
            {
                // Move back from the collision
                transform.Translate(hitDistance.pointA - hitDistance.pointB);
                // Checking if there's a ground below (90 degrees = bottom)
                if (Vector2.Angle(hitDistance.normal, Vector2.up) < 90 && m_Velocity.y < 0)
                {
                    m_TouchGround = true;
                    m_Falling = false;
                }
            }

            //Stopping Velocity if hit something while in air
            if (!m_Falling && !m_TouchGround && (hit.tag == "Wall" || hit.tag == "Cieling"))
            {
                Vector2 stopVelocityOnAxis = Vector2.zero;
                if (hit.tag == "Wall")
                {
                    //Debug.Log("Impact A Wall");
                    stopVelocityOnAxis = stopVelocityOnAxis + new Vector2(0, 1);
                }
                if (hit.tag == "Cieling")
                {
                    //Debug.Log("Impact Cieling");
                    stopVelocityOnAxis = stopVelocityOnAxis + new Vector2(1, 0);
                }

                //float changeInPositionX = Mathf.Abs(m_PreviousFramePosition.x - transform.position.x);
                if (stopVelocityOnAxis != Vector2.zero)
                {
                    //Debug.Log(changeInPositionX);
                    m_Velocity = m_Velocity* stopVelocityOnAxis;
                    m_Falling = true;
                    //SetSpeedModifier(0.3f, 0.5f);
                }
                m_PreviousFramePosition = transform.position;
            }
        }
        //Handle Ambience
        if(m_IsInside)
        {
            AkSoundEngine.SetState("Ambince_State", "Inside");
        }
        else
        {
            AkSoundEngine.SetState("Ambince_State", "Outside");
        }
        m_IsInside = false;

        // Physics update was done, we can clear the jump flag.
        m_RequestedJump = false;
    }

    private void LateUpdate()
    {
        float desiredOpacity = 1.0f - ((m_WantedTimeScale - m_SlowdownTimescale) / (1.0f - m_SlowdownTimescale));
        m_DCVignette.UpdateOpacity(desiredOpacity);
    }
    #endregion

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
            m_Velocity = (m_DashDirection * m_DashIntensity);
            m_TouchGround = false;
            m_Falling = true;
        }

        m_DashIntensity = 0.0f;
        m_IsAiming = false;
    }
}
