using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Appartment : MonoBehaviour
{
    // Start is called before the first frame update
    public FadeWithChildren appartmentCover;
    private bool m_revealed = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnWindowBroke()
    {
        if (!m_revealed)
        {
            m_revealed = true;
            appartmentCover.StartFadeWithChildren();
        }
    }
}
