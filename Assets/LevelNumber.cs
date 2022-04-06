using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(2)]
public class LevelNumber : MonoBehaviour
{

    void Start()
    {
        var level = LevelManager.Instance.Level;
        
        var text = gameObject.GetComponent<Text>();
        text.text = level.ToString();
    }
}
