using System.ComponentModel;
using UnityEngine;

public class DCMainMenu : DCWidget
{
    #region Getters
    public override gui.EWidgetType GetWidgetType() { return gui.EWidgetType.MainMenu; }

    protected override string GetFileName() { return "MainMenu"; }
    #endregion

}
