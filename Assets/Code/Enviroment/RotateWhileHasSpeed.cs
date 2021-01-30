using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWhileHasSpeed : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb2d;
    public float roationSpeed = 1.5f;
    public float stopRoationSpeed = 2;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 moveSpeed = rb2d.velocity;

        if (moveSpeed.magnitude > stopRoationSpeed)
        {
            //Debug.Log(moveSpeed.magnitude * roationSpeed * Time.deltaTime);
            Vector3 blcokedRotationAxis = new Vector3(0, 0, 1);
            transform.Rotate(moveSpeed * roationSpeed * Time.deltaTime * blcokedRotationAxis);
            //rb2d.rotation = moveSpeed.magnitude * roationSpeed * Time.deltaTime;
        }
    }
}
