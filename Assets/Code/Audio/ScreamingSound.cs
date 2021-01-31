using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreamingSound : MonoBehaviour
{
    PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            AkSoundEngine.PostEvent("Shout_Inhale", gameObject);

        }

        if (playerController.IsAiming())
        {
            AkSoundEngine.SetRTPCValue("Shout_Charge", playerController.GetDashIntensityCharge()*100f);

            if (Input.GetMouseButtonUp(0))
            {
                AkSoundEngine.PostEvent("Shout_Shout", gameObject);
            }
        }

        
    }
}
