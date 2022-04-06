using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(2)]
public class LocationBanner : MonoBehaviour
{
    public Sprite[] Banner;

    void Start()
    {
        var bannerIndex = LevelManager.Instance.LevelTypeIndex;
        
        var image = gameObject.GetComponent<Image>();
        image.sprite = Banner[bannerIndex];
    }
}
