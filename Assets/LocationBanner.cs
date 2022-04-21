using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(2)]
public class LocationBanner : MonoBehaviour
{
    public Sprite[] Banner;
    public int LevelTypeIndex = 0;

    void Start()
    {
        var bannerIndex = LevelTypeIndex;
        
        var image = gameObject.GetComponent<Image>();
        image.sprite = Banner[bannerIndex];
    }

    void FixedUpdate()
    {
        if (transform.localPosition.x >= 700)
        {
            Destroy(gameObject);
        }
    }
}
