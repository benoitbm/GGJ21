using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
#region Members
    bool m_InteractAvailable = false;

    [SerializeField, Tooltip("The render of the background of the prompt")]
    GameObject m_PromptBackground;

    IInteractableObject m_InteractibleObject;
    #endregion

#region Updates
    private void Update()
    {
        bool canInteract = m_InteractAvailable && m_InteractibleObject && m_InteractibleObject.CanBeInteracted();
        m_PromptBackground.SetActive(canInteract);
        if (Input.GetButtonDown("Interact"))
        {
            m_InteractibleObject.Interact(GetComponent<PlayerResources>());
        }
    }
#endregion

#region Setters
    public void ClearInteraction() { m_InteractAvailable = false; }
#endregion

    #region Functions
    public void InteractivityCheck(Collider2D collider)
    {
        if (collider.GetComponent<IInteractableObject>())
        {
            m_InteractAvailable = true;
            m_InteractibleObject = collider.GetComponent<IInteractableObject>();
        }
    }
    #endregion
}
