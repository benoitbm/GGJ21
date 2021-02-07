using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResources : MonoBehaviour
{
#region Members
    [SerializeField, Tooltip("Maximum money the player can hold")]
    int m_MaximumMoney = 3;
    int m_CurrentMoney;

    // Number of teeth collected
    int m_Score = 0;
    int m_MaxScore;

    DCCharacterResources m_DC;
    GameMaster m_GM;
    #endregion

    #region Getters
    public int GetCurrentMoney() { return m_CurrentMoney; }
    public int GetCurrentScore() { return m_Score; }
    public int GetMaximumMoney() { return m_MaximumMoney; }
    #endregion

#region Setters
    public void RefillMoney() { m_CurrentMoney = m_MaximumMoney; m_DC.SetMoney(m_CurrentMoney); }
    public void RemoveMoney(int delta = 1) { m_CurrentMoney -= delta; m_DC.SetMoney(m_CurrentMoney); }
    public void IncreaseScore(int delta = 1) 
    { 
        m_Score += delta; 
        m_DC.SetScore(m_Score);

        if (m_Score >= m_MaxScore)
            m_GM.RequestGameEnd();
    }
    #endregion

#region Init
    // Start is called before the first frame update
    void Start()
    {
        m_DC = (DCCharacterResources)FindObjectOfType<ViewModelManager>().GetComponent<ViewModelManager>().CreateWidget(gui.EWidgetType.CharacterResources);
        RefillMoney();

        var truc = FindObjectsOfType<GameMaster>();
        m_GM = FindObjectOfType<GameMaster>().GetComponent<GameMaster>();
        // Only time limit for the moment, update stuff below 
        m_GM.RequestGameTimer(false);

        StartCoroutine(CountMaxScore());
    }
    #endregion

    IEnumerator CountMaxScore()
    {
        yield return new WaitForEndOfFrame();
        m_MaxScore = FindObjectsOfType<InteractableBed>().Length;
        m_GM.SetMaxScore(m_MaxScore);
    }
}
