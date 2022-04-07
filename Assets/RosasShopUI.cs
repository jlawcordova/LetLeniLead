using UnityEngine;
using UnityEngine.UI;

public class RosasShopUI : MonoBehaviour
{
    private Text text { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        text = gameObject.GetComponent<Text>();
        text.text = GameManager.Instance.TotalRosas.ToString();
    }
}
