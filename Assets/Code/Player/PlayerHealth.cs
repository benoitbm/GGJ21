using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // Start is called before the first frame update
    PlayerAnimationController m_PlayerAnimationController;
    void Start()
    {
        m_PlayerAnimationController = GetComponentInChildren<PlayerAnimationController>();
    }

    // Update is called once per frame
    public void GetHurt(bool dealsDamage)
    {
        m_PlayerAnimationController.OnPlayerHurt(dealsDamage);
    }
}
