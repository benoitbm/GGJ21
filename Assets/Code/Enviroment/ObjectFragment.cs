using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFragment : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb2d;
    private SpriteRenderer spriteRenderer;
    public float minRoationSpeed = 30;
    public float maxRoationSpeed = 150;
    public float stopRoationSpeed = 1;
    bool startFadeOut = false;
    public float fadetime = 1.0f;
    private IEnumerator coroutine;
    public string soundEvent ="";
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameObject.transform.eulerAngles = new Vector3(
            gameObject.transform.eulerAngles.x,
            gameObject.transform.eulerAngles.y,
            gameObject.transform.eulerAngles.z + Random.Range(-180.0f, 180.0f));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 moveSpeed = rb2d.velocity;

        if (moveSpeed.magnitude > stopRoationSpeed && !startFadeOut)
        {
            transform.Rotate(Vector3.forward, moveSpeed.magnitude * Random.Range(minRoationSpeed, maxRoationSpeed) * Time.deltaTime);
        }
        else
        {
            startFadeOut = true;
            coroutine = fadeOut(spriteRenderer, fadetime);
            StartCoroutine(coroutine);

        }
    }
    
    void OnCollisionEnter2D(Collision2D other)
    {
        if(soundEvent != "")
        {
            AkSoundEngine.PostEvent(soundEvent, gameObject);
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
