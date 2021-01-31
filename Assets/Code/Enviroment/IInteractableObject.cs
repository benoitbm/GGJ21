using UnityEngine;

public class IInteractableObject : MonoBehaviour
{
    public virtual void Interact(PlayerResources player) { }

    public virtual bool CanBeInteracted() { return true; }
}
