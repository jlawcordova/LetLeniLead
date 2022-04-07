using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolunteerUpgradeItem : UpgradeItem
{
    public override void Buy()
    {
        Debug.Log("Buy Volunteer");
        GameManager.Instance.Volunteers++;
        GameManager.Instance.TotalRosas -= Cost;
        SaveSerial.SaveGame();
    }
}
