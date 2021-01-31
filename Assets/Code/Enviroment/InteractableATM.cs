using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableATM : IInteractableObject
{
    public override void Interact(PlayerResources player) 
    {
        player.RefillMoney();
    }
}
