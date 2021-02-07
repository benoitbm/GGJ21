using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATMCoinAddition : MonoBehaviour
{
    public SpriteRenderer[] coins;
    public SpriteRenderer fulOfCoinsicon;
    public float individualCoinActivationDelay = 0.5f;
    public float fadeoutTime = 0.5f;
    public float hightoffset = 4f;
    public float hightoffsetMaxCoin = 1.0f;
    public float moveSpeedMultiplayer = 2f;
    public IEnumerator FloatCoinsToPlayer(int coinsToFloat, InteractableATM atm, PlayerResources player)
    {
        int i = 0; ;
        foreach (var coin in coins)
        {
            coin.transform.position = new Vector3(atm.transform.position.x, atm.transform.position.y + hightoffset, atm.transform.position.z);
            coin.enabled = false;
            i++;
        }

        //1st Coin is immidiate
        i = 0;
        StartCoroutine(MoveCoinToPlayerWhileFading(i, fadeoutTime, player));
        i++;
        float timer = 0;
        while (i < coinsToFloat)
        {
            if (timer < individualCoinActivationDelay)
            {
                timer += Time.deltaTime;
            }
            else
            {
                timer = 0;
                StartCoroutine(MoveCoinToPlayerWhileFading(i, fadeoutTime, player));
                i++;
            }
            yield return null;
        }
    }
    IEnumerator MoveCoinToPlayerWhileFading(int index, float duration, PlayerResources player)
    {
        float counter = 0;
        //Get current color
        SpriteRenderer MyRenderer = coins[index]; ;
        Color spriteColor = MyRenderer.material.color;
        MyRenderer.enabled = true;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            //Fade from 1 to 0
            float alpha = Mathf.Lerp(1, 0, counter / duration);

            //Change alpha only
            MyRenderer.color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, alpha);
            Vector3 distance = MyRenderer.transform.position - player.transform.position;
            MyRenderer.transform.position = MyRenderer.transform.position - (distance * Time.deltaTime * moveSpeedMultiplayer);
            //Wait for a frame
            yield return null;
        }
        //Reset Coing
        MyRenderer.enabled = false;
    }

    public IEnumerator PlayerFull(InteractableATM atm)
    {
        float counter = 0;
        //Get current color
        SpriteRenderer MyRenderer = fulOfCoinsicon;
        MyRenderer.transform.position = new Vector3(atm.transform.position.x, atm.transform.position.y , atm.transform.position.z);
        Color spriteColor = MyRenderer.material.color;
        MyRenderer.enabled = true;
        while (counter < fadeoutTime)
        {
            counter += Time.deltaTime;
            //Fade from 1 to 0
            float alpha = Mathf.Lerp(1, 0, counter / fadeoutTime);

            //Change alpha only
            MyRenderer.color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, alpha);
            //Wait for a frame
            yield return null;
        }
        //Reset Coing
        MyRenderer.enabled = false;
    }
}
