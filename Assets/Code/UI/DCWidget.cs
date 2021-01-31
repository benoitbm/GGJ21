using UnityEngine;

public class DCWidget : MonoBehaviour
{
    protected string m_AssetPath = "Assets/UI/";
    protected Noesis.UserControl m_DC;

#region Getters
    public virtual gui.EWidgetType GetWidgetType() { return gui.EWidgetType.MAX; }

    protected virtual string GetFileName() { return ""; }

    public virtual bool HideStatesBelow() { return false; }
    #endregion

#region Setters
    public void ChangeVisibility(Noesis.Visibility vis) { m_DC.Visibility = vis; }
#endregion

    // Start is called before the first frame update
    public virtual void Initialize(NoesisView panel)
    {
        Noesis.Grid root = (Noesis.Grid)panel.Content.FindName("ContentRoot");
        m_DC = (Noesis.UserControl)Noesis.GUI.LoadXaml(m_AssetPath + GetFileName());
        root.Children.Add(m_DC);
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
