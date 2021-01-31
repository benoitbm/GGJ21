using System.ComponentModel;

public class DCCharacterResources : DCWidget, INotifyPropertyChanged
{
    int m_Score = 0;
    public int Score { get => m_Score; }
    
    int m_Money = 0;
    public int Money { get => m_Money; }

    #region Getters
    public override gui.EWidgetType GetWidgetType() { return gui.EWidgetType.CharacterResources; }

    protected override string GetFileName() { return "PlayerResources.xaml"; }
    #endregion

    public override void Initialize(NoesisView panel)
    {
        base.Initialize(panel);
        m_DC.DataContext = this;
    }

    #region Setters
    public void SetScore(int newScore)
    {
        if (m_Score != newScore)
        {
            m_Score = newScore;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Score"));
        }
    }

    public void SetMoney(int newMoney)
    {
        if (m_Money != newMoney)
        {
            m_Money = newMoney;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Money"));
        }
    }
    #endregion

    public event PropertyChangedEventHandler PropertyChanged;
}
