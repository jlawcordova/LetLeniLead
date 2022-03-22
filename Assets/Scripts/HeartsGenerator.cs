using UnityEngine;

public class HeartsGenerator : MonoBehaviour
{
    public GameObject Heart;

    public float Rate = 100f;
    public float FirstY = 2f;
    public float SecondY = -2f;
    private float Counter = 0f;

    void FixedUpdate()
    {
        if(Counter > Rate)
        {
            Instantiate(Heart, new Vector3(transform.position.x, transform.position.y + FirstY, 0f), Quaternion.identity);
            Instantiate(Heart, new Vector3(transform.position.x, transform.position.y + SecondY, 0f), Quaternion.identity);
            Counter = 0f;
        }

        Counter++;
    }
}
