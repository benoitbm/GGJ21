using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerComponent : MonoBehaviour
{
    bool m_IsTimeAttack;

    float m_Timer;

    DCTimer m_DC;

    public float GetRemainingTime() { return m_Timer; }
    public bool IsTimeAttack() { return m_IsTimeAttack; }

    public void Init(bool isTimeAttack, float timeLimit = 0.0f)
    {
        m_IsTimeAttack = isTimeAttack;
        m_Timer = isTimeAttack ? 0 : timeLimit;

        m_DC = (DCTimer)FindObjectOfType<ViewModelManager>().GetComponent<ViewModelManager>().CreateWidget(gui.EWidgetType.Timer);
        m_DC.SetIsTimeAttack(isTimeAttack);
        m_DC.UpdateRemainingTime(m_Timer);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_IsTimeAttack)
        {
            m_Timer += Time.deltaTime;
        }
        else
        {
            m_Timer -= Time.deltaTime;
            if (m_Timer <= 0.0f)
            {
                // Time's up, request the end.
                GetComponent<GameMaster>().RequestGameEnd();
            }
        }

        m_DC.UpdateRemainingTime(m_Timer);
    }
}
