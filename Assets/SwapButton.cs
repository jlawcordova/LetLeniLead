using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwapButton : MonoBehaviour
{
    public GameObject Help;
    public GameObject Credits;
    public GameObject ButtonText;
    public static bool IsHelp = true;
    private Text text;
    void Start()
    {
        IsHelp = true;
        text = ButtonText.GetComponent<Text>();

        Help.SetActive(IsHelp);
        Credits.SetActive(!IsHelp);
    }

    public void OnClick()
    {
        IsHelp = !IsHelp;
        Help.SetActive(IsHelp);
        Credits.SetActive(!IsHelp);

        if (IsHelp)
        {
            text.text = "Help";
        }
        else
        {
            text.text = "Credits";
        }
    }
}
