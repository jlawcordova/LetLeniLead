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
    
    public Text TitleText;
    public Image ImageImage;
    public Text DescriptionText;
    public Text CostText;
    

    void Start()
    {
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
        SelectedUpgradeItem = selectedUpgradeItem;
        TitleText.text = selectedUpgradeItem.Title;
        ImageImage.sprite = selectedUpgradeItem.UpgradeImage;
        DescriptionText.text = selectedUpgradeItem.Description;
        CostText.text = selectedUpgradeItem.Cost.ToString();
    }

    public void BuySelected()
    {
        // TODO: Buy Selected.
    }

    public static void SetMain()
    {
        SceneManager.LoadScene("Main");
    }
}
