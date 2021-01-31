using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AMB_Interior_Exterior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("INTERIOR");
            AkSoundEngine.SetState("Ambince_State", "Inside");
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("EXTERIOR");
            AkSoundEngine.SetState("Ambince_State", "Outside");
        }
    }
}
