using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRandomaizer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private SpriteRenderer m_MinimapBG;
    private SpriteRenderer spriteRenderer;
    public Sprite[] backGroundspriteList;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        int i = Random.Range(0, backGroundspriteList.Length);
        spriteRenderer.sprite = backGroundspriteList[i];
        m_MinimapBG.sprite = backGroundspriteList[i];
    }
}