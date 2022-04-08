using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(1)]
public class AngatBuhayUpgradeItem : UpgradeItem
{
    void Start()
    {
        if (GameManager.Instance.Times3Unlocked)
        {
            Acquired = true;
        }
    }

    public override void Buy()
    {
        if (Acquired)
        {
            return;
        }

        if (GameManager.Instance.TotalRosas < Cost)
        {
            return;
        }

        GameManager.Instance.Times3Unlocked = true;
        GameManager.Instance.TotalRosas -= Cost;
        SaveSerial.SaveGame();
        Acquired = true;
        AudioManager.Play("Buy", BuySound, 1, false, 0.8f);
        OnClick();
    }
}
