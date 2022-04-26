using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public AudioClip Sound;
    public GameObject PowerUpCutScene;
    public GameObject IpanaloCutscene;
    public GameObject KulayRosasCutScene;

    public void Consume()
    {
        AudioManager.Play("PowerUp", Sound, 1, false, 1f);
        Destroy(gameObject);

        GameManager.Instance.Freeze();
        Instantiate(PowerUpCutScene, Canvas.Instance.transform);

        var chance = Random.Range(0, 2);
        if (chance == 0)
        {
            Instantiate(IpanaloCutscene, Canvas.Instance.transform);
        }
        else
        {
            Instantiate(KulayRosasCutScene, Canvas.Instance.transform);
        }

        AudioManager.PlaySpecialMusic();
    }
}
