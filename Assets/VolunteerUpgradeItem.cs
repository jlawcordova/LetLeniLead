using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolunteerUpgradeItem : UpgradeItem
{
    public override void Buy()
    {
        if (GameManager.Instance.TotalRosas < Cost)
        {
            return;
        }

        GameManager.Instance.Volunteers++;
        GameManager.Instance.TotalRosas -= Cost;
        SaveSerial.SaveGame();
    }
}
