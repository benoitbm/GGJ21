using System.ComponentModel;
using UnityEngine;

public class DCResults : DCWidget, INotifyPropertyChanged
{
    int m_Minutes;
    public int Minutes { get => m_Minutes; }

    int m_Seconds;
    public int Seconds { get => m_Seconds; }

    int m_Milliseconds;
    public int Milliseconds { get => m_Milliseconds; }

    int m_Score;
    public int Score { get => m_Score; }

    int m_MaxScore;
    public int MaxScore { get => m_MaxScore; }

    bool m_IsTimeAttack;
    public bool IsTimeAttack { get => m_IsTimeAttack; }

    #region Getters
    public override gui.EWidgetType GetWidgetType() { return gui.EWidgetType.Results; }

    protected override string GetFileName() { return "ResultScreen"; }
    #endregion

    public void SetResultData(float time, int score, int maxScore, bool timeAttack)
    {
        m_Minutes = Mathf.FloorToInt(time / 60.0f);
        m_Seconds = Mathf.FloorToInt(time % 60);
        m_Milliseconds = Mathf.FloorToInt((time % 1.0f) * 100);

        m_Score = score;
        m_MaxScore = maxScore;
        m_IsTimeAttack = timeAttack;

        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Minutes"));
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Seconds"));
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Milliseconds"));
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Score"));
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MaxScore"));
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsTimeAttack"));
    }

    public event PropertyChangedEventHandler PropertyChanged;
}
