using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gui
{
    public enum EWidgetType : int
    {
        MainMenu = 0,
        Credits,

        Vignette,
        CharacterResources,
        Minimap,
        Timer,

        GameMenu,
        Options,
        LoadingScreen,

        Results,

        MAX
    };
}

public class ViewModelManager : MonoBehaviour
{
    Dictionary<gui.EWidgetType, DCWidget> LoadedWidgets;
    List<DCWidget> NewWidgets;
    Vector2 m_CurrentResolution;
    bool m_NeedsRatioUpdate;

    void Awake()
    {
        // To prevent duplicates for that kind of thing
        if (FindObjectsOfType<ViewModelManager>().Length > 1)
        {
            DestroyImmediate(gameObject);
            return;
        }

        DontDestroyOnLoad(this);
        Debug.Assert((int)gui.EWidgetType.MAX == 10, "New widget type added, it needs to be added");
        LoadedWidgets = new Dictionary<gui.EWidgetType, DCWidget>();
        NewWidgets = new List<DCWidget>();
        m_CurrentResolution = new Vector2(Screen.width, Screen.height);
        m_NeedsRatioUpdate = true;
    }

    #region Updates
    private void Update()
    {
        if (m_CurrentResolution.x != Screen.width || m_CurrentResolution.y != Screen.height)
        {
            m_CurrentResolution = new Vector2(Screen.width, Screen.height);
            m_NeedsRatioUpdate = true;
        }
    }

    private void LateUpdate()
    {
        if (NewWidgets.Count > 0 || m_NeedsRatioUpdate)
        {
            if (!Camera.main)
                return;

            NoesisView noesisView = Camera.main.GetComponent<NoesisView>();
            if (!noesisView)
                return;

            if (m_NeedsRatioUpdate)
            {
                Noesis.Grid root = (Noesis.Grid)noesisView.Content.FindName("ContentRoot");
                Debug.Assert(root != null, "No ContentRoot found when updating ratio");
                if (root != null)
                {
                    root.Width = Mathf.RoundToInt(1080 * m_CurrentResolution.x / m_CurrentResolution.y);
                    m_NeedsRatioUpdate = false;
                }

            }

            if (NewWidgets.Count > 0)
            {
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
#endregion

    public DCWidget CreateWidget(gui.EWidgetType widgetType)
    {
        if (!LoadedWidgets.ContainsKey(widgetType))
        {
            DCWidget newWidget;
            switch (widgetType)
            {
                case gui.EWidgetType.MainMenu:
                    newWidget = gameObject.AddComponent(typeof(DCMainMenu)) as DCWidget;
                    break;

                case gui.EWidgetType.CharacterResources:
                    newWidget = gameObject.AddComponent(typeof(DCCharacterResources)) as DCWidget;
                    break;

                case gui.EWidgetType.Minimap:
                    newWidget = gameObject.AddComponent(typeof(DCMinimap)) as DCWidget;
                    break;

                case gui.EWidgetType.Timer:
                    newWidget = gameObject.AddComponent(typeof(DCTimer)) as DCWidget;
                    break;

                case gui.EWidgetType.Results:
                    newWidget = gameObject.AddComponent(typeof(DCResults)) as DCWidget;
                    break;

                case gui.EWidgetType.Vignette:
                    newWidget = gameObject.AddComponent(typeof(DCVignette)) as DCWidget;
                    break;

                default:
                case gui.EWidgetType.MAX:
                    Debug.LogError("Widget type not recognized !");
                    return null;
            }

            LoadedWidgets.Add(widgetType, newWidget);
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
}
