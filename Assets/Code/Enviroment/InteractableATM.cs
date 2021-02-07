using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableATM : IInteractableObject
{

    public override void Interact(PlayerResources player) 
    {
        ATMCoinAddition coinAddition = GetComponentInParent<ATMCoinAddition>();
        if (player.GetCurrentMoney() < player.GetMaximumMoney())
        {
            AkSoundEngine.PostEvent("ATM_Use", player.gameObject);
            int coinToFloat = player.GetMaximumMoney() - player.GetCurrentMoney();

            StartCoroutine(coinAddition.FloatCoinsToPlayer(coinToFloat, this, player));
        }
        else
        {
            StartCoroutine(coinAddition.PlayerFull(this));
            AkSoundEngine.PostEvent("ATM_PlayerFull", player.gameObject);
        }

        player.RefillMoney();
    }
}
