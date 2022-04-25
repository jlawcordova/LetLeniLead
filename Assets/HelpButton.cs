using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpButton : MonoBehaviour
{
    public GameObject HelpPanel;

    public void OnClick()
    {
        Instantiate(HelpPanel, Canvas.Instance.transform);
    }
}
