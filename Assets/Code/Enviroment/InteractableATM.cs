using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableATM : IInteractableObject
{
    public override void Interact(PlayerResources player) 
    {
        if(player.GetCurrentMoney() < 3)
        {
            AkSoundEngine.PostEvent("ATM_Use", player.gameObject);
        }
        else
        {
            AkSoundEngine.PostEvent("ATM_PlayerFull", player.gameObject);
        }
        player.RefillMoney();
    }
}
