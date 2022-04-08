using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeItem : MonoBehaviour
{
    public Sprite UpgradeImage;
    public string Title;
    public string Description;
    public int Cost;
    public bool Acquired = false;
    public AudioClip BuySound;

    public void OnClick()
    {
        ShopManager.Instance.SetSelectedUpgrade(this);
    }

    public virtual void Buy()
    {
    }
}
