using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipButton : MonoBehaviour
{
    public void OnClick()
    {
        VolunterGenerator.Stop();
        Destroy(gameObject);
    }
}
