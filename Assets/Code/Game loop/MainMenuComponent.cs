using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuComponent : MonoBehaviour
{
    void Start()
    {
        FindObjectOfType<ViewModelManager>().GetComponent<ViewModelManager>().CreateWidget(gui.EWidgetType.MainMenu);
    }
}
