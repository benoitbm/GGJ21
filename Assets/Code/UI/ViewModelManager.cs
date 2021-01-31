using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gui
{
    public enum EWidgetType : int
    {
        MainMenu = 0,
        Credits,

        CharacterResources,
        Minimap,
        Timer,

        GameMenu,
        Options,
        LoadingScreen,

        MAX
    };
}

public class ViewModelManager : MonoBehaviour
{
    Dictionary<gui.EWidgetType, DCWidget> LoadedWidgets;
    List<DCWidget> NewWidgets;

    void Awake()
    {
        DontDestroyOnLoad(this);
        Debug.Assert((int)gui.EWidgetType.MAX == 8, "New widget type added, it needs to be added");
        LoadedWidgets = new Dictionary<gui.EWidgetType, DCWidget>();
        NewWidgets = new List<DCWidget>();
    }

    public DCWidget CreateWidget(gui.EWidgetType widgetType)
    {
        if (!LoadedWidgets.ContainsKey(widgetType))
        {
            DCWidget newWidget;
            switch (widgetType)
            {
                case gui.EWidgetType.CharacterResources:
                    newWidget = gameObject.AddComponent(typeof(DCCharacterResources)) as DCWidget;
                    LoadedWidgets.Add(widgetType, newWidget);
                    break;

                default:
                case gui.EWidgetType.MAX:
                    Debug.LogError("Widget type not recognized !");
                    return null;
            }

            NewWidgets.Add(newWidget);
            return newWidget;
        }

        return null;
    }

    public void RemoveWidget(gui.EWidgetType widgetType)
    {
        if (LoadedWidgets.ContainsKey(widgetType))
        {
            DCWidget widget = LoadedWidgets[widgetType];
            if (widget.HideStatesBelow())
            {
                for (gui.EWidgetType iType = 0; iType < widgetType; ++iType)
                {
                    if (LoadedWidgets.ContainsKey(iType))
                    {
                        LoadedWidgets[iType].ChangeVisibility(Noesis.Visibility.Visible);
                    }
                }
            }

            widget.Close(Camera.main.GetComponent<NoesisView>());
            LoadedWidgets.Remove(widgetType);
            Destroy(widget);
        }
    }

    private void LateUpdate()
    {
        if (NewWidgets.Count > 0)
        {
            NoesisView noesisView = Camera.main.GetComponent<NoesisView>();
            if (!noesisView)
                return;
            foreach (DCWidget widget in NewWidgets)
            {
                gui.EWidgetType widgetType = widget.GetWidgetType();
                widget.Initialize(noesisView);

                if (widget.HideStatesBelow())
                {
                    for (int i = 0; i < (int)widgetType; ++i)
                    {
                        gui.EWidgetType iType = (gui.EWidgetType)i;
                        if (LoadedWidgets.ContainsKey(iType))
                        {
                            LoadedWidgets[iType].ChangeVisibility(Noesis.Visibility.Collapsed);
                        }
                    }
                }
            }

            NewWidgets.Clear();
        }
    }

}
