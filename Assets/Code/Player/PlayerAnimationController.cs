using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public PlayerController playerContoller;
    private Animator animator;
    private bool m_TouchGroundPreviousFrame;
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Screaming
        bool isCharging = playerContoller.IsAiming();
        animator.SetBool("IsCharging", isCharging);

        //Moveing 
        Vector2 playerVelocity = playerContoller.GetVelocity();

        if (isCharging)
        {
            Vector3 relativePosition =  Camera.main.WorldToScreenPoint(transform.position) - Input.mousePosition;

            if(relativePosition.x > 0)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
        }
        else if(playerVelocity.x != 0)
        {
            animator.SetBool("IsMoving", true);
            if (playerVelocity.x > 0)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }
        //Jumping
        bool currentfameTouchGround = playerContoller.GetTouchGround();
        animator.SetBool("IsTouchingGround", currentfameTouchGround);

        if (!m_TouchGroundPreviousFrame && currentfameTouchGround)
        {
            AkSoundEngine.PostEvent("Landing", gameObject);
        }
        
        m_TouchGroundPreviousFrame = currentfameTouchGround;
    }
    void Update()
    {
        //Charing 
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Charging");
        }
    }
    public void OnPlayerJump()
    {
        animator.SetTrigger("Jumping");
        AkSoundEngine.PostEvent("Swhoosh", gameObject);
    }

    public void OnPlayerHurt(bool isDamageApplyed)
    {
        animator.SetTrigger("Hurt");
    }
    public void OnFalling()
    {
        animator.SetTrigger("Falling");
    }
}

