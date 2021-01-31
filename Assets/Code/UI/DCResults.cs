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

    bool m_IsTimeAttack;
    public bool IsTimeAttack { get => m_IsTimeAttack; }

    // Should be in DCWidget, to be updated post GGJ
    public ui.DelegateCommand QuitCommand { get; private set; }

    public DCResults()
    {
        QuitCommand = new ui.DelegateCommand(this.OnQuitCommand);
    }

    #region Getters
    public override gui.EWidgetType GetWidgetType() { return gui.EWidgetType.Results; }

    protected override string GetFileName() { return "ResultScreen"; }
    #endregion

    // Should be in DCWidget too.
    void OnQuitCommand(object param)
    {
        //Should show a confirmation
        Application.Quit();
    }

    public void SetResultData(float time, int score, bool timeAttack)
    {
        m_Minutes = Mathf.FloorToInt(time / 60.0f);
        m_Seconds = Mathf.FloorToInt(time % 60);
        m_Milliseconds = Mathf.FloorToInt((time % 1.0f) * 100);

        m_Score = score;
        m_IsTimeAttack = timeAttack;

        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Minutes"));
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Seconds"));
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Milliseconds"));
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Score"));
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsTimeAttack"));
    }

    public event PropertyChangedEventHandler PropertyChanged;
}
