using UnityEngine;
using System.ComponentModel;

public class DCTimer : DCWidget, INotifyPropertyChanged
{
    int m_Minutes;
    public int Minutes { get => m_Minutes; }

    int m_Seconds;
    public int Seconds { get => m_Seconds; }

    int m_Milliseconds;
    public int Milliseconds { get => m_Milliseconds; }

    float m_Angle;
    public float Angle { get => m_Angle; }

    bool m_IsTimeAttack;

    #region Getters
    public override gui.EWidgetType GetWidgetType() { return gui.EWidgetType.Timer; }

    protected override string GetFileName() { return "Timer"; }
    #endregion

    public void SetIsTimeAttack(bool isTimeAttack) { m_IsTimeAttack = isTimeAttack; }

    public void UpdateRemainingTime(float remainTime)
    {
        m_Minutes = Mathf.FloorToInt(remainTime / 60.0f);
        m_Seconds = Mathf.FloorToInt(remainTime % 60);
        m_Milliseconds = Mathf.FloorToInt((remainTime % 1.0f) * 100);

        if (m_IsTimeAttack)
        {
            m_Angle = 360.0f * (m_Seconds / 60.0f);
        }
        else
        {
            m_Angle = 360.0f * (1.0f - (m_Seconds / 60.0f));
        }

        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Minutes"));
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Seconds"));
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Milliseconds"));
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Angle"));
    }

    public event PropertyChangedEventHandler PropertyChanged;
}
