using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTooth : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite m_FloatingCoin;
    private SpriteRenderer m_SprintRenderer;
    public float m_PickupFadeTime = 0.75f;
    private bool m_ToothPickedUp;
    private float m_startingY;
    private IEnumerator coroutine;
    //Sine

    public float amplitudeY = 5.0f;
    public float m_PickSpeedMultiplyer = 4.0f;

    public float omegaY = 5.0f;
    float index;

    void Start()
    {
        m_SprintRenderer = gameObject.GetComponent<SpriteRenderer>();
        m_startingY = transform.transform.localPosition.y;
    }

    public void Update()
    {
        if (m_ToothPickedUp)
        {
            float y = transform.position.y + (m_PickSpeedMultiplyer* Time.deltaTime);
            transform.position = new Vector3(transform.position.x, y, transform.position.z);
        }
        else
        {
            index += Time.deltaTime;
            float y = Mathf.Abs(m_startingY + (amplitudeY * Mathf.Sin(omegaY * index)));
            transform.localPosition = new Vector3(0, y, 0);
        }

    }
    public void PickupTooth()
    {
        m_ToothPickedUp = true;
        m_SprintRenderer.sprite = m_FloatingCoin;
        coroutine = fadeOut(m_SprintRenderer, m_PickupFadeTime); 
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
            Debug.Log(alpha);

            //Change alpha only
            MyRenderer.color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, alpha);
            //Wait for a frame
            yield return null;
        }
        //Destroy(MyRenderer.gameObject);
    }
}

