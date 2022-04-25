using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseButton : MonoBehaviour
{
    public GameObject HelpPanel;

    public void OnClick()
    {
        Destroy(HelpPanel);
    }
}
