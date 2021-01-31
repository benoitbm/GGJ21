using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterSound : MonoBehaviour
{
   protected static MasterSound instance;
    public static MasterSound Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(MasterSound)) as MasterSound;

                if (instance == null)
                {
                    Debug.LogWarning("There is no singleton of type " + typeof(MasterSound).ToString() +
                                       " in the scene! Please ensure there's always one before playing.");

                    Debug.LogWarning("Creating provisory " + typeof(MasterSound).ToString() + "...");
                    instance = new GameObject(typeof(MasterSound).ToString()).AddComponent<MasterSound>();
                }
            }

            return instance;
        }
        private set { instance = value; }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void PlaySound(string eventname, GameObject passedObject)
    {
        AkSoundEngine.PostEvent(eventname, passedObject);
    }
    // Update is called once per frame
    public void PlaySound(string eventname)
    {
        AkSoundEngine.PostEvent(eventname, gameObject);
    }
    public void SetRTPC(string rtpcName, float value)
    {
        AkSoundEngine.SetRTPCValue(rtpcName, value);
    }

}
}
