using System;
using UnityEngine;

public class DCWidget : MonoBehaviour
{
    protected string m_AssetPath = "UI/";
    protected Noesis.UserControl m_DC;
    protected NoesisView m_View;

#region Getters
    public virtual gui.EWidgetType GetWidgetType() { return gui.EWidgetType.MAX; }

    protected virtual string GetFileName() { return ""; }

    public virtual bool HideStatesBelow() { return false; }

    protected virtual int GetOrderOverride() { return -1; }
    #endregion

#region Setters
    public void ChangeVisibility(Noesis.Visibility vis) { m_DC.Visibility = vis; }
#endregion

    // Start is called before the first frame update
    public virtual void Initialize(NoesisView panel)
    {
        m_View = panel;
        Noesis.Grid root = (Noesis.Grid)panel.Content.FindName("ContentRoot");
        NoesisXaml xaml = (NoesisXaml)UnityEngine.Resources.Load(m_AssetPath + GetFileName(), typeof(NoesisXaml));
        m_DC = (Noesis.UserControl)xaml.Load();
        if (GetOrderOverride() >= 0)
        {
            root.Children.Insert(GetOrderOverride(), m_DC);
        }
        else
        {
            root.Children.Add(m_DC);
        }
        m_DC.DataContext = this;
    }

    // Update is called once per frame
    public virtual void Close(NoesisView panel)
    {
        Noesis.Grid root = (Noesis.Grid)panel.Content.FindName("ContentRoot");
        if (root.Children.Contains(m_DC))
        {
            root.Children.Remove(m_DC);
        }
    }
}
