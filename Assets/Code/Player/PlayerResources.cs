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

    DCCharacterResources m_DC;
    #endregion

    #region Getters
    public int GetCurrentMoney() { return m_CurrentMoney; }
    public int GetCurrentScore() { return m_Score; }
    #endregion

#region Setters
    public void RefillMoney() { m_CurrentMoney = m_MaximumMoney; m_DC.SetMoney(m_CurrentMoney); }
    public void RemoveMoney(int delta = 1) { m_CurrentMoney -= delta; m_DC.SetMoney(m_CurrentMoney); }
    public void IncreaseScore(int delta = 1) { m_Score += delta; m_DC.SetScore(m_Score); }
    #endregion

#region Init
    // Start is called before the first frame update
    void Start()
    {
        m_DC = (DCCharacterResources)FindObjectOfType<ViewModelManager>().GetComponent<ViewModelManager>().CreateWidget(gui.EWidgetType.CharacterResources);
        RefillMoney();

        FindObjectOfType<GameMaster>().GetComponent<GameMaster>().RequestGameTimer(false);
    }
#endregion


}
