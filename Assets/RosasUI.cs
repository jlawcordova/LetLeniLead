using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RosasUI : MonoBehaviour
{
    private Text text { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        text = gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = GameManager.Instance.TotalRosas.ToString();
    }
}
