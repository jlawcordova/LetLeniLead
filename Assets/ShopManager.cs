using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance;
    public UpgradeItem SelectedUpgradeItem;
    public GameObject TitleObject;
    public GameObject ImageObject;
    public GameObject DescriptionObject;
    public GameObject CostObject;
    public GameObject UpgradePanelCover;
    public GameObject RosasShopUI;
    
    public Text RosasShopUIText;
    public Text TitleText;
    public Image ImageImage;
    public Text DescriptionText;
    public Text CostText;
    

    void Start()
    {
        RosasShopUIText = RosasShopUI.GetComponent<Text>();
        TitleText = TitleObject.GetComponent<Text>();
        DescriptionText = DescriptionObject.GetComponent<Text>();
        ImageImage = ImageObject.GetComponent<Image>();
        CostText = CostObject.GetComponent<Text>();
    }

    private void Awake() 
    { 
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }

    public void SetSelectedUpgrade(UpgradeItem selectedUpgradeItem)
    {
        if(UpgradePanelCover != null)
        {
            Destroy(UpgradePanelCover);
        }

        SelectedUpgradeItem = selectedUpgradeItem;
        TitleText.text = selectedUpgradeItem.Title;
        ImageImage.sprite = selectedUpgradeItem.UpgradeImage;
        DescriptionText.text = selectedUpgradeItem.Description;
        CostText.text = selectedUpgradeItem.Cost.ToString();
    }

    public void BuySelected()
    {
        Debug.Log("SelectedUpgradeItem " + SelectedUpgradeItem);
        SelectedUpgradeItem.Buy();
        RosasShopUIText.text = GameManager.Instance.TotalRosas.ToString();
    }

    public static void SetMain()
    {
        SceneManager.LoadScene("Main");
    }
}
