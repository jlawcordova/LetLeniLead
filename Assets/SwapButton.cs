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
        SetText();
    }

    public void OnClick()
    {
        IsHelp = !IsHelp;
        Help.SetActive(IsHelp);
        Credits.SetActive(!IsHelp);

        SetText();
    }

    private void SetText()
    {
        if (!IsHelp)
        {
            text.text = "Help";
        }
        else
        {
            text.text = "Credits";
        }
    }
}
