using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ambience_InteriorExtirior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("OUT");
            AkSoundEngine.SetState("Ambince_State", "Outside");
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("IN");
            AkSoundEngine.SetState("Ambince_State", "Inside");
        }
    }
}
