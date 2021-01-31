using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    [SerializeField, Tooltip("Time (in seconds) to do the level.")]
    int m_MaximumTime = 90;

    // Score parameters
    float m_RemainingTime;
    int m_Score;
    bool m_WasTimeAttack;

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void RequestGameTimer(bool isTimeAttack)
    {
        m_WasTimeAttack = isTimeAttack;
        TimerComponent comp = gameObject.AddComponent(typeof(TimerComponent)) as TimerComponent;
        comp.Init(isTimeAttack, m_MaximumTime);
    }

    public void RequestGameEnd()
    {
        TimerComponent comp = GetComponent<TimerComponent>();
        m_RemainingTime = Mathf.Max(comp.GetRemainingTime(), 0.0f);
        m_WasTimeAttack = comp.IsTimeAttack();
        ViewModelManager viewModel = FindObjectOfType<ViewModelManager>().GetComponent<ViewModelManager>();
        viewModel.RemoveWidget(gui.EWidgetType.Timer);
        viewModel.RemoveWidget(gui.EWidgetType.Minimap);
        viewModel.RemoveWidget(gui.EWidgetType.CharacterResources);
        Destroy(comp);

        PlayerResources playerResources = FindObjectOfType<PlayerResources>().GetComponent<PlayerResources>();
        m_Score = playerResources.GetCurrentScore();

        SceneManager.LoadScene("ResultScreen");
    }

    public void GetResults(out float remainTime, out int score, out bool timeAttack)
    {
        remainTime = m_RemainingTime;
        score = m_Score;
        timeAttack = m_WasTimeAttack;
    }
}
