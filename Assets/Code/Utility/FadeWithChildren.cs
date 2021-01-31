using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeWithChildren : MonoBehaviour
{
    // Start is called before the first frame update
    public float fadetime = 0.5f;
    private IEnumerator coroutine;

    public void StartFadeWithChildren()
    {
        foreach (Transform child in transform)
        {
            coroutine = fadeOut(child.gameObject.GetComponent<SpriteRenderer>(), fadetime);
            StartCoroutine(coroutine);
        }
        coroutine = fadeOut(gameObject.GetComponent<SpriteRenderer>(), fadetime);
        StartCoroutine(coroutine);
    }

    IEnumerator fadeOut(SpriteRenderer MyRenderer, float duration)
    {
        float counter = 0;
        //Get current color
        Color spriteColor = MyRenderer.material.color;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            //Fade from 1 to 0
            float alpha = Mathf.Lerp(1, 0, counter / duration);
            //Debug.Log(alpha);

            //Change alpha only
            MyRenderer.color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, alpha);
            //Wait for a frame
            yield return null;
        }
        Destroy(MyRenderer.gameObject);
    }
}

