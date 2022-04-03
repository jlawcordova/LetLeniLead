using UnityEngine;
using UnityEngine.UI;

public class Trivia : MonoBehaviour
{

    public string[] TriviaText;

    void Start()
    {
        var text = gameObject.GetComponent<Text>();
        text.text = TriviaText[Random.Range(0, TriviaText.Length)];
    }
}
