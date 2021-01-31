using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float playerSpeedMultipler = 1.0f;
    public PlayerController playercontoller;
    void FixedUpdate()
    {
        Vector2 playerVelocity = playercontoller.GetVelocity();
        float paralexForce = Time.deltaTime * playerSpeedMultipler;
        if (playerVelocity.x != 0)
        {
            if (playerVelocity.x > 0)
            {
                transform.position = new Vector3(transform.position.x - paralexForce, transform.position.y, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(transform.position.x + paralexForce, transform.position.y, transform.position.z);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

}
