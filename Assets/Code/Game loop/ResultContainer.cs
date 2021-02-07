using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultContainer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DCResults dc = (DCResults)FindObjectOfType<ViewModelManager>().GetComponent<ViewModelManager>().CreateWidget(gui.EWidgetType.Results);
        GameMaster gameMaster = FindObjectOfType<GameMaster>().GetComponent<GameMaster>();

        float remainTime;
        int score, maxScore;
        bool wasTimeAttack;

        gameMaster.GetResults(out remainTime, out score, out maxScore, out wasTimeAttack);
        dc.SetResultData(remainTime, score, maxScore, wasTimeAttack);
    }
}
