using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFragment : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb2d;
    private SpriteRenderer spriteRenderer;
    public float roationSpeed = 150f;
    public float stopRoationSpeed = 1;
    bool startFadeOut = false;
    public float fadetime = 1.0f;
    private IEnumerator coroutine;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 moveSpeed = rb2d.velocity;

        if (moveSpeed.magnitude > stopRoationSpeed && !startFadeOut)
        {
            Vector3 blcokedRotationAxis = new Vector3(0, 0, 1);
            transform.Rotate(moveSpeed * roationSpeed * Time.deltaTime * blcokedRotationAxis);
        }
        else
        {
            startFadeOut = true;
            coroutine = fadeOut(spriteRenderer, fadetime);
            StartCoroutine(coroutine);

        }
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
