using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakingGlassWindow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   public void ApplyForceOnFragments(Vector2 force, Transform playerTransform)
    {
        foreach (Transform child in transform)
        {
            Transform glassFragmentTransform = child.gameObject.GetComponent<Transform>();
            Vector2 direction = (glassFragmentTransform.transform.position - playerTransform.position).normalized;
            Rigidbody2D glassFragment = child.gameObject.GetComponent<Rigidbody2D>();
            glassFragment.AddForce(force.magnitude * direction);
        }
    }
}
