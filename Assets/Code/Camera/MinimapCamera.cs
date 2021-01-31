using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCamera : MonoBehaviour
{
    [SerializeField, Tooltip("Render texture for the minimap.")]
    RenderTexture m_Texture;


    // Start is called before the first frame update
    void Start()
    {
        DCMinimap dc = (DCMinimap)FindObjectOfType<ViewModelManager>().GetComponent<ViewModelManager>().CreateWidget(gui.EWidgetType.Minimap);
        dc.SetMinimapTexture(m_Texture);
    }
}
