using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableBed : IInteractableObject
{
    [SerializeField, Tooltip("Bool about if the tooth was already gained, showed in editor for testing purposes")]
    bool m_ToothCollected = false;

    [SerializeField]
    SpriteRenderer m_MinimapCheck;

    public void Awake()
    {
        m_MinimapCheck.enabled = false;
    }

    public override bool CanBeInteracted()
    {
        return !m_ToothCollected;
    }

    public override void Interact(PlayerResources player)
    {
        if (player.GetCurrentMoney() > 0)
        {
            player.IncreaseScore();
            player.RemoveMoney();
            m_ToothCollected = true;
            m_MinimapCheck.enabled = true;
        }
        else
        {
            // Warn about the score
        }
    }
}
