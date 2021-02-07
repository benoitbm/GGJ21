using UnityEngine;
using System.ComponentModel;

public class DCVignette : DCWidget, INotifyPropertyChanged
{
    float m_Opacity = 0.0f;
    public float Opacity { get => m_Opacity; }

    #region Getters
    public override gui.EWidgetType GetWidgetType() { return gui.EWidgetType.Vignette; }

    protected override string GetFileName() { return "Vignette"; }

    protected override int GetOrderOverride() { return 0; }
    #endregion

    public override void Initialize(NoesisView panel)
    {
        base.Initialize(panel);
    }

    public void UpdateOpacity(float newOpacity)
    {
        if (m_Opacity != newOpacity)
        {
            m_Opacity = newOpacity;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Opacity"));
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
}
